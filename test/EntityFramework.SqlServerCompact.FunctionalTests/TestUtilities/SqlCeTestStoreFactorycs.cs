using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.EntityFrameworkCore.TestUtilities
{
    public class SqlCeTestStoreFactory : RelationalTestStoreFactory
    {
        public static SqlCeTestStoreFactory Instance { get; } = new SqlCeTestStoreFactory();

        protected SqlCeTestStoreFactory()
        {
        }

        public override TestStore Create(string storeName)
            => SqlCeTestStore.Create(storeName);

        public override TestStore GetOrCreate(string storeName)
            => SqlCeTestStore.CreateScratch(true);

        public override IServiceCollection AddProviderServices(IServiceCollection serviceCollection)
            => serviceCollection.AddEntityFrameworkSqlCe();
    }
}
