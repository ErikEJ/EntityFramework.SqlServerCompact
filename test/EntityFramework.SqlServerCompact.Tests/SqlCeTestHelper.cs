using System.Data.SqlServerCe;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Framework.DependencyInjection;

namespace Microsoft.Data.Entity.Tests
{
    public class SqlCeTestHelpers : TestHelpers
    {
        protected SqlCeTestHelpers()
        {
        }

        public new static SqlCeTestHelpers Instance { get; } = new SqlCeTestHelpers();

        protected override EntityFrameworkServicesBuilder AddProviderServices(EntityFrameworkServicesBuilder builder)
        {
            return builder.AddSqlCe();
        }

        protected override void UseProviderOptions(EntityOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlCe(new SqlCeConnection("Data Source=DummyDatabase"));
        }
    }
}
