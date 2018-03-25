using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Microsoft.EntityFrameworkCore.TestUtilities
{
    public class SqlCeTestStoreFactory : ITestStoreFactory
    {
        public static SqlCeTestStoreFactory Instance { get; } = new SqlCeTestStoreFactory();

        protected SqlCeTestStoreFactory()
        {
        }

        public virtual TestStore Create(string storeName)
            => SqlCeTestStore.Create(storeName);

        public virtual TestStore GetOrCreate(string storeName)
            => SqlCeTestStore.CreateScratch(true);

        public virtual IServiceCollection AddProviderServices(IServiceCollection serviceCollection)
            => serviceCollection.AddEntityFrameworkSqlCe()
                .AddSingleton<ILoggerFactory>(new TestSqlLoggerFactory());
    }
}
