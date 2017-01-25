using System;
using System.Collections.Generic;
using System.Data.SqlServerCe;
using System.Diagnostics;
using System.Linq;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Microsoft.EntityFrameworkCore.Utilities;


namespace Microsoft.EntityFrameworkCore.Migrations
{
    public class SqlCeMigrationsSqlGenerator : MigrationsSqlGenerator
    {
        private readonly IRelationalCommandBuilderFactory _commandBuilderFactory;
        private readonly IRelationalAnnotationProvider _annotations;
        private readonly ISensitiveDataLogger<SqlCeMigrationsSqlGenerator> _logger;

        public SqlCeMigrationsSqlGenerator(
            [NotNull] IRelationalCommandBuilderFactory commandBuilderFactory,
            [NotNull] ISqlGenerationHelper sqlGenerationHelper,
            [NotNull] IRelationalTypeMapper typeMapper,
            [NotNull] IRelationalAnnotationProvider annotations,
            [NotNull] ISensitiveDataLogger<SqlCeMigrationsSqlGenerator> logger)
            : base(commandBuilderFactory, sqlGenerationHelper, typeMapper, annotations)
        {
            _commandBuilderFactory = commandBuilderFactory;
            _annotations = annotations;
            _logger = logger;
        }

        public override IReadOnlyList<MigrationCommand> Generate(IReadOnlyList<MigrationOperation> operations, IModel model = null)
        {
            Check.NotNull(operations, nameof(operations));

            var builder = new MigrationCommandListBuilder(_commandBuilderFactory);
            foreach (var operation in operations)
            {
                Generate(operation, model, builder);
                builder
                    .EndCommand();
            }
            var list = builder.GetCommandList();

            //HACK to force logging of migration SQL
            foreach (var migrationCommand in list)
            {
                _logger.LogCommandExecuted(new SqlCeCommand(migrationCommand.CommandText), Stopwatch.GetTimestamp(), Stopwatch.GetTimestamp());
            }
            return list;
        }

        protected override void Generate(
            CreateIndexOperation operation,
            IModel model,
            MigrationCommandListBuilder builder,
            bool terminate)
        {
            Check.NotNull(operation, nameof(operation));
            Check.NotNull(builder, nameof(builder));

            builder.Append("CREATE ");

            var isNullableKey = operation
                .Columns.Select(column => FindProperty(model, null, operation.Table, column))
                .Any(property => (property != null) && property.IsColumnNullable() && property.IsForeignKey());

            if (operation.IsUnique && !isNullableKey)
            {
                builder.Append("UNIQUE ");
            }

            builder
                .Append("INDEX ")
                .Append(SqlGenerationHelper.DelimitIdentifier(operation.Name))
                .Append(" ON ")
                .Append(SqlGenerationHelper.DelimitIdentifier(operation.Table, operation.Schema))
                .Append(" (")
                .Append(ColumnList(operation.Columns))
                .Append(")");

            if (terminate)
            {
                builder.AppendLine(SqlGenerationHelper.StatementTerminator);

                EndStatement(builder);
            }
        }


        protected override void Generate(AlterColumnOperation operation, IModel model, MigrationCommandListBuilder builder)
        {
            Check.NotNull(operation, nameof(operation));
            Check.NotNull(builder, nameof(builder));

            builder
                .EndCommand()
                .Append("ALTER TABLE ")
                .Append(SqlGenerationHelper.DelimitIdentifier(operation.Table))
                .Append(" ALTER COLUMN ")
                .Append(SqlGenerationHelper.DelimitIdentifier(operation.Name))
                .Append(" DROP DEFAULT")
                .AppendLine();
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
                operation.IsUnicode,
                operation.MaxLength,
                operation.IsRowVersion,
                operation.IsNullable,
                /*defaultValue:*/ null,
                /*defaultValueSql:*/ null,
                operation.ComputedColumnSql,
                /*identity:*/ false,
                operation,
                model,
                builder);

            builder.AppendLine();

            if ((operation.DefaultValue != null) || (operation.DefaultValueSql != null))
            {
                builder
                    .EndCommand()
                    .Append("ALTER TABLE ")
                    .Append(SqlGenerationHelper.DelimitIdentifier(operation.Table))
                    .Append(" ALTER COLUMN ")
                    .Append(SqlGenerationHelper.DelimitIdentifier(operation.Name))
                    .Append(" SET ");
                DefaultValue(operation.DefaultValue, operation.DefaultValueSql, builder);
            }
        }

        protected override void ForeignKeyAction(ReferentialAction referentialAction, MigrationCommandListBuilder builder)
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

