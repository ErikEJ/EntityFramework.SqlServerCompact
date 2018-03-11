using Microsoft.EntityFrameworkCore.Infrastructure;

namespace EFCore.SqlCe.Infrastructure.Internal
{
    /// <summary>
    ///     Options set at the <see cref="IServiceProvider" /> singleton level to control SqlServer specific options.
    /// </summary>
    public interface ISqlCeOptions : ISingletonOptions
    {
        /// <summary>
        ///     Reflects the option set by <see cref="SqlCeDbContextOptionsBuilder.UseClientEvalForUnsupportedSqlConstructs" />.
        /// </summary>
        bool ClientEvalForUnsupportedSqlConstructs { get; }
    }
}
