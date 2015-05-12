using System.Data.SqlServerCe;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Framework.DependencyInjection;

namespace Microsoft.Data.Entity.Tests
{
    public class SqlServerCeTestHelpers : TestHelpers
    {
        protected SqlServerCeTestHelpers()
        {
        }

        public new static SqlServerCeTestHelpers Instance { get; } = new SqlServerCeTestHelpers();

        protected override EntityFrameworkServicesBuilder AddProviderServices(EntityFrameworkServicesBuilder builder)
        {
            return builder.AddSqlServerCe();
        }

        protected override void UseProviderOptions(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServerCe(new SqlCeConnection("Data Source=DummyDatabase"));
        }
    }
}
