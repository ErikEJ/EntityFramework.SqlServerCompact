namespace Microsoft.EntityFrameworkCore.Query
{
    public class FiltersInheritanceSqlCeFixture : InheritanceSqlCeFixture
    {
        protected override bool EnableFilters => true;
    }
}
