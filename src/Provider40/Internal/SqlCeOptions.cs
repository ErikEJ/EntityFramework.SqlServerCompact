using System;
using EFCore.SqlCe.Infrastructure.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;

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
                //TODO Use other strings
                throw new InvalidOperationException(
                    CoreStrings.SingletonOptionChanged(
                        nameof(SqlCeDbContextOptionsBuilder.UseClientEvalForUnsupportedSqlConstructs),
                        nameof(DbContextOptionsBuilder.UseInternalServiceProvider)));
            }
        }

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public virtual bool ClientEvalForUnsupportedSqlConstructs { get; private set; }
    }
}
