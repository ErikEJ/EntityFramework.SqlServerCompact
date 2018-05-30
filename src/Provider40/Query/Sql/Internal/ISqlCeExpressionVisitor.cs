using System.Linq.Expressions;
using EFCore.SqlCe.Query.Expressions.Internal;
using JetBrains.Annotations;

namespace EFCore.SqlCe.Query.Sql.Internal
{
    public interface ISqlCeExpressionVisitor
    {
        Expression VisitDatePartExpression([NotNull] DatePartExpression datePartExpression);
    }
}
