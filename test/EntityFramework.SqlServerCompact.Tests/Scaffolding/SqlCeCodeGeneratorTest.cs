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

            Assert.Equal("UseSqlCe", codeGenerator.UseProviderMethod);
        }
    }
}
