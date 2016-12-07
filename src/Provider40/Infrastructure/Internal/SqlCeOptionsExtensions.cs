using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Utilities;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.EntityFrameworkCore.Infrastructure.Internal
{
    public class SqlCeOptionsExtension : RelationalOptionsExtension
    {
        private bool? _clientEvalForUnsupportedSqlConstructs;

        public SqlCeOptionsExtension()
        {
        }

        public SqlCeOptionsExtension([NotNull] SqlCeOptionsExtension copyFrom)
            : base(copyFrom)
        {
        }

        public virtual bool? ClientEvalForUnsupportedSqlConstructs
        {
            get { return _clientEvalForUnsupportedSqlConstructs; }
            set { _clientEvalForUnsupportedSqlConstructs = value; }
        }

        public override void ApplyServices(IServiceCollection services)
            => Check.NotNull(services, nameof(services)).AddEntityFrameworkSqlCe();
    }
}
