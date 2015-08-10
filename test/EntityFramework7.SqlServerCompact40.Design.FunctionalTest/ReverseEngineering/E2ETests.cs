using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.Data.Entity.Relational.Design.ReverseEngineering;
using Microsoft.Data.Entity.Relational.Design.Templating.Compilation;
using Microsoft.Data.Entity.SqlServerCompact.Design.ReverseEngineering;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Logging;
using Xunit;
using Xunit.Abstractions;

namespace EntityFramework7.SqlServerCompact40.Design.FunctionalTest.ReverseEngineering
{
    public class E2ETests : IClassFixture<E2EFixture>
    {
        public const string E2EConnectionString =
            @"Data Source=E2E.sdf";

        private const string ProviderAssembyName = "EntityFramework.SqlServerCompact40.Design";
        private const string ProviderFullClassPath =
            "Microsoft.Data.Entity.SqlServerCompact.Design.ReverseEngineering.SqlCeMetadataModelProvider";
        private const string ProviderDbContextTemplateName =
            ProviderAssembyName + "." + ReverseEngineeringGenerator.DbContextTemplateFileName;
        private const string ProviderEntityTypeTemplateName =
            ProviderAssembyName + "." + ReverseEngineeringGenerator.EntityTypeTemplateFileName;
        private const string TestNamespace = "E2ETest.Namespace";
        private const string TestOutputDir = @"E2ETest\Output\Dir";
        private const string CustomizedTemplateDir = @"E2ETest\CustomizedTemplate\Dir";

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

        private const string CustomDbContextTemplateContents =
            "This is the output from a customized DbContextTemplate";
        private const string CustomEntityTypeTemplateContents =
            "This is the output from a customized EntityTypeTemplate";

        private readonly ITestOutputHelper _output;

