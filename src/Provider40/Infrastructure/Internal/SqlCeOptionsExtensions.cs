using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Utilities;
using Microsoft.Extensions.DependencyInjection;
using System.Text;

namespace EFCore.SqlCe.Infrastructure.Internal
{
    public class SqlCeOptionsExtension : RelationalOptionsExtension
    {
        private long? _serviceProviderHash;
        private bool? _clientEvalForUnsupportedSqlConstructs;
        private string _logFragment;

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

        public override long GetServiceProviderHashCode()
        {
            if (_serviceProviderHash == null)
            {
                _serviceProviderHash = (base.GetServiceProviderHashCode() * 397) ^ (_clientEvalForUnsupportedSqlConstructs?.GetHashCode() ?? 0L);
            }

            return _serviceProviderHash.Value;
        }

        public override bool ApplyServices(IServiceCollection services)
        {
            Check.NotNull(services, nameof(services));

            services.AddEntityFrameworkSqlCe();

            return true;
        }

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public override string LogFragment
        {
            get
            {
                if (_logFragment == null)
                {
                    var builder = new StringBuilder();

                    builder.Append(base.LogFragment);

                    if (_clientEvalForUnsupportedSqlConstructs == true)
                    {
                        builder.Append("ClientEvalForUnsupportedSqlConstructs ");
                    }

                    _logFragment = builder.ToString();
                }

                return _logFragment;
            }
        }
    }
}
