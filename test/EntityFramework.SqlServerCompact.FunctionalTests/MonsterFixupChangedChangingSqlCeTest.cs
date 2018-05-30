using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.TestUtilities;

namespace Microsoft.EntityFrameworkCore
{
    public class MonsterFixupChangedChangingSqlCeTest :
        MonsterFixupTestBase<MonsterFixupChangedChangingSqlCeTest.MonsterFixupChangedChangingSqlCeFixture>
    {
        public MonsterFixupChangedChangingSqlCeTest(MonsterFixupChangedChangingSqlCeFixture fixture)
            : base(fixture)
        {
        }

        public class MonsterFixupChangedChangingSqlCeFixture : MonsterFixupChangedChangingFixtureBase
        {
            protected override ITestStoreFactory TestStoreFactory => SqlCeTestStoreFactory.Instance;

            public override DbContextOptionsBuilder AddOptions(DbContextOptionsBuilder builder)
                => base.AddOptions(builder).ConfigureWarnings(w => w.Log(RelationalEventId.QueryClientEvaluationWarning));

            protected override void OnModelCreating<TMessage, TProduct, TProductPhoto, TProductReview, TComputerDetail, TDimensions>(
                ModelBuilder builder)
            {
                base.OnModelCreating<TMessage, TProduct, TProductPhoto, TProductReview, TComputerDetail, TDimensions>(builder);

                builder.Entity<TMessage>().HasKey(e => e.MessageId);

                builder.Entity<TProduct>()
                    .OwnsOne(c => (TDimensions)c.Dimensions, db =>
                    {
                        db.Property(d => d.Depth).HasColumnType("decimal(18,2)");
                        db.Property(d => d.Width).HasColumnType("decimal(18,2)");
                        db.Property(d => d.Height).HasColumnType("decimal(18,2)");
                    });

                builder.Entity<TProductPhoto>().HasKey(e => e.PhotoId);
                builder.Entity<TProductReview>().HasKey(e => e.ReviewId);

                builder.Entity<TComputerDetail>()
                    .OwnsOne(c => (TDimensions)c.Dimensions, db =>
                    {
                        db.Property(d => d.Depth).HasColumnType("decimal(18,2)");
                        db.Property(d => d.Width).HasColumnType("decimal(18,2)");
                        db.Property(d => d.Height).HasColumnType("decimal(18,2)");
                    });
            }
        }
    }
}
