using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Utilities;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.EntityFrameworkCore.Infrastructure.Internal
{
    public class SqlCeOptionsExtension : RelationalOptionsExtension
    {
        public SqlCeOptionsExtension()
        {
        }

        public SqlCeOptionsExtension([NotNull] SqlCeOptionsExtension copyFrom)
            : base(copyFrom)
        {
        }

        public override void ApplyServices(IServiceCollection services)
            => Check.NotNull(services, nameof(services)).AddEntityFrameworkSqlCe();
    }
}
