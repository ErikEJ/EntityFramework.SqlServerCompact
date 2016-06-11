using System.Linq.Expressions;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Query.Expressions;

namespace Microsoft.EntityFrameworkCore.Query.Sql
{
    public interface ISqlCeExpressionVisitor
    {
        Expression VisitDatePartExpression([NotNull] DatePartExpression datePartExpression);
    }
}
