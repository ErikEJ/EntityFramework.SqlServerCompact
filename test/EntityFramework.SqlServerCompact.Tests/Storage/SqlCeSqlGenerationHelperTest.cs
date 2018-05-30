using System;
using EFCore.SqlCe.Storage.Internal;
using Xunit;

namespace Microsoft.EntityFrameworkCore.Storage
{
    public class SqlCeSqlGenerationHelperTest : SqlGenerationHelperTestBase
    {
        public override void BatchSeparator_returns_separator()
        {
            Assert.Equal("GO" + Environment.NewLine + Environment.NewLine, CreateSqlGenerationHelper().BatchTerminator);
        }

        protected override ISqlGenerationHelper CreateSqlGenerationHelper()
            => new SqlCeSqlGenerationHelper(new RelationalSqlGenerationHelperDependencies());
    }
}
