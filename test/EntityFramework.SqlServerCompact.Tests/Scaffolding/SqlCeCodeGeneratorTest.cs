using EFCore.SqlCe.Scaffolding.Internal;
using Xunit;

namespace Microsoft.EntityFrameworkCore.Scaffolding
{
    public class SqlCeCodeGeneratorTest
    {
        [Fact]
        public virtual void Use_provider_method_is_generated_correctly()
        {
            var codeGenerator = new SqlCeCodeGenerator(new ProviderCodeGeneratorDependencies());

            var result = codeGenerator.GenerateUseProvider("Data Source=Test");

            Assert.Equal("UseSqlCe", result.Method);
            Assert.Collection(
                result.Arguments,
                a => Assert.Equal("Data Source=Test", a));
        }
    }
}
