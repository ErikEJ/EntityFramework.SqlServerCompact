using System;
using System.Linq;

namespace Microsoft.EntityFrameworkCore.Scaffolding
{
    internal static class SqlCeTableSelectionSetExtensions
    {
        public static bool Allows(this TableSelectionSet tableSet, string tableName)
        {
            if ((tableSet == null)
                || (tableSet.Tables.Count == 0))
            {
                return true;
            }

            //TODO: look into performance for large selection sets and numbers of tables
            var result = false;
            var matchingTableSelections = tableSet.Tables.Where(
                t => t.Text.Equals(tableName, StringComparison.OrdinalIgnoreCase)).ToList();
            if (matchingTableSelections.Any())
            {
                matchingTableSelections.ToList().ForEach(selection => selection.IsMatched = true);
                result = true;
            }

            return result;
        }

    }
}
