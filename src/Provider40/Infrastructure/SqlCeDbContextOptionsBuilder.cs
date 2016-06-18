using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;

namespace Microsoft.EntityFrameworkCore.Infrastructure
{
    /// <summary>
    ///     <para>
    ///         Allows SQL Server Compact specific configuration to be performed on <see cref="DbContextOptions"/>.
    ///     </para>
    ///     <para>
    ///         Instances of this class are returned from a call to 
    ///         <see cref="SqlCeDbContextOptionsExtensions.UseSqlCe(DbContextOptionsBuilder, string, System.Action{SqlCeDbContextOptionsBuilder})"/>
    ///         and it is not designed to be directly constructed in your application code.
    ///     </para>
    /// </summary>
    public class SqlCeDbContextOptionsBuilder : RelationalDbContextOptionsBuilder<SqlCeDbContextOptionsBuilder, SqlCeOptionsExtension>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="SqlCeDbContextOptionsBuilder"/> class.
        /// </summary>
        /// <param name="optionsBuilder"> The options builder. </param>
        public SqlCeDbContextOptionsBuilder([NotNull] DbContextOptionsBuilder optionsBuilder)
            : base(optionsBuilder)
        {
        }

        /// <summary>
        ///     Clones the configuration in this builder.
        /// </summary>
        /// <returns> The cloned configuration. </returns>
        protected override SqlCeOptionsExtension CloneExtension()
            => new SqlCeOptionsExtension(OptionsBuilder.Options.GetExtension<SqlCeOptionsExtension>());
    }
}