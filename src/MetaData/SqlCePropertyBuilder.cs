using JetBrains.Annotations;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Utilities;

namespace ErikEJ.Data.Entity.SqlServerCe.Metadata
{
    public class SqlCePropertyBuilder
    {
        private readonly Property _property;

        public SqlCePropertyBuilder([NotNull] Property property)
        {
            Check.NotNull(property, nameof(property));

            _property = property;
        }

        public virtual SqlCePropertyBuilder UseIdentity()
        {
            _property.SqlCe().IdentityKeyGeneration = true;
            _property.StoreGeneratedPattern = StoreGeneratedPattern.Identity;

            return this;
        }

        public virtual SqlCePropertyBuilder UseNoValueGeneration()
        {
            _property.SqlCe().IdentityKeyGeneration = null;
            //TODO Need to set this?
            _property.StoreGeneratedPattern = StoreGeneratedPattern.None;
            return this;
        }
    }
}
