using EFCore.SqlCe.Metadata.Conventions.Internal;
using Microsoft.EntityFrameworkCore.TestModels.ConcurrencyModel;
using Microsoft.EntityFrameworkCore.TestUtilities;

namespace Microsoft.EntityFrameworkCore
{
    public class F1SqlCeFixture : F1RelationalFixture
    {
        protected override ITestStoreFactory TestStoreFactory => SqlCeTestStoreFactory.Instance;

        public override ModelBuilder CreateModelBuilder()
            => new ModelBuilder(SqlCeConventionSetBuilder.Build());

        protected override void BuildModelExternal(ModelBuilder modelBuilder)
        {
            base.BuildModelExternal(modelBuilder);

            modelBuilder.Entity<Chassis>().Property<byte[]>("Version").IsRowVersion();
            modelBuilder.Entity<Driver>().Property<byte[]>("Version").IsRowVersion();

            modelBuilder.Entity<Team>().Property<byte[]>("Version")
                .ValueGeneratedOnAddOrUpdate()
                .IsConcurrencyToken();

            modelBuilder.Entity<TitleSponsor>()
                .OwnsOne(s => s.Details)
                .Property(d => d.Space).HasColumnType("decimal(18,2)");
        }
    }
}
