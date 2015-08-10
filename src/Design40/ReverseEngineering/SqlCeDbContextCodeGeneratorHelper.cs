using System.Collections.Generic;
using System.Data.SqlServerCe;
using System.IO;
using JetBrains.Annotations;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Metadata.Conventions.Internal;
using Microsoft.Data.Entity.Relational.Design.CodeGeneration;
using Microsoft.Data.Entity.Relational.Design.ReverseEngineering;
using Microsoft.Data.Entity.Relational.Design.ReverseEngineering.Configuration;
using Microsoft.Data.Entity.Utilities;

namespace Microsoft.Data.Entity.SqlServerCompact.Design.ReverseEngineering
{
    public class SqlCeDbContextCodeGeneratorHelper : DbContextCodeGeneratorHelper
    {
        private const string _dbContextSuffix = "Context";
        private KeyConvention _keyConvention = new KeyConvention();

        public SqlCeDbContextCodeGeneratorHelper(
            [NotNull] DbContextGeneratorModel generatorModel,
            [NotNull] IRelationalMetadataExtensionProvider extensionsProvider)
            : base(generatorModel, extensionsProvider)
        {
        }

        public override string ClassName([NotNull] string connectionString)
        {
            Check.NotEmpty(connectionString, nameof(connectionString));

            var path = PathFromConnectionString(connectionString);
            if (File.Exists(path))
            {
                return CSharpUtilities.Instance.GenerateCSharpIdentifier(
                    Path.GetFileNameWithoutExtension(path) + _dbContextSuffix, null);
            }

            return base.ClassName(connectionString);
        }

        public override void AddPropertyFacetsConfiguration([NotNull] PropertyConfiguration propertyConfiguration)
        {
            Check.NotNull(propertyConfiguration, nameof(propertyConfiguration));

            base.AddPropertyFacetsConfiguration(propertyConfiguration);

            AddValueGeneratedNeverFacetConfiguration(propertyConfiguration);
        }

        public virtual void AddValueGeneratedNeverFacetConfiguration(
            [NotNull] PropertyConfiguration propertyConfiguration)
        {
            Check.NotNull(propertyConfiguration, nameof(propertyConfiguration));

            // If the EntityType has a single integer key KeyConvention assumes ValueGeneratedOnAdd().
            // If the underlying column does not have Identity set then we need to set to
            // ValueGeneratedNever() to override this behavior.
            if (_keyConvention.ValueGeneratedOnAddProperty(
                new List<Property> { (Property)propertyConfiguration.Property },
                (EntityType)propertyConfiguration.EntityConfiguration.EntityType) != null)
            {
                propertyConfiguration.AddFacetConfiguration(
                    new FacetConfiguration("ValueGeneratedNever()"));
            }
        }

        private string PathFromConnectionString(string connectionString)
        {
            var conn = new SqlCeConnection(GetFullConnectionString(connectionString));
            return conn.Database;
        }

        private string GetFullConnectionString(string connectionString)
        {
            using (var repl = new SqlCeReplication())
            {
                repl.SubscriberConnectionString = connectionString;
                return repl.SubscriberConnectionString;
            }
        }
    }
}
