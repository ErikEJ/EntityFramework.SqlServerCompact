namespace Microsoft.EntityFrameworkCore.Infrastructure
{
    /// <summary>
    ///     Values that are used as the eventId when logging messages from the SQL Compact provider via <see cref="ILogger" />.
    /// </summary>
    public enum SqlCeEventId
    {
        /// <summary>
        ///     A schema was configured for an entity type
        /// </summary>
        SchemaConfiguredWarning = 1,

        /// <summary>
        ///     A sequence was configured
        /// </summary>
        SequenceWarning = 2
    }
}