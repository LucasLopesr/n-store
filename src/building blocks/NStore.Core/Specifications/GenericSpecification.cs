﻿using System;
using System.Linq.Expressions;

namespace NStore.Core.Specifications
{
    public class GenericSpecification<T>
    {
        private Expression<Func<T, bool>> Expression { get; }

        public GenericSpecification(Expression<Func<T, bool>> expression)
        {
            Expression = expression;
        }

        public bool IsSatisfiedBy(T entity)
        {
            return Expression.Compile().Invoke(entity);
        }
    }
}
