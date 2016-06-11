using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Microsoft.EntityFrameworkCore.Specification.Tests.Utilities
{
    public static class DbContextOptionsBuilderExtensions
    {
        public static SqlCeDbContextOptionsBuilder ApplyConfiguration(this SqlCeDbContextOptionsBuilder optionsBuilder) => optionsBuilder;
    }
}

