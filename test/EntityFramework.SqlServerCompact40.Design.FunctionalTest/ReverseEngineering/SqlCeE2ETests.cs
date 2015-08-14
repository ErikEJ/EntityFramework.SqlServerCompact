using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.Data.Entity.Relational.Design.FunctionalTests.ReverseEngineering;
using Microsoft.Data.Entity.Relational.Design.ReverseEngineering;
using Microsoft.Data.Entity.SqlServerCompact.Design.ReverseEngineering;
using Xunit;
using Xunit.Abstractions;

namespace EntityFramework7.SqlServerCompact40.Design.FunctionalTest.ReverseEngineering
{
    public class SqlCeE2ETests : E2ETestBase, IClassFixture<SqlCeE2EFixture>
    {
        protected override string ProviderName => "EntityFramework.SqlServerCompact40.Design";
        protected override IDesignTimeMetadataProviderFactory GetFactory() => new SqlCeDesignTimeMetadataProviderFactory();
        public virtual string TestNamespace => "E2ETest.Namespace";
        public virtual string TestOutputDir => "E2ETest/Output/Dir";
        public virtual string CustomizedTemplateDir => "E2ETest/CustomizedTemplate/Dir";

        public virtual string ProviderDbContextTemplateName
            => ProviderName + "." + ReverseEngineeringGenerator.DbContextTemplateFileName;

        public virtual string ProviderEntityTypeTemplateName
            => ProviderName + "." + ReverseEngineeringGenerator.EntityTypeTemplateFileName;

        private static readonly List<string> _expectedFiles = new List<string>
            {
                @"E2EContext.expected",
                @"AllDataTypes.expected",
                @"OneToManyDependent.expected",
                @"OneToManyPrincipal.expected",
                @"OneToOneDependent.expected",
                @"OneToOnePrincipal.expected",
                @"OneToOneSeparateFKDependent.expected",
                @"OneToOneSeparateFKPrincipal.expected",
                @"PropertyConfiguration.expected",
                @"ReferredToByTableWithUnmappablePrimaryKeyColumn.expected",
                @"SelfReferencing.expected",
                @"Test_Spaces_Keywords_Table.expected",
            };

        private const string _connectionString = @"Data Source=E2E.sdf";

        public SqlCeE2ETests(SqlCeE2EFixture fixture, ITestOutputHelper output)
            : base(output)
        {
        }

        protected override E2ECompiler GetCompiler() => new E2ECompiler
        {
            NamedReferences =
                    {
                        "EntityFramework.Core",
                        "EntityFramework.Relational",
                        "EntityFramework.SqlServerCompact40",
                    },
            References =
                    {
                        MetadataReference.CreateFromFile(
                            Assembly.Load(new AssemblyName(
                                "System.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")).Location)
                    }
        };


        [Fact]
        public void E2ETest()
        {
            var configuration = new ReverseEngineeringConfiguration
            {
                Provider = MetadataModelProvider,
                ConnectionString = _connectionString,
                Namespace = TestNamespace,
                CustomTemplatePath = null, // not used for this test
                OutputPath = TestOutputDir
            };

            var filePaths = Generator.GenerateAsync(configuration).GetAwaiter().GetResult();

            var actualFileSet = new FileSet(InMemoryFiles, TestOutputDir)
            {
                Files = filePaths.Select(Path.GetFileName).ToList()
            };

            var expectedFileSet = new FileSet(new FileSystemFileService(), Path.Combine("ReverseEngineering", "ExpectedResults", "E2E"))
            {
                Files = _expectedFiles
            };

            //int i = 0;
            //foreach (var fileName in actualFileSet.Files)
            //{
            //    var actualContent = InMemoryFiles.RetrieveFileContents(TestOutputDir, fileName);
            //    var expectedContent = expectedFileSet.Contents(i);
            //    Assert.Equal(expectedContent, actualContent);
            //    i++;
            //}

            AssertEqualFileContents(expectedFileSet, actualFileSet);
            AssertCompile(actualFileSet);
        }

        [Fact]
        public void Code_generation_will_use_customized_templates_if_present()
        {
            var configuration = new ReverseEngineeringConfiguration
            {
                Provider = MetadataModelProvider,
                ConnectionString = _connectionString,
                Namespace = TestNamespace,
                CustomTemplatePath = CustomizedTemplateDir,
                OutputPath = TestOutputDir
            };
            InMemoryFiles.OutputFile(CustomizedTemplateDir, ProviderDbContextTemplateName, "DbContext template");
            InMemoryFiles.OutputFile(CustomizedTemplateDir, ProviderEntityTypeTemplateName, "EntityType template");

            var filePaths = Generator.GenerateAsync(configuration).GetAwaiter().GetResult();

            AssertLog(new LoggerMessages
            {
                Info =
                        {
                            "Using custom template " + Path.Combine(CustomizedTemplateDir, ProviderDbContextTemplateName),
                            "Using custom template " + Path.Combine(CustomizedTemplateDir, ProviderEntityTypeTemplateName)
                        }
            });

            foreach (var fileName in filePaths.Select(Path.GetFileName))
            {
                var fileContents = InMemoryFiles.RetrieveFileContents(TestOutputDir, fileName);
                var contents = "E2EContext.cs" == fileName ? "DbContext template" : "EntityType template";
                Assert.Contains(fileName.Replace(".cs", ".expected"), _expectedFiles);
                Assert.Equal(contents, fileContents);
            }
        }

        [Fact]
        public void Can_output_templates_to_be_customized()
        {
            var filePaths = Generator.Customize(MetadataModelProvider, TestOutputDir);

            AssertLog(new LoggerMessages());

            Assert.Collection(filePaths,
                file1 => Assert.Equal(file1, Path.Combine(TestOutputDir, ProviderDbContextTemplateName)),
                file2 => Assert.Equal(file2, Path.Combine(TestOutputDir, ProviderEntityTypeTemplateName)));

            var dbContextTemplateContents = InMemoryFiles.RetrieveFileContents(
                TestOutputDir, ProviderDbContextTemplateName);
            Assert.Equal(MetadataModelProvider.DbContextTemplate, dbContextTemplateContents);

            var entityTypeTemplateContents = InMemoryFiles.RetrieveFileContents(
                TestOutputDir, ProviderEntityTypeTemplateName);
            Assert.Equal(MetadataModelProvider.EntityTypeTemplate, entityTypeTemplateContents);
        }
    }
}
