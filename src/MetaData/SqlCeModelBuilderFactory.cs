using ErikEJ.Data.Entity.SqlServerCe.MetaData.ModelConventions;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Metadata.Builders;
using Microsoft.Data.Entity.Metadata.ModelConventions;

namespace ErikEJ.Data.Entity.SqlServerCe.Metadata
{
    public class SqlCeModelBuilderFactory : ModelBuilderFactory
    {
        protected override ConventionSet CreateConventionSet()
        {
            var conventions = base.CreateConventionSet();

            //TODO ErikEJ Proper API usage? Would like UseIdentity to be default 
            // - but do I need a top level UseNoValueGeneration then?
            //conventions.ModelConventions.Add(new SqlServerCeIdentityConvention());

            return conventions;
        }

        public override ModelBuilder CreateConventionBuilder(Model model)
        {
            return base.CreateConventionBuilder(model);
        }
    }
}