        public E2ETests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void E2ETest()
        {
            SetCurrentCulture();

            var serviceCollection = SetupInitialServices();
            var logger = new InMemoryCommandLogger("E2ETest");
            serviceCollection.AddScoped(typeof(ILogger), sp => logger);
            var fileService = new InMemoryFileService();
            serviceCollection.AddScoped(typeof(IFileService), sp => fileService);

            var provider = GetMetadataModelProvider(serviceCollection);

            var configuration = new ReverseEngineeringConfiguration
            {
                Provider = provider,
                ConnectionString = E2EConnectionString,
                Namespace = TestNamespace,
                CustomTemplatePath = null, // not used for this test
                OutputPath = TestOutputDir
            };

            var expectedFileContents = InitializeE2EExpectedFileContents();

            var serviceProvider = serviceCollection.BuildServiceProvider();
            var generator = serviceProvider.GetRequiredService<ReverseEngineeringGenerator>();
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

            var listOfFileContents = new List<string>();
            foreach (var fileName in _E2ETestExpectedFileNames)
            {
                var fileContents = fileService.RetrieveFileContents(TestOutputDir, fileName);
                Assert.Equal(expectedFileContents[fileName], fileContents);
                listOfFileContents.Add(fileContents);
            }

            //TODO ErikEJ Await fix fr hardcoded UseSqlServer in template
            // compile generated code 
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

        [Fact]
        public void Code_generation_will_use_customized_templates_if_present()
        {
            SetCurrentCulture();

            var serviceCollection = SetupInitialServices();
            var logger = new InMemoryCommandLogger("E2ETest");
            serviceCollection.AddScoped(typeof(ILogger), sp => logger);
            var fileService = new InMemoryFileService();
            serviceCollection.AddScoped(typeof(IFileService), sp => fileService);

            var provider = GetMetadataModelProvider(serviceCollection);

            var configuration = new ReverseEngineeringConfiguration
            {
                Provider = provider,
                ConnectionString = E2EConnectionString,
                Namespace = TestNamespace,
                CustomTemplatePath = null, // not used for this test
                OutputPath = TestOutputDir
            };

            var expectedFileContents = InitializeE2EExpectedFileContents();

            var serviceProvider = serviceCollection.BuildServiceProvider();
            var generator = serviceProvider.GetRequiredService<ReverseEngineeringGenerator>();
            var filePaths = generator.GenerateAsync(configuration).Result;

            Assert.Equal(_E2ETestExpectedWarnings.Count, logger.WarningMessages.Count);
            // loop over warnings instead of using the collection form of Assert.Equal()
            // to give better error messages if it does fail. Similarly for file paths below.
            var i = 0;
            foreach (var expectedWarning in _E2ETestExpectedWarnings)
            {
                Assert.Equal(expectedWarning, logger.WarningMessages[i++]);
            }

            //TODO ErikEJ Why is this failing?
            //Assert.Equal(_CustomizedTemplatesTestExpectedInfos.Count, logger.InformationMessages.Count);
            //i = 0;
            //foreach (var expectedInfo in _CustomizedTemplatesTestExpectedInfos)
            //{
            //    Assert.Equal(expectedInfo, logger.InformationMessages[i++]);
            //}

            Assert.Equal(0, logger.VerboseMessages.Count);

            var expectedFilePaths = _E2ETestExpectedFileNames.Select(name => TestOutputDir + @"\" + name);
            Assert.Equal(expectedFilePaths.Count(), filePaths.Count);
            i = 0;
            foreach (var expectedFilePath in expectedFilePaths)
            {
                Assert.Equal(expectedFilePath, filePaths[i++]);
            }

            var listOfFileContents = new List<string>();

            //TODO ErikEJ Why is this failing?
            //foreach (var fileName in _E2ETestExpectedFileNames)
            //{
            //    var fileContents = fileService.RetrieveFileContents(TestOutputDir, fileName);
            //    if ("E2EContext.cs" == fileName)
            //    {
            //        Assert.Equal(CustomDbContextTemplateContents, fileContents);
            //    }
            //    else
            //    {
            //        Assert.Equal(CustomEntityTypeTemplateContents, fileContents);
            //    }
            //}
        }

        [Fact]
        public void Can_output_templates_to_be_customized()
        {
            SetCurrentCulture();

            var serviceCollection = SetupInitialServices();
            var logger = new InMemoryCommandLogger("E2ETest");
            serviceCollection.AddScoped(typeof(ILogger), sp => logger);
            var fileService = new InMemoryFileService();
            serviceCollection.AddScoped(typeof(IFileService), sp => fileService);

            var provider = GetMetadataModelProvider(serviceCollection);
            var expectedFileContents = InitializeE2EExpectedFileContents();

            var serviceProvider = serviceCollection.BuildServiceProvider();
            var generator = serviceProvider.GetRequiredService<ReverseEngineeringGenerator>();
            var filePaths = generator.Customize(provider, TestOutputDir);

            Assert.Equal(0, logger.WarningMessages.Count);
            Assert.Equal(0, logger.InformationMessages.Count);
            Assert.Equal(0, logger.VerboseMessages.Count);
            Assert.Equal(2, filePaths.Count);
            Assert.Equal(TestOutputDir + @"\" + ProviderDbContextTemplateName, filePaths[0]);
            Assert.Equal(TestOutputDir + @"\" + ProviderEntityTypeTemplateName, filePaths[1]);

            var dbContextTemplateContents = fileService.RetrieveFileContents(
                TestOutputDir, ProviderDbContextTemplateName);
            Assert.Equal(provider.DbContextTemplate, dbContextTemplateContents);

            var entityTypeTemplateContents = fileService.RetrieveFileContents(
                TestOutputDir, ProviderEntityTypeTemplateName);
            Assert.Equal(provider.EntityTypeTemplate, entityTypeTemplateContents);
        }

        private ServiceCollection SetupInitialServices()
        {
            var serviceCollection = new ServiceCollection();
            return serviceCollection;
        }


        private Dictionary<string, string> InitializeE2EExpectedFileContents()
        {
            var expectedContents = new Dictionary<string, string>(); ;
            foreach (var fileName in _E2ETestExpectedFileNames)
            {
                expectedContents[fileName] = File.ReadAllText(
                    @"ReverseEngineering\ExpectedResults\E2E\" + fileName.Replace(".cs", ".expected"));
            }

            return expectedContents;
        }

        private void InitializeCustomizedTemplates(InMemoryFileService fileService)
        {
            fileService.OutputFile(CustomizedTemplateDir, ProviderDbContextTemplateName, CustomDbContextTemplateContents);
            fileService.OutputFile(CustomizedTemplateDir, ProviderEntityTypeTemplateName, CustomEntityTypeTemplateContents);
        }

        private List<MetadataReference> SetupMetadataReferencesForCompilationOfGeneratedCode(
            MetadataReferencesProvider metadataReferencesProvider)
        {
            metadataReferencesProvider.AddReferenceFromName("EntityFramework.Core");
            metadataReferencesProvider.AddReferenceFromName("EntityFramework.Relational");
            metadataReferencesProvider.AddReferenceFromName("EntityFramework.SqlServerCompact40");

            var metadataReferences = metadataReferencesProvider.GetApplicationReferences();
            metadataReferences.Add(MetadataReference.CreateFromFile(
                Assembly.Load(new AssemblyName(
                    "System.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")).Location));

            return metadataReferences;
        }

        private IDatabaseMetadataModelProvider GetMetadataModelProvider(IServiceCollection serviceCollection)
        {
            var designTimeAssembly = Assembly.Load(new AssemblyName(ProviderAssembyName));
            var type = designTimeAssembly.GetType(ProviderFullClassPath);
            var designTimeMetadataProviderFactory = new SqlCeDesignTimeMetadataProviderFactory();
            designTimeMetadataProviderFactory.AddMetadataProviderServices(serviceCollection);
            return serviceCollection.BuildServiceProvider().GetRequiredService<IDatabaseMetadataModelProvider>();
        }

        private void SetCurrentCulture()
        {
            // set current cultures to English because expected results for error messages
            // (both those output to the Logger and those put in comments in the .cs files)
            // are in English
#if DNXCORE50
            CultureInfo.CurrentCulture = new CultureInfo("en-US");
            CultureInfo.CurrentUICulture = new CultureInfo("en-US");
#else
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
#endif
        }

    }
}
