using System;
using System.Text;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Utilities;

namespace Microsoft.EntityFrameworkCore.Storage.Internal
{
    public class SqlCeSqlGenerationHelper : RelationalSqlGenerationHelper
    {
        public SqlCeSqlGenerationHelper([NotNull] RelationalSqlGenerationHelperDependencies dependencies)
            : base(dependencies)
        {
        }

        public override string BatchTerminator => "GO" + Environment.NewLine + Environment.NewLine;

        public override string StatementTerminator => Environment.NewLine;

        public override string EscapeIdentifier(string identifier)
            => Check.NotEmpty(identifier, nameof(identifier)).Replace("]", "]]");

        public override string DelimitIdentifier(string identifier)
            => $"[{EscapeIdentifier(Check.NotEmpty(identifier, nameof(identifier)))}]";

        public override void DelimitIdentifier(StringBuilder builder, string identifier)
        {
            Check.NotEmpty(identifier, nameof(identifier));

            builder.Append('[');
            EscapeIdentifier(builder, identifier);
            builder.Append(']');
        }
    }
}
