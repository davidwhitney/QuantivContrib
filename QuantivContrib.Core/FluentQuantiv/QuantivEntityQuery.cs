using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Quantiv.Runtime;

namespace QuantivContrib.Core.FluentQuantiv
{
    public class QuantivEntityQuery: IQueryable<Entity>, IQueryProvider
    {
        private readonly FluentQueryBuilder _queryBuilder;

        private IList<Entity> _results;

        public QuantivEntityQuery(string entityClassRef)
        {
            _queryBuilder = new FluentQueryBuilder(entityClassRef);
        }

        public IEnumerator<Entity> GetEnumerator()
        {
            return _results.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _results.GetEnumerator();
        }

        public Expression Expression
        {
            get { return Expression.Constant(this); }
        }

        public Type ElementType
        {
            get { return typeof(Entity); }
        }

        public IQueryProvider Provider
        {
            get { return this; }
        }

        public IQueryable CreateQuery(Expression expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            var methodcall = (MethodCallExpression)expression;
            foreach (var param in methodcall.Arguments)
            {
                ProcessExpression(param);
            }

            var queryable = (IQueryable<TElement>)this;
            return queryable;
        }

        public object Execute(Expression expression)
        {
            throw new NotImplementedException();
        }

        public TResult Execute<TResult>(Expression expression)
        {
            _results = _queryBuilder.ExecuteQuery();
            return default(TResult);
        }

        private void ProcessExpression(Expression expression)
        {
            if (expression is UnaryExpression)
            {
                var unary = (UnaryExpression)expression;
                ProcessExpression(unary.Operand);
            }

            if (expression is LambdaExpression)
            {
                var lambda = (LambdaExpression)expression;
                ProcessExpression(lambda.Body);
            }

            if(expression is BinaryExpression)
            {
                var binaryExpression = (BinaryExpression)expression;
                
                var leftItem = binaryExpression.Left;
                var rightValue = binaryExpression.Right;

                if (leftItem is MemberExpression && rightValue is ConstantExpression)
                {
                    _queryBuilder.AddQueryPart((MemberExpression)leftItem, (ConstantExpression)rightValue, binaryExpression.NodeType);
                }
                else
                {
                    ProcessExpression(leftItem);
                    ProcessExpression(rightValue);
                }
            }

        }
    }

}
