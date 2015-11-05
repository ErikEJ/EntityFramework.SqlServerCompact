using System.Linq;
using JetBrains.Annotations;

namespace Microsoft.Data.Entity.Scaffolding
{
    internal static class SqlCeTableSelectionSetExtensions
    {
        public static bool Allows(this TableSelectionSet _tableSelectionSet, [NotNull] string tableName)
        {
            if (_tableSelectionSet == null
                || (_tableSelectionSet.Tables.Count == 0))
            {
                return true;
            }

            return _tableSelectionSet.Tables.Contains($"{tableName}")
                || _tableSelectionSet.Tables.Contains($"[{tableName}]");
        }
    }
}
