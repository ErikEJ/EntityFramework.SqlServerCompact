using System;
using System.Data.SqlServerCe;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.TestModels;

namespace Microsoft.EntityFrameworkCore.Specification.Tests
{
    public class MonsterFixupSqlCeTest : MonsterFixupTestBase
    {
        protected override IServiceProvider CreateServiceProvider(bool throwingStateManager = false)
        {
            var serviceCollection = new ServiceCollection()
                .AddEntityFrameworkSqlCe();

            if (throwingStateManager)
            {
                serviceCollection.AddScoped<IStateManager, ThrowingMonsterStateManager>();
            }

            return serviceCollection.BuildServiceProvider();
        }

        protected override DbContextOptions CreateOptions(string databaseName)
        {
            var optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder
                .UseSqlCe(CreateConnectionString(databaseName));

            return optionsBuilder.Options;
        }

        private static string CreateConnectionString(string name)
        {
            return new SqlCeConnectionStringBuilder
            {
                DataSource = name + ".sdf"
            }.ConnectionString;
        }

        private SqlCeTestStore _testStore;

        protected override void CreateAndSeedDatabase(string databaseName, Func<MonsterContext> createContext, Action<MonsterContext> seed)
        {
            _testStore = SqlCeTestStore.GetOrCreateShared(databaseName, () =>
            {
                using (var context = createContext())
                {
                    context.Database.EnsureCreated();
                    seed(context);
                }
            });
        }

        public virtual void Dispose() => _testStore?.Dispose();

        protected override void OnModelCreating<TMessage, TProductPhoto, TProductReview>(ModelBuilder builder)
        {
            base.OnModelCreating<TMessage, TProductPhoto, TProductReview>(builder);

            builder.Entity<TMessage>().HasKey(e => e.MessageId);
            builder.Entity<TProductPhoto>().HasKey(e => e.PhotoId);
            builder.Entity<TProductReview>().HasKey(e => e.ReviewId);
        }
    }
}