        protected override void Generate(DropIndexOperation operation, IModel model, MigrationCommandListBuilder builder)
        {
            Check.NotNull(operation, nameof(operation));
            Check.NotNull(builder, nameof(builder));

            builder
                .EndCommand()
                .Append("DROP INDEX ")
                .Append(SqlGenerationHelper.DelimitIdentifier(operation.Table))
                .Append(".")
                .Append(SqlGenerationHelper.DelimitIdentifier(operation.Name));
        }

        private const string NotSupported = "SQL Server Compact does not support this migration operation ('{0}').";

        #region Invalid schema operations
        protected override void Generate(EnsureSchemaOperation operation, IModel model, MigrationCommandListBuilder builder)
        {
            throw new NotSupportedException(string.Format(NotSupported, operation.GetType().Name));
        }

        protected override void Generate(DropSchemaOperation operation, IModel model, MigrationCommandListBuilder builder)
        {
            throw new NotSupportedException(string.Format(NotSupported, operation.GetType().Name));
        }
        #endregion

        #region Sequences not supported
        protected override void Generate(RestartSequenceOperation operation, IModel model, MigrationCommandListBuilder builder)
        {
            throw new NotSupportedException(string.Format(NotSupported, operation.GetType().Name));
        }

        protected override void Generate(CreateSequenceOperation operation, IModel model, MigrationCommandListBuilder builder)
        {
            throw new NotSupportedException(string.Format(NotSupported, operation.GetType().Name));
        }

        protected override void Generate(AlterSequenceOperation operation, IModel model, MigrationCommandListBuilder builder)
        {
            throw new NotSupportedException(string.Format(NotSupported, operation.GetType().Name));
        }

        protected override void Generate(DropSequenceOperation operation, IModel model, MigrationCommandListBuilder builder)
        {
            throw new NotSupportedException(string.Format(NotSupported, operation.GetType().Name));
        }

        protected override void Generate(RenameSequenceOperation operation, IModel model, MigrationCommandListBuilder builder)
        {
            throw new NotSupportedException(string.Format(NotSupported, operation.GetType().Name));
        }
        #endregion 

        protected override void Generate(RenameColumnOperation operation, IModel model, MigrationCommandListBuilder builder)
        {
            throw new NotSupportedException(string.Format(NotSupported, operation.GetType().Name));
        }

        protected override void Generate(RenameIndexOperation operation, IModel model, MigrationCommandListBuilder builder)
        {
            Check.NotNull(operation, nameof(operation));
            Check.NotNull(builder, nameof(builder));

            if (model == null)
            {
                throw new NotSupportedException(string.Format(NotSupported, operation.GetType().Name));
            }

            var index = FindEntityType(model, null, operation.Table).GetIndexes().Single(i => _annotations.For(i).Name == operation.Name);

            var dropIndexOperation = new DropIndexOperation
            {
                Name = operation.Name,
                IsDestructiveChange = true,
                Table = operation.Table
            };
            Generate(dropIndexOperation, model, builder);

            var createIndexOperation = new CreateIndexOperation
            {
                Columns = index.Properties.Select(p => p.Name).ToArray(),
                IsUnique = index.IsUnique,
                Name = operation.NewName,
                Table = operation.Table
            };
            Generate(createIndexOperation, model, builder);
        }

        protected override void Generate(RenameTableOperation operation, IModel model, MigrationCommandListBuilder builder)
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
                    bool? unicode,
                    int? maxLength,
                    bool rowVersion,
                    bool nullable,
                    object defaultValue,
                    string defaultValueSql,
                    string computedColumnSql,
                    IAnnotatable annotatable,
                    IModel model,
                    MigrationCommandListBuilder builder)
        {
            var valueGeneration = (string)annotatable[SqlCeAnnotationNames.Prefix + SqlCeAnnotationNames.ValueGeneration];

            ColumnDefinition(
                schema,
                table,
                name,
                clrType,
                type,
                unicode,
                maxLength,
                rowVersion,
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
            [NotNull] string table,
            [NotNull] string name,
            [NotNull] Type clrType,
            [CanBeNull] string type,
            [CanBeNull] bool? unicode,
            [CanBeNull] int? maxLength,
            bool rowVersion,
            bool nullable,
            [CanBeNull] object defaultValue,
            [CanBeNull] string defaultValueSql,
            [CanBeNull] string computedColumnSql,
            bool identity,
            [NotNull] IAnnotatable annotatable,
            [CanBeNull] IModel model,
            [NotNull] MigrationCommandListBuilder builder)
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
                unicode,
                maxLength,
                rowVersion,
                nullable,
                identity
                    ? null     
                    : defaultValue,
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

       private IEntityType FindEntityType(
       IModel model,
       string schema,
       string tableName)
       => model?.GetEntityTypes().FirstOrDefault(
           t => (_annotations.For(t).TableName == tableName) && (_annotations.For(t).Schema == schema));
    }
}
