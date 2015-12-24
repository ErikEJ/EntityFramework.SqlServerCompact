using System;
using System.Linq;
using JetBrains.Annotations;

namespace Microsoft.Data.Entity.Scaffolding
{
    internal static class SqlCeTableSelectionSetExtensions
    {
        public static bool Allows(this TableSelectionSet tableSelectionSet, [NotNull] string tableName)
        {
            if ((tableSelectionSet == null)
                || (tableSelectionSet.Tables.Count == 0))
            {
                return true;
            }

            return tableSelectionSet.Tables.Contains($"{tableName}", StringComparer.OrdinalIgnoreCase)
                || tableSelectionSet.Tables.Contains($"[{tableName}]", StringComparer.OrdinalIgnoreCase);
        }
    }
}
