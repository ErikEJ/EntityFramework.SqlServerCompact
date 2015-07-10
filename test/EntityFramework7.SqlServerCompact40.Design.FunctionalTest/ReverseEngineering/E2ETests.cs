// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using Microsoft.Data.Entity.Commands.Utilities;
using Microsoft.Data.Entity.Relational.Design.CodeGeneration;
using Microsoft.Data.Entity.Relational.Design.ReverseEngineering;
using Microsoft.Data.Entity.Relational.Design.Utilities;
using Microsoft.Framework.Logging;
using Microsoft.Data.Entity.Relational.Design.Templating;
using Microsoft.Data.Entity.Relational.Design.Templating.Compilation;
using Xunit;
using Xunit.Abstractions;
using Microsoft.CodeAnalysis;

namespace EntityFramework7.SqlServerCompact40.Design.FunctionalTest
{
    public class E2ETests : IClassFixture<E2EFixture>
    {
        public const string E2EConnectionString =
            @"Data Source=E2E.sdf";
        private const string TestNamespace = @"E2ETest.Namespace";
        private const string TestOutputDir = @"E2ETest\Output\Dir";
        private static readonly List<string> _E2ETestExpectedWarnings = new List<string>();
        private static readonly List<string> _E2ETestExpectedFileNames = new List<string>
            {
                @"E2EContext.cs",
                @"AllDataTypes.cs",
                @"OneToManyDependent.cs",
                @"OneToManyPrincipal.cs",
                @"OneToOneDependent.cs",
                @"OneToOnePrincipal.cs",
                @"OneToOneSeparateFKDependent.cs",
                @"OneToOneSeparateFKPrincipal.cs",
                @"PropertyConfiguration.cs",
                @"SelfReferencing.cs",
                @"Test_Spaces_Keywords_Table.cs",
            };

        private readonly ITestOutputHelper _output;

        public E2ETests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void E2ETest()
        {
            // set current cultures to English because expected results for error messages
            // (both those output to the Logger and those put in comments in the .cs files)
            // are in English
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");

            var serviceProvider = SetupServiceProvider();
            var logger = new InMemoryCommandLogger("E2ETest");
            serviceProvider.AddService(typeof(ILogger), logger);
            var fileService = new InMemoryFileService();
            serviceProvider.AddService(typeof(IFileService), fileService);

            var designTimeAssembly = Assembly.Load(new AssemblyName("EntityFramework7.SqlServerCompact40.Design"));
            var type = designTimeAssembly.GetExportedTypes()
                .First(t => t.FullName == "Microsoft.Data.Entity.SqlServerCompact.Design.ReverseEngineering.SqlCeMetadataModelProvider");
            var provider = (IDatabaseMetadataModelProvider)Activator.CreateInstance(type, serviceProvider);

            var configuration = new ReverseEngineeringConfiguration
            {
                Provider = provider,
                ConnectionString = E2EConnectionString,
                Namespace = TestNamespace,
                OutputPath = TestOutputDir
            };

            var expectedFileContents = InitializeExpectedFileContents();

            var generator = new ReverseEngineeringGenerator(serviceProvider);
            var filePaths = generator.GenerateAsync(configuration).Result;

            //Assert.Equal(_E2ETestExpectedWarnings.Count, logger.WarningMessages.Count);
            //// loop over warnings instead of using the collection form of Assert.Equal()
            //// to give better error messages if it does fail. Similarly for file paths below.
            //var i = 0;
            //foreach (var expectedWarning in _E2ETestExpectedWarnings)
            //{
            //    Assert.Equal(expectedWarning, logger.WarningMessages[i++]);
            //}
            Assert.Equal(0, logger.InformationMessages.Count);
            Assert.Equal(0, logger.VerboseMessages.Count);

            var expectedFilePaths = _E2ETestExpectedFileNames.Select(name => TestOutputDir + @"\" + name);
            Assert.Equal(expectedFilePaths.Count(), filePaths.Count);
            var i = 0;
            foreach (var expectedFilePath in expectedFilePaths)
            {
                Assert.Equal(expectedFilePath, filePaths[i++]);
            }

            //TODO ErikEJ Investigate why 
            //entity.Reference(d => d.OneToOneSeparateFKDependentFK).InverseCollection(p => p.OneToOneSeparateFKDependent).ForeignKey(d => new { d.OneToOneSeparateFKDependentFK1, d.OneToOneSeparateFKDependentFK2 });
            //this is wrong compared to SQL Server expected

            //TODO ErikEJ Add defaultvalue (-1) to "withdefaultvalue" column

            var listOfFileContents = new List<string>();
            foreach (var fileName in _E2ETestExpectedFileNames)
            {
                var fileContents = fileService.RetrieveFileContents(TestOutputDir, fileName);
                Assert.Equal(expectedFileContents[fileName], fileContents);
                listOfFileContents.Add(fileContents);
            }

            //// compile generated code
            //var metadataReferencesProvider =
            //    (MetadataReferencesProvider)serviceProvider.GetService(typeof(MetadataReferencesProvider));
            //var metadataReferences = SetupMetadataReferencesForCompilationOfGeneratedCode(metadataReferencesProvider);
            //var roslynCompilationService = new RoslynCompilationService();
            //var compilationResult =
            //    roslynCompilationService.Compile(listOfFileContents, metadataReferences);

            //if (compilationResult.Messages.Any())
            //{
            //    _output.WriteLine("Compilation Errors from compiling generated code");
            //    _output.WriteLine("================================================");
            //    foreach (var message in compilationResult.Messages)
            //    {
            //        _output.WriteLine(message);
            //    }
            //    _output.WriteLine("================================================");
            //    Assert.Equal(string.Empty, "See Compilation Errors in Output.");
            //}
        }

        private ServiceProvider SetupServiceProvider()
        {
            var serviceProvider = new ServiceProvider(null);
            serviceProvider.AddService(typeof(CSharpCodeGeneratorHelper), new CSharpCodeGeneratorHelper());
            serviceProvider.AddService(typeof(ModelUtilities), new ModelUtilities());
            var metadataReferencesProvider = new MetadataReferencesProvider(serviceProvider);
            serviceProvider.AddService(typeof(MetadataReferencesProvider), metadataReferencesProvider);
            var compilationService = new RoslynCompilationService();
            serviceProvider.AddService(typeof(ITemplating), new RazorTemplating(compilationService, metadataReferencesProvider));

            return serviceProvider;
        }

        private Dictionary<string, string> InitializeExpectedFileContents()
        {
            var expectedContents = new Dictionary<string, string>(); ;
            foreach (var fileName in _E2ETestExpectedFileNames)
            {
                expectedContents[fileName] = File.ReadAllText(
                    @"ReverseEngineering\ExpectedResults\E2E\" + fileName.Replace(".cs", ".expected"));
            }

            return expectedContents;
        }

        private List<MetadataReference> SetupMetadataReferencesForCompilationOfGeneratedCode(
            MetadataReferencesProvider metadataReferencesProvider)
        {
            metadataReferencesProvider.AddReferenceFromName("EntityFramework.Core");
            metadataReferencesProvider.AddReferenceFromName("EntityFramework.Relational");
            metadataReferencesProvider.AddReferenceFromName("EntityFramework7.SqlServerCompact40");

            var metadataReferences = metadataReferencesProvider.GetApplicationReferences();
            metadataReferences.Add(MetadataReference.CreateFromFile(
                Assembly.Load(new AssemblyName(
                    "System.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")).Location));

            return metadataReferences;
        }

    }
}
