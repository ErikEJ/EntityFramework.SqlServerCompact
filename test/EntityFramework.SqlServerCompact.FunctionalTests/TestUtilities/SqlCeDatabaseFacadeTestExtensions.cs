using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Microsoft.EntityFrameworkCore.TestUtilities
{
    public static class SqlCeDatabaseFacadeTestExtensions
    {
        public static void EnsureClean(this DatabaseFacade databaseFacade)
            => new SqlCeDatabaseCleaner().Clean(databaseFacade);
    }
}

