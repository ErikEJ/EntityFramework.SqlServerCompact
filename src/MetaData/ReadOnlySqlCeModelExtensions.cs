using JetBrains.Annotations;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Relational.Metadata;

namespace ErikEJ.Data.Entity.SqlServerCe.Metadata
{
    public class ReadOnlySqlCeModelExtensions : ReadOnlyRelationalModelExtensions, ISqlCeModelExtensions
    {
        protected const string SqlServerCeValueGenerationAnnotation = SqlCeAnnotationNames.Prefix + SqlCeAnnotationNames.ValueGeneration;

        public ReadOnlySqlCeModelExtensions([NotNull] IModel model)
            : base(model)
        {
        }

        public virtual bool? IdentityKeyGeneration
        {
            get
            {
                // TODO: Issue #777: Non-string annotations
                var value = Model[SqlServerCeValueGenerationAnnotation] as string;

                if (value == null)
                {
                    return null;
                }
                else
                {
                    return bool.Parse(value);
                };
            }
        }
    }
}
