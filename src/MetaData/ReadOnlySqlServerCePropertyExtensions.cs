using JetBrains.Annotations;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Relational.Metadata;
using Microsoft.Data.Entity;

namespace ErikEJ.Data.Entity.SqlServerCe.Metadata
{
    public class ReadOnlySqlServerCePropertyExtensions : ReadOnlyRelationalPropertyExtensions, ISqlServerCePropertyExtensions
    {
        //protected const string SqlServerNameAnnotation = SqlServerAnnotationNames.Prefix + RelationalAnnotationNames.ColumnName;
        //protected const string SqlServerColumnTypeAnnotation = SqlServerAnnotationNames.Prefix + RelationalAnnotationNames.ColumnType;
        //protected const string SqlServerDefaultExpressionAnnotation = SqlServerAnnotationNames.Prefix + RelationalAnnotationNames.ColumnDefaultExpression;
        protected const string SqlServerCeValueGenerationAnnotation = SqlServerCeAnnotationNames.Prefix + SqlServerCeAnnotationNames.ValueGeneration;
        //protected const string SqlServerDefaultValueAnnotation = SqlServerAnnotationNames.Prefix + RelationalAnnotationNames.ColumnDefaultValue;
        //protected const string SqlServerDefaultValueTypeAnnotation = SqlServerAnnotationNames.Prefix + RelationalAnnotationNames.ColumnDefaultValueType;

        public ReadOnlySqlServerCePropertyExtensions([NotNull] IProperty property)
            : base(property)
        {
        }

        public virtual bool? IdentityKeyGeneration
        {
            get
            {
                // TODO: Issue #777: Non-string annotations
                var value = Property[SqlServerCeValueGenerationAnnotation] as string;

                if (value == null && Property.StoreGeneratedPattern == StoreGeneratedPattern.Identity)
                {
                    return Property.EntityType.Model.SqlCe().IdentityKeyGeneration;
                }
                if (value == null)
                {
                    return null;
                }
                else
                {
                    return bool.Parse(value);
                }
            }
        }
    }
}
