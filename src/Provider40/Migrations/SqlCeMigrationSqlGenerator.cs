using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations.Operations;
using Microsoft.Data.Entity.Migrations.Sql;
using Microsoft.Data.Entity.SqlServerCompact.MetaData;
using Microsoft.Data.Entity.Utilities;

namespace Microsoft.Data.Entity.SqlServerCompact.Migrations
{
    public class SqlCeMigrationSqlGenerator : MigrationSqlGenerator
    {
        private readonly IUpdateSqlGenerator _sql;

        public SqlCeMigrationSqlGenerator(
            [NotNull] IUpdateSqlGenerator sqlGenerator)
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

        public override void Generate(CreateSchemaOperation operation, IModel model, SqlBatchBuilder builder)
        {
            throw new NotSupportedException("SQL Server Compact does not support schemas.");
        }

        public override void Generate(DropSchemaOperation operation, IModel model, SqlBatchBuilder builder)
        {
            throw new NotSupportedException("SQL Server Compact does not support schemas.");
        }

        public override void Generate(CreateSequenceOperation operation, IModel model, SqlBatchBuilder builder)
        {
            throw new NotSupportedException("SQL Server Compact does not support sequences.");
        }

        public override void Generate(AlterSequenceOperation operation, IModel model, SqlBatchBuilder builder)
        {
            throw new NotSupportedException("SQL Server Compact does not support sequences.");
        }

        public override void Generate(DropSequenceOperation operation, IModel model, SqlBatchBuilder builder)
        {
            throw new NotSupportedException("SQL Server Compact does not support sequences.");
        }

        public override void Generate(RenameSequenceOperation operation, IModel model, SqlBatchBuilder builder)
        {
            throw new NotSupportedException("SQL Server Compact does not support sequences.");
        }

        public override void Generate(RenameColumnOperation operation, IModel model, SqlBatchBuilder builder)
        {
            throw new NotSupportedException("SQL Server Compact does not support column renames.");
        }

        public override void Generate(RenameIndexOperation operation, IModel model, SqlBatchBuilder builder)
        {
            throw new NotSupportedException("SQL Server Compact does not support index renames.");
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
                    operation.DefaultValueSql,
                    operation.ComputedColumnSql,
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
            string defaultValueSql,
            string computedColumnSql,
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
                defaultValueSql,
                computedColumnSql,
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
