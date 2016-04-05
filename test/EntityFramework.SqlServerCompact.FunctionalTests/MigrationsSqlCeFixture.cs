using System;
using System.Data.SqlServerCe;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.EntityFrameworkCore.FunctionalTests
{
    public class MigrationsSqlCeFixture : MigrationsFixtureBase
    {
        private readonly DbContextOptions _options;
        private readonly IServiceProvider _serviceProvider;

        public MigrationsSqlCeFixture()
        {
            _serviceProvider = new ServiceCollection()
                .AddEntityFrameworkSqlCe()
                .BuildServiceProvider();

            var connectionStringBuilder = new SqlCeConnectionStringBuilder
            {
                DataSource = nameof(MigrationsSqlCeTest)
            };
            //connectionStringBuilder.ApplyConfiguration();

            var optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder
                .UseSqlCe(connectionStringBuilder.ConnectionString)
                .UseInternalServiceProvider(_serviceProvider);
            _options = optionsBuilder.Options;
        }

        public override MigrationsContext CreateContext() => new MigrationsContext(_options);

        public override EmptyMigrationsContext CreateEmptyContext() => new EmptyMigrationsContext(_options);
    }
}
