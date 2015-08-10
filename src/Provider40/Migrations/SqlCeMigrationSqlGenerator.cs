using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations.Operations;
using Microsoft.Data.Entity.Migrations.Sql;
using Microsoft.Data.Entity.SqlServerCompact.Metadata;
using Microsoft.Data.Entity.Update;
using Microsoft.Data.Entity.Utilities;

namespace Microsoft.Data.Entity.SqlServerCompact.Migrations
{
    public class SqlCeMigrationSqlGenerator : MigrationSqlGenerator
    {
        private readonly IUpdateSqlGenerator _sql;

        public SqlCeMigrationSqlGenerator(
            [NotNull] SqlCeUpdateSqlGenerator sqlGenerator,
            [NotNull] SqlCeTypeMapper typeMapper,
            [NotNull] SqlCeMetadataExtensionProvider annotations)
            : base(sqlGenerator, typeMapper, annotations)
        {
            _sql = sqlGenerator;
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

        public override void Generate(DropIndexOperation operation, IModel model, SqlBatchBuilder builder)
        {
            Check.NotNull(operation, nameof(operation));
            Check.NotNull(builder, nameof(builder));

            builder
                .EndBatch()
                .Append("DROP INDEX ")
                .Append(_sql.DelimitIdentifier(operation.Name));
        }

        #region Invalid schema operations

        public override void Generate(CreateSchemaOperation operation, IModel model, SqlBatchBuilder builder)
        {
            throw new NotSupportedException("SQL Server Compact does not support schemas.");
        }

        public override void Generate(DropSchemaOperation operation, IModel model, SqlBatchBuilder builder)
        {
            throw new NotSupportedException("SQL Server Compact does not support schemas.");
        }

        #endregion

        #region Sequences not supported

        public override void Generate(RestartSequenceOperation operation, IModel model, SqlBatchBuilder builder)
        {
            throw new NotSupportedException("SQL Server Compact does not support sequences.");
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

        #endregion 

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

        public override void ColumnDefinition(
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
            Check.NotEmpty(name, nameof(name));
            Check.NotNull(clrType, nameof(clrType));
            Check.NotNull(annotatable, nameof(annotatable));
            Check.NotNull(builder, nameof(builder));

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

            var valueGeneration = (string)annotatable[SqlCeAnnotationNames.Prefix + SqlCeAnnotationNames.ValueGeneration];
            if (valueGeneration == SqlCeAnnotationNames.Identity)
            {
                builder.Append(" IDENTITY");
            }
        }

    //    protected virtual void DropDefaultConstraint(
    //[CanBeNull] string schema,
    //[NotNull] string tableName,
    //[NotNull] string columnName,
    //[NotNull] SqlBatchBuilder builder)
    //    {
    //        Check.NotEmpty(tableName, nameof(tableName));
    //        Check.NotEmpty(columnName, nameof(columnName));
    //        Check.NotNull(builder, nameof(builder));

    //        var variable = "@var" + _variableCounter++;

    //        builder
    //            .Append("DECLARE ")
    //            .Append(variable)
    //            .AppendLine(" sysname;")
    //            .Append("SELECT ")
    //            .Append(variable)
    //            .AppendLine(" = [d].[name]")
    //            .AppendLine("FROM [sys].[default_constraints] [d]")
    //            .AppendLine("INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id]")
    //            .Append("WHERE ([d].[parent_object_id] = OBJECT_ID(N'");

    //        if (schema != null)
    //        {
    //            builder
    //                .Append(_sql.EscapeLiteral(schema))
    //                .Append(".");
    //        }

    //        builder
    //            .Append(_sql.EscapeLiteral(tableName))
    //            .Append("') AND [c].[name] = N'")
    //            .Append(_sql.EscapeLiteral(columnName))
    //            .AppendLine("');")
    //            .Append("IF ")
    //            .Append(variable)
    //            .Append(" IS NOT NULL EXEC(N'ALTER TABLE ")
    //            .Append(_sql.DelimitIdentifier(tableName, schema))
    //            .Append(" DROP CONSTRAINT [' + ")
    //            .Append(variable)
    //            .AppendLine(" + ']');");
    //    }
    }
}
