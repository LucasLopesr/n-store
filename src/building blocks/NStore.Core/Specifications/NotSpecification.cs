using System;
using System.Linq;
using System.Linq.Expressions;


namespace NStore.Core.Specifications
{
    internal sealed class NotSpecification<T> : Specification<T>
    {
        private readonly Specification<T> specification;

        public NotSpecification(Specification<T> specification)
        {
            this.specification = specification;
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            var expression = specification.ToExpression();
            var notExpression = Expression.Not(expression.Body);

            return Expression.Lambda<Func<T, bool>>(notExpression, expression.Parameters.Single());
        }
    }
}
