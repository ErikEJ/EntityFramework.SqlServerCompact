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
        public virtual string TestProjectDir => Path.Combine("E2ETest", "Output");
        public virtual string TestSubDir => "SubDir";

        public virtual string CustomizedTemplateDir => "E2ETest/CustomizedTemplate/Dir";

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

        //[Fact]
        //public void E2ETest_UseAttributesInsteadOfFluentApi()
        //{
        //    var configuration = new ReverseEngineeringConfiguration
        //    {
        //        ConnectionString = _connectionString,
        //        CustomTemplatePath = null, // not used for this test
        //        ProjectPath = TestProjectDir,
        //        ProjectRootNamespace = TestNamespace,
        //        RelativeOutputPath = TestSubDir
        //    };

        //    var filePaths = Generator.GenerateAsync(configuration).GetAwaiter().GetResult();

        //    var actualFileSet = new FileSet(InMemoryFiles, Path.Combine(TestProjectDir, TestSubDir))
        //    {
        //        Files = filePaths.Select(Path.GetFileName).ToList()
        //    };

        //    var expectedFileSet = new FileSet(new FileSystemFileService(),
        //        Path.Combine("ReverseEngineering", "ExpectedResults", "E2E_UseAttributesInsteadOfFluentApi"),
        //        contents => contents.Replace("namespace " + TestNamespace, "namespace " + TestNamespace + "." + TestSubDir))
        //    {
        //        Files = _expectedFiles
        //    };

        //    AssertEqualFileContents(expectedFileSet, actualFileSet);
        //    AssertCompile(actualFileSet);
        //}

        [Fact]
        public void E2ETest_AllFluentApi()
        {
            var configuration = new ReverseEngineeringConfiguration
            {
                ConnectionString = _connectionString,
                CustomTemplatePath = "AllFluentApiTemplatesDir",
                ProjectPath = TestProjectDir,
                ProjectRootNamespace = TestNamespace,
                RelativeOutputPath = null // not used for this test
            };

            // use templates where the flag to use attributes instead of fluent API has been turned off
            var dbContextTemplate = MetadataModelProvider.DbContextTemplate
                .Replace("useAttributesOverFluentApi = true", "useAttributesOverFluentApi = false");
            var entityTypeTemplate = MetadataModelProvider.EntityTypeTemplate
                .Replace("useAttributesOverFluentApi = true", "useAttributesOverFluentApi = false");
            InMemoryFiles.OutputFile("AllFluentApiTemplatesDir", ProviderDbContextTemplateName, dbContextTemplate);
            InMemoryFiles.OutputFile("AllFluentApiTemplatesDir", ProviderEntityTypeTemplateName, entityTypeTemplate);

            var filePaths = Generator.GenerateAsync(configuration).GetAwaiter().GetResult();

            var actualFileSet = new FileSet(InMemoryFiles, TestProjectDir)
            {
                Files = filePaths.Select(Path.GetFileName).ToList()
            };

            var expectedFileSet = new FileSet(new FileSystemFileService(),
                Path.Combine("ReverseEngineering", "ExpectedResults", "E2E_AllFluentApi"))
            {
                Files = _expectedFiles
            };

            //int i = 0;
            //foreach (var fileName in actualFileSet.Files)
            //{
            //    var actualContent = InMemoryFiles.RetrieveFileContents(Path.Combine(TestProjectDir, TestSubDir), fileName);
            //    var expectedContent = expectedFileSet.Contents(i);
            //    Assert.Equal(expectedContent, actualContent);
            //    i++;
            //}

            AssertLog(new LoggerMessages
            {
                Info =
                        {
                            "Using custom template " + Path.Combine("AllFluentApiTemplatesDir", ProviderDbContextTemplateName),
                            "Using custom template " + Path.Combine("AllFluentApiTemplatesDir", ProviderEntityTypeTemplateName)
                        }
            });
            AssertEqualFileContents(expectedFileSet, actualFileSet);
            AssertCompile(actualFileSet);
        }

        [Fact]
        public void Code_generation_will_use_customized_templates_if_present()
        {
            var configuration = new ReverseEngineeringConfiguration
            {
                ConnectionString = _connectionString,
                CustomTemplatePath = CustomizedTemplateDir,
                ProjectPath = TestProjectDir,
                ProjectRootNamespace = TestNamespace,
                RelativeOutputPath = null // tests outputting to top-level directory
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
                var fileContents = InMemoryFiles.RetrieveFileContents(TestProjectDir, fileName);
                var contents = "E2EContext.cs" == fileName ? "DbContext template" : "EntityType template";
                Assert.Contains(fileName.Replace(".cs", ".expected"), _expectedFiles);
                Assert.Equal(contents, fileContents);
            }
        }

        [Fact]
        public void Can_output_templates_to_be_customized()
        {
            var filePaths = Generator.Customize(TestProjectDir);

            AssertLog(new LoggerMessages());

            Assert.Collection(filePaths,
                file1 => Assert.Equal(file1, Path.Combine(TestProjectDir, ProviderDbContextTemplateName)),
                file2 => Assert.Equal(file2, Path.Combine(TestProjectDir, ProviderEntityTypeTemplateName)));

            var dbContextTemplateContents = InMemoryFiles.RetrieveFileContents(
                TestProjectDir, ProviderDbContextTemplateName);
            Assert.Equal(MetadataModelProvider.DbContextTemplate, dbContextTemplateContents);

            var entityTypeTemplateContents = InMemoryFiles.RetrieveFileContents(
                TestProjectDir, ProviderEntityTypeTemplateName);
            Assert.Equal(MetadataModelProvider.EntityTypeTemplate, entityTypeTemplateContents);
        }
    }
}
