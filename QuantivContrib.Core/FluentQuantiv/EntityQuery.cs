using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Quantiv.Runtime;

namespace QuantivContrib.Core.FluentQuantiv
{
    public class EntityQuery : IQueryable<QueryableEntity>, IQueryProvider
    {
        private readonly QuantivEntityQueryBuilder _queryBuilder;
        private readonly IList<QueryableEntity> _results;

        public EntityQuery(string entityClassRef)
        {
            _queryBuilder = new QuantivEntityQueryBuilder(entityClassRef);
            _results = new List<QueryableEntity>();
        }

        public IEnumerator<QueryableEntity> GetEnumerator()
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
            return Execute<object>(expression);
        }

        public TResult Execute<TResult>(Expression expression)
        {
            var pureEntityResults = _queryBuilder.ExecuteQuery();
           
            foreach (var quantivEntity in pureEntityResults)
            {
                _results.Add(new QueryableEntity(quantivEntity));
            }

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
                else if (leftItem is ConstantExpression && rightValue is MemberExpression)
                {
                    _queryBuilder.AddQueryPart((MemberExpression)rightValue, (ConstantExpression)leftItem, InvertExpressionType(binaryExpression.NodeType));
                }
                else if (leftItem is MethodCallExpression && rightValue is ConstantExpression)
                {
                    _queryBuilder.AddQueryPart((MethodCallExpression)leftItem, (ConstantExpression)rightValue, binaryExpression.NodeType);
                }
                else if (leftItem is ConstantExpression && rightValue is MethodCallExpression)
                {
                    _queryBuilder.AddQueryPart((MethodCallExpression)rightValue, (ConstantExpression)leftItem, InvertExpressionType(binaryExpression.NodeType));
                }
                else if (leftItem is UnaryExpression && rightValue is ConstantExpression)
                {
                    _queryBuilder.AddQueryPart((UnaryExpression)leftItem, (ConstantExpression)rightValue, binaryExpression.NodeType);
                }
                else if (leftItem is ConstantExpression && rightValue is UnaryExpression)
                {
                    _queryBuilder.AddQueryPart((UnaryExpression)rightValue, (ConstantExpression)leftItem, InvertExpressionType(binaryExpression.NodeType));
                }
                else
                {
                    ProcessExpression(leftItem);
                    ProcessExpression(rightValue);
                }
            }
        }

        private static ExpressionType InvertExpressionType(ExpressionType expressionType)
        {
            var invertExpressionType = expressionType;
            switch (expressionType)
            {
                case ExpressionType.GreaterThan:
                    invertExpressionType = ExpressionType.LessThan;
                    break;
                case ExpressionType.GreaterThanOrEqual:
                    invertExpressionType = ExpressionType.LessThanOrEqual;
                    break;
                case ExpressionType.LessThan:
                    invertExpressionType = ExpressionType.GreaterThan;
                    break;
                case ExpressionType.LessThanOrEqual:
                    invertExpressionType = ExpressionType.GreaterThanOrEqual;
                    break;
                case ExpressionType.Add:
                    invertExpressionType = ExpressionType.Subtract;
                    break;
                case ExpressionType.AddAssign:
                    invertExpressionType = ExpressionType.SubtractAssign;
                    break;
                case ExpressionType.AddAssignChecked:
                    invertExpressionType = ExpressionType.SubtractAssignChecked;
                    break;
                case ExpressionType.Subtract:
                    invertExpressionType = ExpressionType.Add;
                    break;
                case ExpressionType.SubtractAssign:
                    invertExpressionType = ExpressionType.AddAssign;
                    break;
                case ExpressionType.SubtractAssignChecked:
                    invertExpressionType = ExpressionType.AddAssignChecked;
                    break;
                case ExpressionType.LeftShift:
                    invertExpressionType = ExpressionType.RightShift;
                    break;
                case ExpressionType.LeftShiftAssign:
                    invertExpressionType = ExpressionType.RightShiftAssign;
                    break;
                case ExpressionType.RightShift:
                    invertExpressionType = ExpressionType.LeftShift;
                    break;
                case ExpressionType.RightShiftAssign:
                    invertExpressionType = ExpressionType.LeftShiftAssign;
                    break;
                case ExpressionType.Or:
                    invertExpressionType = ExpressionType.And;
                    break;
                case ExpressionType.And:
                    invertExpressionType = ExpressionType.Or;
                    break;
                case ExpressionType.AndAlso:
                    invertExpressionType = ExpressionType.OrElse;
                    break;
            }

            return invertExpressionType;
        }
    }
}
