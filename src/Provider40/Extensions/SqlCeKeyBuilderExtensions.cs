using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Utilities;

// ReSharper disable once CheckNamespace

namespace Microsoft.EntityFrameworkCore
{
    /// <summary>
    ///     SQL Server Compact specific extension methods for <see cref="KeyBuilder"/>.
    /// </summary>
    public static class SqlCeKeyBuilderExtensions
    {
        /// <summary>
        ///     Configures the name of the key constraint in the database when targeting SQL Server Compact.
        /// </summary>
        /// <param name="keyBuilder"> The builder for the key being configured. </param>
        /// <param name="name"> The name of the key. </param>
        /// <returns> The same builder instance so that multiple calls can be chained. </returns>
        public static KeyBuilder ForSqlCeHasName([NotNull] this KeyBuilder keyBuilder, [CanBeNull] string name)
        {
            Check.NotNull(keyBuilder, nameof(keyBuilder));
            Check.NullButNotEmpty(name, nameof(name));

            keyBuilder.Metadata.SqlCe().Name = name;

            return keyBuilder;
        }
    }
}
