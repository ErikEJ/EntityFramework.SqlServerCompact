using System.Linq;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Microsoft.Extensions.Logging;

namespace Microsoft.EntityFrameworkCore.Internal
{
    public class SqlCeModelValidator : RelationalModelValidator
    {
        public SqlCeModelValidator(
            [NotNull] ModelValidatorDependencies dependencies,
            [NotNull] RelationalModelValidatorDependencies relationalDependencies)
            : base(dependencies, relationalDependencies)
        {
        }

        public override void Validate(IModel model)
        {
            base.Validate(model);

            EnsureNoSchemas(model);
            EnsureNoSequences(model);
        }

        protected virtual void EnsureNoSchemas([NotNull] IModel model)
        {
            foreach (var entityType in model.GetEntityTypes().Where(e => e.SqlCe().Schema != null))
            {
                ShowWarning(SqlCeEventId.SchemaConfiguredWarning,
                    $"The entity type '{entityType.DisplayName()}' is configured to use schema '{entityType.SqlCe().Schema}'.SQL Compact does not support schemas. This configuration will be ignored by the SQL Compact provider.");
            }
        }

        protected virtual void EnsureNoSequences([NotNull] IModel model)
        {
            foreach (var sequence in model.SqlCe().Sequences)
            {
                ShowWarning(SqlCeEventId.SequenceWarning, 
                    $"The model was configured with the database sequence '{sequence.Name}'. SQL Compact does not support sequences.");
            }
        }

        protected virtual void ShowWarning(SqlCeEventId eventId, [NotNull] string message)
        { }
        //TODO Add proper event ids (see SQLite provider)
            //=> Dependencies.Logger.LogWarning(eventId, () => message);
    }
}