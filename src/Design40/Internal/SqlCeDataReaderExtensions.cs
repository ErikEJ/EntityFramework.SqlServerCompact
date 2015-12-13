using System.Data.SqlServerCe;
using JetBrains.Annotations;

namespace Microsoft.Data.Entity.Scaffolding.Internal
{
    public static class SqlCeDataReaderExtensions
    {
        public static string GetStringOrNull([NotNull] this SqlCeDataReader reader, int ordinal)
        {
            return reader.IsDBNull(ordinal) ? null : reader.GetString(ordinal);
        }
    }
}
