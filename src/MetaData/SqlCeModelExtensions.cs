using JetBrains.Annotations;
using Microsoft.Data.Entity.Metadata;

namespace ErikEJ.Data.Entity.SqlServerCe.Metadata
{
    public class SqlCeModelExtensions : ReadOnlySqlCeModelExtensions
    {
        public SqlCeModelExtensions([NotNull] Model model)
            : base(model)
        {
        }

        [CanBeNull]
        public new virtual bool? IdentityKeyGeneration
        {
            get { return base.IdentityKeyGeneration; }
            [param: CanBeNull]
            set
            {
                // TODO: Issue #777: Non-string annotations
                ((Model)Model)[SqlServerCeValueGenerationAnnotation] = value?.ToString();
            }
        }
    }
}
