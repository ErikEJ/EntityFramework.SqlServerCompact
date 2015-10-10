using Microsoft.Extensions.Logging;

namespace Microsoft.Data.Entity.FunctionalTests
{
    // Watch the log in PS with: "tail -f $env:userprofile\.klog\sql.log"
    public class SqlFileLogger : TestFileLogger
    {
        public new static readonly ILogger Instance = new SqlFileLogger();

        private SqlFileLogger()
            : base("sql.log")
        {
        }
    }
}
