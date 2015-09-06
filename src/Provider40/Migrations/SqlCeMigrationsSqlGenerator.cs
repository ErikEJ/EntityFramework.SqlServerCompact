using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Migrations.Operations;
using Microsoft.Data.Entity.SqlServerCompact.Metadata;
using Microsoft.Data.Entity.Storage.Commands;
using Microsoft.Data.Entity.Update;
using Microsoft.Data.Entity.Utilities;

namespace Microsoft.Data.Entity.SqlServerCompact.Migrations
{
    public class SqlCeMigrationsSqlGenerator : MigrationsSqlGenerator
    {
        private readonly IUpdateSqlGenerator _sql;

        public SqlCeMigrationsSqlGenerator(
            [NotNull] SqlCeUpdateSqlGenerator sqlGenerator,
            [NotNull] SqlCeTypeMapper typeMapper,
            [NotNull] SqlCeMetadataExtensionProvider annotations)
            : base(sqlGenerator, typeMapper, annotations)
        {
            _sql = sqlGenerator;
        }

        protected override void Generate(
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
                .Append(" ALTER COLUMN ")
                .Append(_sql.DelimitIdentifier(operation.Name))
                .Append(" DROP DEFAULT;");

            builder
                .EndBatch()
                .Append("ALTER TABLE ")
                .Append(_sql.DelimitIdentifier(operation.Table))
                .Append(" ALTER COLUMN ");
            ColumnDefinition(
                    null,
                    operation.Table,
                    operation.Name,
                    operation.ClrType,
                    operation.ColumnType,
                    operation.IsNullable,
                    null /*operation.DefaultValue */,
                    null /*operation.DefaultValueSql */,
                    operation.ComputedColumnSql,
                    operation,
                    model,
                    builder);

            if (operation.DefaultValue != null || operation.DefaultValueSql != null)
            {
                builder
                    .EndBatch()
                    .AppendLine(";")
                    .Append("ALTER TABLE ")
                    .Append(_sql.DelimitIdentifier(operation.Table))
                    .Append(" ALTER COLUMN ")
                    .Append(_sql.DelimitIdentifier(operation.Name))
                    .Append(" SET ");
                DefaultValue(operation.DefaultValue, operation.DefaultValueSql, builder);
            }
        }

        protected override void Generate(DropIndexOperation operation, IModel model, SqlBatchBuilder builder)
        {
            Check.NotNull(operation, nameof(operation));
            Check.NotNull(builder, nameof(builder));

            builder
                .EndBatch()
                .Append("DROP INDEX ")
                .Append(_sql.DelimitIdentifier(operation.Name));
        }

        #region Invalid schema operations

        protected override void Generate(EnsureSchemaOperation operation, IModel model, SqlBatchBuilder builder)
        {
            throw new NotSupportedException("SQL Server Compact does not support schemas.");
        }

        protected override void Generate(DropSchemaOperation operation, IModel model, SqlBatchBuilder builder)
        {
            throw new NotSupportedException("SQL Server Compact does not support schemas.");
        }

        #endregion

        #region Sequences not supported

        protected override void Generate(RestartSequenceOperation operation, IModel model, SqlBatchBuilder builder)
        {
            throw new NotSupportedException("SQL Server Compact does not support sequences.");
        }

        protected override void Generate(CreateSequenceOperation operation, IModel model, SqlBatchBuilder builder)
        {
            throw new NotSupportedException("SQL Server Compact does not support sequences.");
        }

        protected override void Generate(AlterSequenceOperation operation, IModel model, SqlBatchBuilder builder)
        {
            throw new NotSupportedException("SQL Server Compact does not support sequences.");
        }

        protected override void Generate(DropSequenceOperation operation, IModel model, SqlBatchBuilder builder)
        {
            throw new NotSupportedException("SQL Server Compact does not support sequences.");
        }

        protected override void Generate(RenameSequenceOperation operation, IModel model, SqlBatchBuilder builder)
        {
            throw new NotSupportedException("SQL Server Compact does not support sequences.");
        }

        #endregion 

        protected override void Generate(RenameColumnOperation operation, IModel model, SqlBatchBuilder builder)
        {
            throw new NotSupportedException("SQL Server Compact does not support column renames.");
        }

        protected override void Generate(RenameIndexOperation operation, IModel model, SqlBatchBuilder builder)
        {
            throw new NotSupportedException("SQL Server Compact does not support index renames.");
        }

        protected override void Generate(RenameTableOperation operation, IModel model, SqlBatchBuilder builder)
        {
            Check.NotNull(operation, nameof(operation));
            Check.NotNull(builder, nameof(builder));
            if (operation.NewName != null)
            {
                builder
                    .EndBatch()
                    .Append("sp_rename N'")
                    .Append(operation.Name)
                    .Append("', N'")
                    .Append(operation.NewName)
                    .Append("'");
            }
        }

        //    //TODO ErikEJ - may not be required any longer(pending bug fix)
        public override IReadOnlyList<RelationalCommand> Generate(
    IReadOnlyList<MigrationOperation> operations,
    IModel model = null)
        {
            Check.NotNull(operations, nameof(operations));

            var builder = new SqlBatchBuilder();
            foreach (var operation in operations)
            {
                Generate(operation, model, builder);
                builder
                    //.AppendLine(Sql.BatchCommandSeparator)
                    .AppendLine()
                    .EndBatch();
            }
            return builder.RelationalCommands;
        }

        protected override void ColumnDefinition(
            string schema,
            string table,
            string name,
            Type clrType,
            string type,
            bool nullable,
            object defaultValue,
            string defaultValueSql,
            string computedColumnSql,
            IAnnotatable annotatable,
            IModel model,
            SqlBatchBuilder builder)
        {
            var valueGeneration = (string)annotatable[SqlCeAnnotationNames.Prefix + SqlCeAnnotationNames.ValueGeneration];

            ColumnDefinition(
                schema,
                table,
                name,
                clrType,
                type,
                nullable,
                defaultValue,
                defaultValueSql,
                computedColumnSql,
                valueGeneration == SqlCeAnnotationNames.Identity,
                annotatable,
                model,
                builder);
        }

        protected virtual void ColumnDefinition(
            string schema,
            string table,
            string name,
            Type clrType,
            string type,
            bool nullable,
            object defaultValue,
            string defaultValueSql,
            string computedColumnSql,
            bool identity,
            IAnnotatable annotatable,
            IModel model,
            SqlBatchBuilder builder)
        {
            Check.NotEmpty(name, nameof(name));
            Check.NotNull(clrType, nameof(clrType));
            Check.NotNull(annotatable, nameof(annotatable));
            Check.NotNull(builder, nameof(builder));

            if (computedColumnSql != null)
            {
                builder
                    .Append(Sql.DelimitIdentifier(name))
                    .Append(" AS ")
                    .Append(computedColumnSql);

                return;
            }

            base.ColumnDefinition(
                schema,
                table,
                name,
                clrType,
                type,
                nullable,
                defaultValue,
                defaultValueSql,
                computedColumnSql,
                annotatable,
                model,
                builder);

            if (identity)
            {
                builder.Append(" IDENTITY");
            }
        }
    }
}
