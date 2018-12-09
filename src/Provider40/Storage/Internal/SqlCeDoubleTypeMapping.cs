using System.Data;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Storage;

namespace EFCore.SqlCe.Storage.Internal
{
    /// <summary>
    ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public class SqlCeDoubleTypeMapping : DoubleTypeMapping
    {
        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public SqlCeDoubleTypeMapping(
            [NotNull] string storeType,
            DbType? dbType = null)
            : base(storeType, dbType)
        {
        }

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        protected SqlCeDoubleTypeMapping(RelationalTypeMappingParameters parameters)
            : base(parameters)
        {
        }

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        protected override RelationalTypeMapping Clone(RelationalTypeMappingParameters parameters)
            => new SqlCeDoubleTypeMapping(parameters);

        //TODO Investigate

        //Test 'Microsoft.EntityFrameworkCore.Query.SimpleQuerySqlCeTest.Average_over_nested_subquery_is_client_eval

        ///// <summary>
        /////     This API supports the Entity Framework Core infrastructure and is not intended to be used
        /////     directly from your code. This API may change or be removed in future releases.
        ///// </summary>
        //protected override string GenerateNonNullSqlLiteral(object value)
        //    => $"CAST({base.GenerateNonNullSqlLiteral(value)} AS {StoreType})";
    }
}
