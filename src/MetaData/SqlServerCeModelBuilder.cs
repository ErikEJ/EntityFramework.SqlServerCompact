using JetBrains.Annotations;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Utilities;
using Microsoft.Data.Entity;

namespace ErikEJ.Data.Entity.SqlServerCe.Metadata
{
    public class SqlServerCeModelBuilder
    {
        private readonly Model _model;

        public SqlServerCeModelBuilder([NotNull] Model model)
        {
            Check.NotNull(model, nameof(model));

            _model = model;
        }

        public virtual SqlServerCeModelBuilder UseIdentity()
        {
            var extensions = _model.SqlCe();

            extensions.IdentityKeyGeneration = true;

            return this;
        }
    }
}
