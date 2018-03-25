using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Microsoft.EntityFrameworkCore.TestUtilities
{
    public static class DbContextOptionsBuilderExtensions
    {
        public static SqlCeDbContextOptionsBuilder ApplyConfiguration(this SqlCeDbContextOptionsBuilder optionsBuilder) => optionsBuilder;
    }
}

