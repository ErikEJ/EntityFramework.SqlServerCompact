using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore.Specification.Tests.Utilities;
using Microsoft.EntityFrameworkCore.Query;

namespace Microsoft.EntityFrameworkCore.Specification.Tests
{
    public class FiltersInheritanceSqlCeFixture : InheritanceSqlCeFixture
    {
        protected override bool EnableFilters => true;
        protected override string DatabaseName => "FiltersInheritanceSqlServerTest";
    }
}
