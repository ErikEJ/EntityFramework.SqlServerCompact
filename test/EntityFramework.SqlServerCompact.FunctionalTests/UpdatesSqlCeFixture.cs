using Microsoft.EntityFrameworkCore.TestModels.UpdatesModel;
using Microsoft.EntityFrameworkCore.TestUtilities;

namespace Microsoft.EntityFrameworkCore
{
    public class UpdatesSqlCeFixture : UpdatesRelationalFixture
    {
        protected override ITestStoreFactory TestStoreFactory => SqlCeTestStoreFactory.Instance;

        protected override void OnModelCreating(ModelBuilder modelBuilder, DbContext context)
        {
            base.OnModelCreating(modelBuilder, context);

            modelBuilder.Entity<Product>()
                .Property(p => p.Price).HasColumnType("decimal(18,2)");

            modelBuilder.Entity<ProductWithBytes>()
                .Property(p => p.Bytes).HasColumnType("varbinary(8000)");

            modelBuilder.Entity<LoginEntityTypeWithAnExtremelyLongAndOverlyConvolutedNameThatIsUsedToVerifyThatTheStoreIdentifierGenerationLengthLimitIsWorkingCorrectly>()
                .Property(l => l.ProfileId3).HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Profile>()
                    .Property(l => l.Id3).HasColumnType("decimal(18,2)");
        }
    }
}
