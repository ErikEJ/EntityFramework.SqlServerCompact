using System;
using EFCore.SqlCe.Infrastructure.Internal;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Xunit;

// ReSharper disable InconsistentNaming
namespace Microsoft.EntityFrameworkCore
{
    public class LoggingSqlCeTest : LoggingRelationalTest<SqlCeDbContextOptionsBuilder, SqlCeOptionsExtension>
    {
        [Fact]
        public void Logs_context_initialization_()
        {
            Assert.Equal(
                ExpectedMessage("ClientEvalForUnsupportedSqlConstructs "),
                ActualMessage(CreateOptionsBuilder(b => ((SqlCeDbContextOptionsBuilder)b).UseClientEvalForUnsupportedSqlConstructs(true))));
        }

        protected override DbContextOptionsBuilder CreateOptionsBuilder(
            Action<RelationalDbContextOptionsBuilder<SqlCeDbContextOptionsBuilder, SqlCeOptionsExtension>> relationalAction)
            => new DbContextOptionsBuilder().UseSqlCe("Data Source=LoggingSqlServerTest.db", relationalAction);

        protected override string ProviderName => "EntityFrameworkCore.SqlServerCompact40";
    }
}
