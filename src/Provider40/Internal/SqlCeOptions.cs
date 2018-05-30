using System;
using EFCore.SqlCe.Infrastructure.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace EFCore.SqlCe.Internal
{
    /// <summary>
    ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public class SqlCeOptions : ISqlCeOptions
    {
        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public virtual void Initialize(IDbContextOptions options)
        {
            var sqlCeOptions = options.FindExtension<SqlCeOptionsExtension>() ?? new SqlCeOptionsExtension();

            ClientEvalForUnsupportedSqlConstructs = sqlCeOptions.ClientEvalForUnsupportedSqlConstructs ?? false;
        }

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public virtual void Validate(IDbContextOptions options)
        {
            var sqlCeOptions = options.FindExtension<SqlCeOptionsExtension>() ?? new SqlCeOptionsExtension();

            if (ClientEvalForUnsupportedSqlConstructs != (sqlCeOptions.ClientEvalForUnsupportedSqlConstructs ?? false))
            {
                throw new InvalidOperationException(
                    $"A call was made to '{nameof(SqlCeDbContextOptionsBuilder.UseClientEvalForUnsupportedSqlConstructs)}' that changed an option that must be constant within a service provider, but Entity Framework is not building its own internal service provider. Either allow EF to build the service provider by removing the call to '{nameof(DbContextOptionsBuilder.UseInternalServiceProvider)}', or ensure that the configuration for '{nameof(SqlCeDbContextOptionsBuilder.UseClientEvalForUnsupportedSqlConstructs)}' does not change for all uses of a given service provider passed to '{nameof(DbContextOptionsBuilder.UseInternalServiceProvider)}'.");
            }
        }

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public virtual bool ClientEvalForUnsupportedSqlConstructs { get; private set; }
    }
}
