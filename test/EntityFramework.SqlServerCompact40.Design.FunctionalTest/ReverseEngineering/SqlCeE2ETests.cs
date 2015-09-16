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
                                "System.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")).Location),
                        MetadataReference.CreateFromFile(
                            Assembly.Load(new AssemblyName(
                                "System.ComponentModel.DataAnnotations, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")).Location),
                    }
        };

        private const string _connectionString = @"Data Source=E2E.sdf";

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

        public SqlCeE2ETests(SqlCeE2EFixture fixture, ITestOutputHelper output)
            : base(output)
        {
        }

        [Fact]
        public void E2ETest_UseAttributesInsteadOfFluentApi()
        {
            var configuration = new ReverseEngineeringConfiguration
            {
                ConnectionString = _connectionString,
                CustomTemplatePath = null, // not used for this test
                ProjectPath = TestProjectDir,
                ProjectRootNamespace = TestNamespace,
                RelativeOutputPath = TestSubDir
            };

            var filePaths = Generator.GenerateAsync(configuration).GetAwaiter().GetResult();

            var actualFileSet = new FileSet(InMemoryFiles, Path.Combine(TestProjectDir, TestSubDir))
            {
                Files = Enumerable.Repeat(filePaths.ContextFile, 1).Concat(filePaths.EntityTypeFiles).Select(Path.GetFileName).ToList()
            };

            var expectedFileSet = new FileSet(new FileSystemFileService(),
                Path.Combine("ReverseEngineering", "ExpectedResults", "E2E_UseAttributesInsteadOfFluentApi"),
                contents => contents.Replace("namespace " + TestNamespace, "namespace " + TestNamespace + "." + TestSubDir))
            {
                Files = _expectedFiles
            };

            AssertEqualFileContents(expectedFileSet, actualFileSet);
            AssertCompile(actualFileSet);
        }

        [Fact]
        public void E2ETest_AllFluentApi()
        {
            var configuration = new ReverseEngineeringConfiguration
            {
                ConnectionString = _connectionString,
                ProjectPath = TestProjectDir,
                ProjectRootNamespace = TestNamespace,
                RelativeOutputPath = null, // not used for this test
                UseFluentApiOnly = true
            };

            var filePaths = Generator.GenerateAsync(configuration).GetAwaiter().GetResult();

            var actualFileSet = new FileSet(InMemoryFiles, TestProjectDir)
            {
                Files = Enumerable.Repeat(filePaths.ContextFile, 1).Concat(filePaths.EntityTypeFiles).Select(Path.GetFileName).ToList()
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

            AssertEqualFileContents(expectedFileSet, actualFileSet);
            AssertCompile(actualFileSet);
        }
    }
}
