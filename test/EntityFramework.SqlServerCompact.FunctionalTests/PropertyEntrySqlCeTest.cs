using Xunit;

namespace Microsoft.EntityFrameworkCore.Specification.Tests
{
    public class PropertyEntrySqlCeTest : PropertyEntryTestBase<SqlCeTestStore, F1SqlCeFixture>
    {
        public PropertyEntrySqlCeTest(F1SqlCeFixture fixture)
            : base(fixture)
        {
        }

        public override void Property_entry_original_value_is_set()
        {
            base.Property_entry_original_value_is_set();

            Assert.Contains(
                @"SELECT TOP(1) [e].[Id], [e].[EngineSupplierId], [e].[Name]
FROM [Engines] AS [e]",
                Sql);

            Assert.Contains(
                @"UPDATE [Engines] SET [Name] = @p0
WHERE [Id] = @p1 AND [EngineSupplierId] = @p2 AND [Name] = @p3",
                Sql);
        }

        private static string Sql => TestSqlLoggerFactory.Sql;
    }
}
