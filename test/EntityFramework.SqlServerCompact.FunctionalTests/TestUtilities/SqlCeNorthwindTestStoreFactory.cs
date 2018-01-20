namespace Microsoft.EntityFrameworkCore.TestUtilities
{
    public class SqlServerNorthwindTestStoreFactory : SqlCeTestStoreFactory
    {
        public const string Name = "Northwind";
        public static readonly string NorthwindConnectionString = SqlCeTestStore.CreateConnectionString(Name);
        public new static SqlServerNorthwindTestStoreFactory Instance { get; } = new SqlServerNorthwindTestStoreFactory();

        protected SqlServerNorthwindTestStoreFactory()
        {
        }

        public override TestStore GetOrCreate(string storeName)
            => SqlCeTestStore.GetNorthwindStore();
    }
}
