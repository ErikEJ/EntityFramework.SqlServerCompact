using System.Data.SqlServerCe;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.EntityFrameworkCore.Tests
{
    public class SqlCeTestHelpers : RelationalTestHelpers
    {
        protected SqlCeTestHelpers()
        {
        }

        public new static SqlCeTestHelpers Instance { get; } = new SqlCeTestHelpers();

        public override IServiceCollection AddProviderServices(IServiceCollection services) 
            => services.AddEntityFrameworkSqlCe();

        protected override void UseProviderOptions(DbContextOptionsBuilder optionsBuilder) 
            => optionsBuilder.UseSqlCe(new SqlCeConnection("Data Source=DummyDatabase"));
    }
}
