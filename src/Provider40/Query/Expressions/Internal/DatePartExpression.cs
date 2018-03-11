using System;
using System.Linq.Expressions;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Utilities;
using EFCore.SqlCe.Query.Sql.Internal;

namespace EFCore.SqlCe.Query.Expressions.Internal
{
    public class DatePartExpression : Expression
    {
        public DatePartExpression(
            [NotNull] string datePart,
            [NotNull] Type type,
            [NotNull] Expression argument)
        {
            DatePart = datePart;
            Type = type;
            Argument = argument;
        }

        public override Type Type { get; }
        public override ExpressionType NodeType => ExpressionType.Extension;

        public virtual Expression Argument { get; }
        public virtual string DatePart { get; }

        protected override Expression Accept(ExpressionVisitor visitor)
        {
            Check.NotNull(visitor, nameof(visitor));

            var specificVisitor = visitor as ISqlCeExpressionVisitor;

            return specificVisitor != null
                ? specificVisitor.VisitDatePartExpression(this)
                : base.Accept(visitor);
        }

        protected override Expression VisitChildren(ExpressionVisitor visitor) => this;
    }
}
