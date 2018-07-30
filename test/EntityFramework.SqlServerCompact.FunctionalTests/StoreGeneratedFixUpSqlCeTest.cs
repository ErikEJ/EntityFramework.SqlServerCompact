using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.TestUtilities;
using Xunit;

// ReSharper disable InconsistentNaming
namespace Microsoft.EntityFrameworkCore
{
    public class StoreGeneratedFixupSqlCeTest : StoreGeneratedFixupRelationalTestBase<StoreGeneratedFixupSqlCeTest.StoreGeneratedFixupSqlCeFixture>
    {
        public StoreGeneratedFixupSqlCeTest(StoreGeneratedFixupSqlCeFixture fixture)
            : base(fixture)
        {
        }

        [Fact(Skip = "ErikEJ Investigate")]
        public override void Add_overlapping_graph_from_game()
        {
            base.Add_overlapping_graph_from_game();
        }

        [Fact(Skip = "ErikEJ Investigate")]
        public override void Add_overlapping_graph_from_item()
        {
            base.Add_overlapping_graph_from_item();
        }

        [Fact(Skip = "ErikEJ Investigate")]
        public override void Add_overlapping_graph_from_level()
        {
            base.Add_overlapping_graph_from_level();
        }

        [Fact(Skip="ErikEJ Investigate")]
        public void Temp_values_can_be_made_permanent()
        {
            using (var context = CreateContext())
            {
                var entry = context.Add(new TestTemp());

                Assert.True(entry.Property(e => e.Id).IsTemporary);
                Assert.False(entry.Property(e => e.NotId).IsTemporary);

                var tempValue = entry.Property(e => e.Id).CurrentValue;

                entry.Property(e => e.Id).IsTemporary = false;

                context.SaveChanges();

                Assert.False(entry.Property(e => e.Id).IsTemporary);
                Assert.Equal(tempValue, entry.Property(e => e.Id).CurrentValue);
            }
        }

        protected override bool EnforcesFKs => true;

        protected override void UseTransaction(DatabaseFacade facade, IDbContextTransaction transaction)
            => facade.UseTransaction(transaction.GetDbTransaction());

        public class StoreGeneratedFixupSqlCeFixture : StoreGeneratedFixupRelationalFixtureBase
        {
            protected override ITestStoreFactory TestStoreFactory => SqlCeTestStoreFactory.Instance;

            protected override void OnModelCreating(ModelBuilder modelBuilder, DbContext context)
            {
                base.OnModelCreating(modelBuilder, context);

                modelBuilder.Entity<TestTemp>().Property(b => b.Id).ValueGeneratedOnAdd();

                modelBuilder.Entity<Game>(b => { b.Property(e => e.Id).ValueGeneratedOnAdd().HasDefaultValueSql("newid()"); });
            }
        }
    }
}
