using JetBrains.Annotations;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Relational.Metadata;

namespace ErikEJ.Data.Entity.SqlServerCe.Metadata
{
    public class ReadOnlySqlServerCeModelExtensions : ReadOnlyRelationalModelExtensions, ISqlServerCeModelExtensions
    {
        protected const string SqlServerCeValueGenerationAnnotation = SqlServerCeAnnotationNames.Prefix + SqlServerCeAnnotationNames.ValueGeneration;

        public ReadOnlySqlServerCeModelExtensions([NotNull] IModel model)
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
