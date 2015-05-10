using System;
using ErikEJ.Data.Entity.SqlServerCe.Metadata;
using JetBrains.Annotations;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Relational;
using Microsoft.Data.Entity.Relational.Migrations.Operations;
using Microsoft.Data.Entity.Relational.Migrations.Sql;
using Microsoft.Data.Entity.Utilities;

namespace ErikEJ.Data.Entity.SqlServerCe
{
    public class SqlServerCeMigrationSqlGenerator : MigrationSqlGenerator, ISqlServerCeMigrationSqlGenerator
    {
        private readonly ISqlServerCeSqlGenerator _sql;

        public SqlServerCeMigrationSqlGenerator(
            [NotNull] ISqlServerCeSqlGenerator sqlGenerator)
            : base(sqlGenerator)
        {
            _sql = sqlGenerator;
        }

        public override void Generate(DropIndexOperation operation, IModel model, SqlBatchBuilder builder)
        {
            Check.NotNull(operation, nameof(operation));
            Check.NotNull(builder, nameof(builder));

            builder
                .EndBatch()
                .Append("DROP INDEX ")
                .Append(_sql.DelimitIdentifier(operation.Name));
        }

        public override void Generate(RenameSequenceOperation operation, IModel model, SqlBatchBuilder builder)
        {
            throw new NotImplementedException();
        }

        public override void Generate(RenameColumnOperation operation, IModel model, SqlBatchBuilder builder)
        {
            throw new NotImplementedException();
        }

        public override void Generate(RenameTableOperation operation, IModel model, SqlBatchBuilder builder)
        {
            Check.NotNull(operation, nameof(operation));
            Check.NotNull(builder, nameof(builder));

            if (operation.NewName != null)
            {
                builder
                    .EndBatch()
                    .Append("sp_rename '")
                    .Append(operation.Name)
                    .Append("', '")
                    .Append(operation.NewName)
                    .Append("'");
            }
        }

        public override void Generate(RenameIndexOperation operation, IModel model, SqlBatchBuilder builder)
        {
            throw new NotImplementedException();
        }

        public override void Generate([NotNull]CreateTableOperation operation, [CanBeNull]IModel model, [NotNull]SqlBatchBuilder builder)
        {
            builder.EndBatch();
            base.Generate(operation, model, builder);
            
        }

        public override void Generate(
            [NotNull] AddColumnOperation operation,
            [CanBeNull] IModel model,
            [NotNull] SqlBatchBuilder builder)
        {
            Check.NotNull(operation, nameof(operation));
            Check.NotNull(builder, nameof(builder));

            builder
                .EndBatch()
                .Append("ALTER TABLE ")
                .Append(_sql.DelimitIdentifier(operation.Table))
                .Append(" ADD ");
            ColumnDefinition(operation, model, builder);
        }


        public override void Generate(
            [NotNull] AlterColumnOperation operation,
            [CanBeNull] IModel model,
            [NotNull] SqlBatchBuilder builder)
        {
            Check.NotNull(operation, nameof(operation));
            Check.NotNull(builder, nameof(builder));

            // TODO: Test default value/expression
            builder
                .EndBatch()
                .Append("ALTER TABLE ")
                .Append(_sql.DelimitIdentifier(operation.Table))
                .Append(" ALTER COLUMN ");                            
            ColumnDefinition(operation, model, builder);
        }

        public virtual void ColumnDefinition(
            [NotNull] AlterColumnOperation operation,
            [CanBeNull] IModel model,
            [NotNull] SqlBatchBuilder builder) =>
                ColumnDefinition(
                    operation.Schema,
                    operation.Table,
                    operation.Name,
                    operation.Type,
                    operation.IsNullable,
                    operation.DefaultValue,
                    operation.DefaultExpression,
                    operation,
                    model,
                    builder);

        public override void ColumnDefinition(
            string schema,
            string table,
            string name,
            string type,
            bool nullable,
            object defaultValue,
            string defaultExpression,
            IAnnotatable annotatable,
            IModel model,
            SqlBatchBuilder builder)
        {
            Check.NotEmpty(name, nameof(name));
            Check.NotEmpty(type, nameof(type));
            Check.NotNull(annotatable, nameof(annotatable));
            Check.NotNull(builder, nameof(builder));

            base.ColumnDefinition(
                schema,
                table,
                name,
                type,
                nullable,
                defaultValue,
                defaultExpression,
                annotatable,
                model,
                builder);

            var valueGeneration = (string)annotatable[SqlServerCeAnnotationNames.Prefix + SqlServerCeAnnotationNames.ValueGeneration];
            if (valueGeneration == SqlServerCeAnnotationNames.Strategy)
            {
                builder.Append(" IDENTITY");
            }
        }
    }
}
