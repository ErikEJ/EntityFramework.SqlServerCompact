namespace Microsoft.EntityFrameworkCore.TestUtilities
{
    public class SqlCeNorthwindTestStoreFactory : SqlCeTestStoreFactory
    {
        public const string Name = "Northwind";
        public static readonly string NorthwindConnectionString = SqlCeTestStore.CreateConnectionString(Name);
        public new static SqlCeNorthwindTestStoreFactory Instance { get; } = new SqlCeNorthwindTestStoreFactory();

        protected SqlCeNorthwindTestStoreFactory()
        {
        }

        public override TestStore GetOrCreate(string storeName)
            => SqlCeTestStore.GetNorthwindStore();
    }
}
