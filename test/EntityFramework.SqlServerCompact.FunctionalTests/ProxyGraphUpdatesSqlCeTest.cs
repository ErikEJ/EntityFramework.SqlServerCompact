using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.TestUtilities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit;

namespace Microsoft.EntityFrameworkCore
{
    public abstract class ProxyGraphUpdatesSqlCeTest
    {
        public abstract class ProxyGraphUpdatesSqlCeTestBase<TFixture> : ProxyGraphUpdatesTestBase<TFixture>
            where TFixture : ProxyGraphUpdatesSqlCeTestBase<TFixture>.ProxyGraphUpdatesSqlCeFixtureBase, new()
        {
            protected ProxyGraphUpdatesSqlCeTestBase(TFixture fixture)
                : base(fixture)
            {
            }

            protected override void UseTransaction(DatabaseFacade facade, IDbContextTransaction transaction)
                => facade.UseTransaction(transaction.GetDbTransaction());

            public abstract class ProxyGraphUpdatesSqlCeFixtureBase : ProxyGraphUpdatesFixtureBase
            {
                public TestSqlLoggerFactory TestSqlLoggerFactory => (TestSqlLoggerFactory)ServiceProvider.GetRequiredService<ILoggerFactory>();
                protected override ITestStoreFactory TestStoreFactory => SqlCeTestStoreFactory.Instance;
            }
        }

        public class LazyLoading : ProxyGraphUpdatesSqlCeTestBase<LazyLoading.ProxyGraphUpdatesWithLazyLoadingSqlCeFixture>
        {
            public LazyLoading(ProxyGraphUpdatesWithLazyLoadingSqlCeFixture fixture)
                : base(fixture)
            {
            }

            [Fact(Skip = "SQLCE limitation")]
            public override void Optional_one_to_one_with_AK_relationships_are_one_to_one()
            {
                base.Optional_one_to_one_with_AK_relationships_are_one_to_one();
            }

            [Fact(Skip = "SQLCE limitation")]
            public override void Optional_one_to_one_relationships_are_one_to_one()
            {
                base.Optional_one_to_one_relationships_are_one_to_one();
            }

            public class ProxyGraphUpdatesWithLazyLoadingSqlCeFixture : ProxyGraphUpdatesSqlCeFixtureBase
            {
                protected override string StoreName { get; } = "ProxyGraphLazyLoadingUpdatesTest";

                public override DbContextOptionsBuilder AddOptions(DbContextOptionsBuilder builder)
                    => base.AddOptions(builder.UseLazyLoadingProxies());

                protected override IServiceCollection AddServices(IServiceCollection serviceCollection)
                    => base.AddServices(serviceCollection.AddEntityFrameworkProxies());
            }
        }
    }
}
