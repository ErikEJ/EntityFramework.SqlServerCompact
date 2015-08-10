using System;
using System.Collections.Generic;

namespace Microsoft.Data.Entity.SqlServerCompact.Design.ReverseEngineering.Utilities
{
    public static class SqlCeTypeMapping
    {
        public static readonly Dictionary<string, Type> _sqlTypeToClrTypeMap
            = new Dictionary<string, Type>
            {
                // exact numerics
                { "bigint", typeof(long) },
                { "bit", typeof(bool) },
                { "decimal", typeof(decimal) },
                { "int", typeof(int) },
                { "money", typeof(decimal) },
                { "numeric", typeof(decimal) },
                { "smallint", typeof(short) },
                { "tinyint", typeof(byte) },

                // approximate numerics
                { "real", typeof(float) },
                { "float", typeof(double) },

                // date and time
                { "datetime", typeof(DateTime) },

                // unicode character strings
                { "nchar", typeof(string) },
                { "ntext", typeof(string) },
                { "nvarchar", typeof(string) },

                // binary
                { "binary", typeof(byte[]) },
                { "image", typeof(byte[]) },
                { "varbinary", typeof(byte[]) },

                //other
                { "rowversion", typeof(byte[]) },
                { "uniqueidentifier", typeof(Guid) }
            };
    }
}
