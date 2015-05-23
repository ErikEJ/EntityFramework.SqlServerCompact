using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.FunctionalTests;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Internal;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Metadata.Builders;
using Microsoft.Framework.DependencyInjection;

namespace ErikEJ.Data.Entity.SqlServerCe.FunctionalTests
{
    public class TestSqlServerCeModelSource : SqlServerCeModelSource
    {
        private readonly TestModelSource _testModelSource;

        public TestSqlServerCeModelSource(Action<ModelBuilder> onModelCreating, IDbSetFinder setFinder, IModelValidator modelValidator)
            : base(setFinder, modelValidator)
        {
            _testModelSource = new TestModelSource(onModelCreating, setFinder);
        }

        public static Func<IServiceProvider, SqlServerCeModelSource> GetFactory(Action<ModelBuilder> onModelCreating) =>
            p => new TestSqlServerCeModelSource(
                onModelCreating,
                p.GetRequiredService<IDbSetFinder>(),
                p.GetRequiredService<IModelValidator>());

        public override IModel GetModel(DbContext context, IModelBuilderFactory modelBuilderFactory) =>
            _testModelSource.GetModel(context, modelBuilderFactory);
    }
}
