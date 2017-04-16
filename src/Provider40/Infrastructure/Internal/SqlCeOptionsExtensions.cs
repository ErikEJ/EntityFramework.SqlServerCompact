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
            _clientEvalForUnsupportedSqlConstructs = copyFrom._clientEvalForUnsupportedSqlConstructs;
        }

        protected override RelationalOptionsExtension Clone()
            => new SqlCeOptionsExtension(this);

        public virtual bool? ClientEvalForUnsupportedSqlConstructs 
            => _clientEvalForUnsupportedSqlConstructs;

        public virtual SqlCeOptionsExtension WithClientEvalForUnsupportedSqlConstructs(bool clientEvalForUnsupportedSqlConstructs)
        {
            var clone = (SqlCeOptionsExtension)Clone();
            clone._clientEvalForUnsupportedSqlConstructs = clientEvalForUnsupportedSqlConstructs;
            return clone;
        }

        public override bool ApplyServices(IServiceCollection services)
        {
            Check.NotNull(services, nameof(services));

            services.AddEntityFrameworkSqlCe();

            return true;
        }
    }
}
