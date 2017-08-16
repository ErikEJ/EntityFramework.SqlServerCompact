﻿using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace Microsoft.EntityFrameworkCore.Diagnostics
{
    /// <summary>
    ///     <para>
    ///         Event IDs for SQL Server Compact events that correspond to messages logged to an <see cref="ILogger" />
    ///         and events sent to a <see cref="DiagnosticSource" />.
    ///     </para>
    ///     <para>
    ///         These IDs are also used with <see cref="WarningsConfigurationBuilder" /> to configure the
    ///         behavior of warnings.
    ///     </para>
    /// </summary>
    public static class SqlCeEventId
    {
        // Warning: These values must not change between releases.
        // Only add new values to the end of sections, never in the middle.
        // Try to use <Noun><Verb> naming and be consistent with existing names.
        private enum Id
        {
            // Model validation events
            SchemaConfiguredWarning = CoreEventId.ProviderBaseId,
            SequenceConfiguredWarning,

            // Scaffolding events
            ColumnFound = CoreEventId.ProviderDesignBaseId,
            ForeignKeyFound,
            ForeignKeyPrincipalColumnMissingWarning,
            ForeignKeyReferencesMissingTableWarning,
            IndexFound,
            MissingTableWarning,
            PrimaryKeyFound,
            SchemasNotSupportedWarning,
            TableFound,
            UniqueConstraintFound
        }

        private static readonly string _validationPrefix = DbLoggerCategory.Model.Validation.Name + ".";
        private static EventId MakeValidationId(Id id) => new EventId((int)id, _validationPrefix + id);

        /// <summary>
        ///     <para>
        ///         A schema was configured for an entity type, but SQL Compact does not support schemas.
        ///     </para>
        ///     <para>
        ///         This event is in the <see cref="DbLoggerCategory.Model.Validation" /> category.
        ///     </para>
        ///     <para>
        ///         This event uses the <see cref="EntityTypeSchemaEventData" /> payload when used with a <see cref="DiagnosticSource" />.
        ///     </para>
        /// </summary>
        public static readonly EventId SchemaConfiguredWarning = MakeValidationId(Id.SchemaConfiguredWarning);

        /// <summary>
        ///     <para>
        ///         A sequence was configured for an entity type, but SQL Compact does not support sequences.
        ///     </para>
        ///     <para>
        ///         This event is in the <see cref="DbLoggerCategory.Model.Validation" /> category.
        ///     </para>
        ///     <para>
        ///         This event uses the <see cref="SequenceEventData" /> payload when used with a <see cref="DiagnosticSource" />.
        ///     </para>
        /// </summary>
        public static readonly EventId SequenceConfiguredWarning = MakeValidationId(Id.SequenceConfiguredWarning);

        private static readonly string _scaffoldingPrefix = DbLoggerCategory.Scaffolding.Name + ".";
        private static EventId MakeScaffoldingId(Id id) => new EventId((int)id, _scaffoldingPrefix + id);

        /// <summary>
        ///     A column was found.
        ///     This event is in the <see cref="DbLoggerCategory.Scaffolding" /> category.
        /// </summary>
        public static readonly EventId ColumnFound = MakeScaffoldingId(Id.ColumnFound);

        /// <summary>
        ///     SQL Server Compact does not support schemas.
        ///     This event is in the <see cref="DbLoggerCategory.Scaffolding" /> category.
        /// </summary>
        public static readonly EventId SchemasNotSupportedWarning = MakeScaffoldingId(Id.SchemasNotSupportedWarning);

        /// <summary>
        ///     A foreign key references a missing table.
        ///     This event is in the <see cref="DbLoggerCategory.Scaffolding" /> category.
        /// </summary>
        public static readonly EventId ForeignKeyReferencesMissingTableWarning = MakeScaffoldingId(Id.ForeignKeyReferencesMissingTableWarning);

        /// <summary>
        ///     A table was found.
        ///     This event is in the <see cref="DbLoggerCategory.Scaffolding" /> category.
        /// </summary>
        public static readonly EventId TableFound = MakeScaffoldingId(Id.TableFound);

        /// <summary>
        ///     The database is missing a table.
        ///     This event is in the <see cref="DbLoggerCategory.Scaffolding" /> category.
        /// </summary>
        public static readonly EventId MissingTableWarning = MakeScaffoldingId(Id.MissingTableWarning);

        /// <summary>
        ///     A column referenced by a foreign key constraint was not found.
        ///     This event is in the <see cref="DbLoggerCategory.Scaffolding" /> category.
        /// </summary>
        public static readonly EventId ForeignKeyPrincipalColumnMissingWarning = MakeScaffoldingId(Id.ForeignKeyPrincipalColumnMissingWarning);

        /// <summary>
        ///     An index was found.
        ///     This event is in the <see cref="DbLoggerCategory.Scaffolding" /> category.
        /// </summary>
        public static readonly EventId IndexFound = MakeScaffoldingId(Id.IndexFound);

        /// <summary>
        ///     A foreign key was found.
        ///     This event is in the <see cref="DbLoggerCategory.Scaffolding" /> category.
        /// </summary>
        public static readonly EventId ForeignKeyFound = MakeScaffoldingId(Id.ForeignKeyFound);

        /// <summary>
        ///     A primary key was found.
        ///     This event is in the <see cref="DbLoggerCategory.Scaffolding" /> category.
        /// </summary>
        public static readonly EventId PrimaryKeyFound = MakeScaffoldingId(Id.PrimaryKeyFound);

        /// <summary>
        ///     A unique constraint was found.
        ///     This event is in the <see cref="DbLoggerCategory.Scaffolding" /> category.
        /// </summary>
        public static readonly EventId UniqueConstraintFound = MakeScaffoldingId(Id.UniqueConstraintFound);
    }
}