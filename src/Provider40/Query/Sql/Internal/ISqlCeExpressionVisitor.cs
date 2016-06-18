using System.Linq.Expressions;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Query.Expressions;
using Microsoft.EntityFrameworkCore.Query.Expressions.Internal;

namespace Microsoft.EntityFrameworkCore.Query.Sql.Internal
{
    public interface ISqlCeExpressionVisitor
    {
        Expression VisitDatePartExpression([NotNull] DatePartExpression datePartExpression);
    }
}
