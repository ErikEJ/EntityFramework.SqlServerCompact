using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.FunctionalTests;
using Microsoft.Data.Entity.Internal;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Metadata.Builders;
using Microsoft.Framework.DependencyInjection;

namespace ErikEJ.Data.Entity.SqlServerCe.FunctionalTests
{
    public class TestSqlCeModelSource : SqlCeModelSource
    {
        private readonly TestModelSource _testModelSource;

        public TestSqlCeModelSource(Action<ModelBuilder> onModelCreating, IDbSetFinder setFinder)
            : base(setFinder)
        {
            _testModelSource = new TestModelSource(onModelCreating, setFinder);
        }

        public static Func<IServiceProvider, SqlCeModelSource> GetFactory(Action<ModelBuilder> onModelCreating) =>
            p => new TestSqlCeModelSource(
                onModelCreating,
                p.GetRequiredService<IDbSetFinder>());

        public override IModel GetModel(DbContext context, IModelBuilderFactory modelBuilderFactory, IModelValidator validator) =>
            _testModelSource.GetModel(context, modelBuilderFactory, validator);
    }
}
