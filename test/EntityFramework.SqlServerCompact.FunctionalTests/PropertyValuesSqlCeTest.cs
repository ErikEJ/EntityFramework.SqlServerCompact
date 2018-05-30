using Microsoft.EntityFrameworkCore.TestUtilities;

namespace Microsoft.EntityFrameworkCore
{
    public class PropertyValuesSqlCeTest
        : PropertyValuesTestBase<PropertyValuesSqlCeTest.PropertyValuesSqlCeFixture>
    {
        public PropertyValuesSqlCeTest(PropertyValuesSqlCeFixture fixture)
            : base(fixture)
        {
        }

        public class PropertyValuesSqlCeFixture : PropertyValuesFixtureBase
        {
            protected override ITestStoreFactory TestStoreFactory => SqlCeTestStoreFactory.Instance;

            protected override void OnModelCreating(ModelBuilder modelBuilder, DbContext context)
            {
                base.OnModelCreating(modelBuilder, context);

                modelBuilder.Entity<Building>()
                    .Property(b => b.Value).HasColumnType("decimal(18,2)");

                modelBuilder.Entity<CurrentEmployee>()
                    .Property(ce => ce.LeaveBalance).HasColumnType("decimal(18,2)");
            }
        }
    }
}
