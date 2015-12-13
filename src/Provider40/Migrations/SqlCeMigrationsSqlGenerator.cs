using System;
using JetBrains.Annotations;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Metadata.Internal;
using Microsoft.Data.Entity.Migrations.Operations;
using Microsoft.Data.Entity.Storage;
using Microsoft.Data.Entity.Utilities;

namespace Microsoft.Data.Entity.Migrations
{
    public class SqlCeMigrationsSqlGenerator : MigrationsSqlGenerator
    {
        public SqlCeMigrationsSqlGenerator(
            [NotNull] IRelationalCommandBuilderFactory commandBuilderFactory,
            [NotNull] ISqlGenerationHelper sqlGenerationHelper,
            [NotNull] IRelationalTypeMapper typeMapper,
            [NotNull] IRelationalAnnotationProvider annotations)
            : base(commandBuilderFactory, sqlGenerationHelper, typeMapper, annotations)
        {
        }

        protected override void Generate(AlterColumnOperation operation, IModel model, RelationalCommandListBuilder builder)
        {
            Check.NotNull(operation, nameof(operation));
            Check.NotNull(builder, nameof(builder));

            builder
                .EndCommand()
                .Append("ALTER TABLE ")
                .Append(SqlGenerationHelper.DelimitIdentifier(operation.Table))
                .Append(" ALTER COLUMN ")
                .Append(SqlGenerationHelper.DelimitIdentifier(operation.Name))
                .Append(" DROP DEFAULT;");

            builder
                .EndCommand()
                .Append("ALTER TABLE ")
                .Append(SqlGenerationHelper.DelimitIdentifier(operation.Table))
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
                    .EndCommand()
                    .AppendLine(";")
                    .Append("ALTER TABLE ")
                    .Append(SqlGenerationHelper.DelimitIdentifier(operation.Table))
                    .Append(" ALTER COLUMN ")
                    .Append(SqlGenerationHelper.DelimitIdentifier(operation.Name))
                    .Append(" SET ");
                DefaultValue(operation.DefaultValue, operation.DefaultValueSql, builder);
            }
        }

        protected override void ForeignKeyAction(ReferentialAction referentialAction, RelationalCommandListBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            if (referentialAction == ReferentialAction.Restrict)
            {
                builder.Append("NO ACTION");
            }
            else
            {
                base.ForeignKeyAction(referentialAction, builder);
            }
        }

        protected override void Generate(DropIndexOperation operation, IModel model, RelationalCommandListBuilder builder)
        {
            Check.NotNull(operation, nameof(operation));
            Check.NotNull(builder, nameof(builder));

            builder
                .EndCommand()
                .Append("DROP INDEX ")
                .Append(SqlGenerationHelper.DelimitIdentifier(operation.Name));
        }

        #region Invalid schema operations

        protected override void Generate(EnsureSchemaOperation operation, IModel model, RelationalCommandListBuilder builder)
        {
            throw new NotSupportedException("SQL Server Compact does not support schemas.");
        }

        protected override void Generate(DropSchemaOperation operation, IModel model, RelationalCommandListBuilder builder)
        {
            throw new NotSupportedException("SQL Server Compact does not support schemas.");
        }

        #endregion

        #region Sequences not supported

        protected override void Generate(RestartSequenceOperation operation, IModel model, RelationalCommandListBuilder builder)
        {
            throw new NotSupportedException("SQL Server Compact does not support sequences.");
        }

        protected override void Generate(CreateSequenceOperation operation, IModel model, RelationalCommandListBuilder builder)
        {
            throw new NotSupportedException("SQL Server Compact does not support sequences.");
        }

        protected override void Generate(AlterSequenceOperation operation, IModel model, RelationalCommandListBuilder builder)
        {
            throw new NotSupportedException("SQL Server Compact does not support sequences.");
        }

        protected override void Generate(DropSequenceOperation operation, IModel model, RelationalCommandListBuilder builder)
        {
            throw new NotSupportedException("SQL Server Compact does not support sequences.");
        }

        protected override void Generate(RenameSequenceOperation operation, IModel model, RelationalCommandListBuilder builder)
        {
            throw new NotSupportedException("SQL Server Compact does not support sequences.");
        }

        #endregion 

        protected override void Generate(RenameColumnOperation operation, IModel model, RelationalCommandListBuilder builder)
        {
            throw new NotSupportedException("SQL Server Compact does not support column renames.");
        }

        protected override void Generate(RenameIndexOperation operation, IModel model, RelationalCommandListBuilder builder)
        {
            throw new NotSupportedException("SQL Server Compact does not support index renames.");
        }

        protected override void Generate(RenameTableOperation operation, IModel model, RelationalCommandListBuilder builder)
        {
            Check.NotNull(operation, nameof(operation));
            Check.NotNull(builder, nameof(builder));
            if (operation.NewName != null)
            {
                builder
                    .EndCommand()
                    .Append("sp_rename N'")
                    .Append(operation.Name)
                    .Append("', N'")
                    .Append(operation.NewName)
                    .Append("'");
            }
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
            RelationalCommandListBuilder builder)
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
            [CanBeNull] string schema,
            [CanBeNull] string table,
            [NotNull] string name,
            [NotNull] Type clrType,
            [CanBeNull] string type,
            bool nullable,
            [CanBeNull] object defaultValue,
            [CanBeNull] string defaultValueSql,
            [CanBeNull] string computedColumnSql,
            bool identity,
            [NotNull] IAnnotatable annotatable,
            [CanBeNull] IModel model,
            [NotNull] RelationalCommandListBuilder builder)
        {
            Check.NotEmpty(name, nameof(name));
            Check.NotNull(clrType, nameof(clrType));
            Check.NotNull(annotatable, nameof(annotatable));
            Check.NotNull(builder, nameof(builder));

            if (computedColumnSql != null)
            {
                builder
                    .Append(SqlGenerationHelper.DelimitIdentifier(name))
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
