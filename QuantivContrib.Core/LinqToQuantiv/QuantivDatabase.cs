using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Quantiv.Runtime;
using QuantivContrib.Core.DataAccessExtensions;

namespace QuantivContrib.Core.LinqToQuantiv
{
    public class QuantivDatabase: IQueryable<EntityQueryParts>, IQueryProvider
    {
        private ConnectedEntity _currentEntity;
        private string _entityRef;
        private readonly FluentQueryBuilder _queryBuilder;

        public QuantivDatabase()
        {
            _queryBuilder = new FluentQueryBuilder(this);
        }

        public IEnumerator<EntityQueryParts> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
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
            var methodCall = expression as MethodCallExpression;

            if (methodCall != null)
            {
                foreach (var param in methodCall.Arguments)
                {
                    ProcessExpression(param);
                }
            }
            

            throw new NotImplementedException();
        }

        public object Execute(Expression expression)
        {
            using (var session = new EntityActivitySession("BB01_Donation"))
            using (var webDirectoryEntity = new ConnectedEntity(session, "WebDirectory", 1))
            {
                _currentEntity = webDirectoryEntity;
            }

            return null;
        }

        public TResult Execute<TResult>(Expression expression)
        {
            MethodCallExpression methodcall = expression as MethodCallExpression;
            foreach(var param in methodcall.Arguments)
            {
                ProcessExpression(param);
            }

            return default(TResult);
        }

        private void ProcessExpression(Expression expression)
        {
            if (expression.NodeType == ExpressionType.Equal)
            {

            }
            if (expression.NodeType == ExpressionType.LessThan)
            {

            }
        }

        public IQuantivEntityIdentifierQueryBuilder Load(string entityRef)
        {
            _entityRef = entityRef;
            return _queryBuilder;
        }
    }

    public class FluentQueryBuilder : IQueryBuilderLoadType, IQuantivEntityIdentifierQueryBuilder, IBuilderSearchConditions,ILookupTargetBuilder
    {
        private readonly QuantivDatabase _quantivDatabase;
        private bool _queryById;
        private int _id;

        public FluentQueryBuilder(QuantivDatabase quantivDatabase)
        {
            _quantivDatabase = quantivDatabase;
        }

        FluentQueryBuilder IQueryBuilderLoadType.IdField(int id)
        {
            _queryById = true;
            _id = id;

            return this;
        }

        IBuilderSearchConditions IQueryBuilderLoadType.SearchConditions
        {
            get { return this; }
        }

        IQueryBuilderLoadType IQuantivEntityIdentifierQueryBuilder.By
        {
            get { return this; }
        }

        ILookupTargetBuilder IBuilderSearchConditions.Match(Expression<Func<AttributeRefQuery, bool>> condition)
        {
            return this;
        }

        public QuantivDatabase ToList()
        {
            return _quantivDatabase;
        }

        IBuilderSearchConditions ILookupTargetBuilder.Where(Expression<Func<AttributeRefQueryTarget, bool>> condition)
        {
            return this;
        }
    }


    public interface IQuantivEntityIdentifierQueryBuilder
    {
        IQueryBuilderLoadType By { get; }
    }

    public interface IQueryBuilderLoadType
    {
        FluentQueryBuilder IdField(int id);
        IBuilderSearchConditions SearchConditions { get; }
    }

    public interface IBuilderSearchConditions
    {
        ILookupTargetBuilder Match(Expression<Func<AttributeRefQuery, bool>> condition);
        QuantivDatabase ToList();
    }

    public interface ILookupTargetBuilder
    {
        IBuilderSearchConditions Where(Expression<Func<AttributeRefQueryTarget, bool>> condition);
    }

}
