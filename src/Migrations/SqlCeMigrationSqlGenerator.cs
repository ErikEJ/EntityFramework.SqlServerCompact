using System;
using System.Collections.Generic;
using ErikEJ.Data.Entity.SqlServerCe.Metadata;
using JetBrains.Annotations;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Relational;
using Microsoft.Data.Entity.Relational.Migrations.Operations;
using Microsoft.Data.Entity.Relational.Migrations.Sql;
using Microsoft.Data.Entity.Utilities;

namespace ErikEJ.Data.Entity.SqlServerCe.Migrations
{
    public class SqlCeMigrationSqlGenerator : MigrationSqlGenerator
    {
        private readonly ISqlGenerator _sql;

        public SqlCeMigrationSqlGenerator(
            [NotNull] ISqlGenerator sqlGenerator)
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

        public override void Generate([NotNull]CreateSequenceOperation operation, [CanBeNull]IModel model, [NotNull]SqlBatchBuilder builder)
        {
            throw new NotImplementedException();
        }

        public override void Generate([NotNull]AlterSequenceOperation operation, [CanBeNull]IModel model, [NotNull]SqlBatchBuilder builder)
        {
            throw new NotImplementedException();
        }

        public override void Generate([NotNull]DropSequenceOperation operation, [CanBeNull]IModel model, [NotNull]SqlBatchBuilder builder)
        {
            throw new NotImplementedException();
        }

        public override void Generate([NotNull]RenameSequenceOperation operation, [CanBeNull]IModel model, [NotNull]SqlBatchBuilder builder)
        {
            throw new NotImplementedException();
        }

        public override void Generate(RenameColumnOperation operation, IModel model, SqlBatchBuilder builder)
        {
            throw new NotImplementedException();
        }

        public override void Generate(RenameIndexOperation operation, IModel model, SqlBatchBuilder builder)
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

        public override IReadOnlyList<SqlBatch> Generate(IReadOnlyList<MigrationOperation> operations, IModel model = null)
        {
            Check.NotNull(operations, nameof(operations));

            var builder = new SqlBatchBuilder();
            foreach (var operation in operations)
            {
                // TODO: Too magic? (Wait for changes in EF7 code base)
                ((dynamic)this).Generate((dynamic)operation, model, builder);
                builder.EndBatch();
            }
            return builder.SqlBatches;
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

            var valueGeneration = (string)annotatable[SqlCeAnnotationNames.Prefix + SqlCeAnnotationNames.ValueGeneration];
            if (valueGeneration == SqlCeAnnotationNames.Identity)
            {
                builder.Append(" IDENTITY");
            }
        }
    }
}
