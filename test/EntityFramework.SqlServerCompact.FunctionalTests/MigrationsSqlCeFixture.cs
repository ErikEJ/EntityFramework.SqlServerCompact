using System;
using System.Data.SqlClient;
using System.Data.SqlServerCe;
using Microsoft.Data.Entity.FunctionalTests;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Framework.DependencyInjection;

namespace Microsoft.Data.Entity.SqlCe.FunctionalTests
{
    public class MigrationsSqlCeFixture : MigrationsFixtureBase
    {
        private readonly DbContextOptions _options;
        private readonly IServiceProvider _serviceProvider;

        public MigrationsSqlCeFixture()
        {
            _serviceProvider = new ServiceCollection()
                .AddEntityFramework()
                .AddSqlCe()
                .ServiceCollection()
                .BuildServiceProvider();

            var connectionStringBuilder = new SqlCeConnectionStringBuilder
            {
                DataSource = nameof(MigrationsSqlCeTest)
            };
            //connectionStringBuilder.ApplyConfiguration();

            var optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseSqlCe(connectionStringBuilder.ConnectionString);
            _options = optionsBuilder.Options;
        }

        public override MigrationsContext CreateContext() => new MigrationsContext(_serviceProvider, _options);
    }
}
