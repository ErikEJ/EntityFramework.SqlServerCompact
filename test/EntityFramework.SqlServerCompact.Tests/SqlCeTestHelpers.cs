using System.Data.SqlServerCe;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.Specification.Tests;

namespace Microsoft.EntityFrameworkCore.Tests
{
    public class SqlCeTestHelpers : TestHelpers
    {
        protected SqlCeTestHelpers()
        {
        }

        public static SqlCeTestHelpers Instance { get; } = new SqlCeTestHelpers();

        public override IServiceCollection AddProviderServices(IServiceCollection services) 
            => services.AddEntityFrameworkSqlCe();

        protected override void UseProviderOptions(DbContextOptionsBuilder optionsBuilder) 
            => optionsBuilder.UseSqlCe(new SqlCeConnection("Data Source=DummyDatabase"));
    }
}
