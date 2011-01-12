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

        public QuantivEntityIdentifierQueryBuilder Load(string entityRef)
        {
            _entityRef = entityRef;
            return new QuantivEntityIdentifierQueryBuilder(this);
        }
    }

    public class QuantivEntityIdentifierQueryBuilder
    {
        private readonly QuantivDatabase _quantivDatabase;
        public QuantivEntityIdentifierQueryBuilderBy By { get; private set; }

        public QuantivEntityIdentifierQueryBuilder(QuantivDatabase quantivDatabase)
        {
            _quantivDatabase = quantivDatabase;
            By = new QuantivEntityIdentifierQueryBuilderBy(_quantivDatabase);
        }
    }

    public class QuantivEntityIdentifierQueryBuilderBy
    {
        public QuantivEntityIdentifierQueryBuilderBySearchCondition SearchCondition { get; private set; }
        
        private readonly QuantivDatabase _quantivDatabase;

        public QuantivEntityIdentifierQueryBuilderBy(QuantivDatabase quantivDatabase)
        {
            _quantivDatabase = quantivDatabase;
            SearchCondition = new QuantivEntityIdentifierQueryBuilderBySearchCondition(_quantivDatabase);
        }

        public QuantivDatabase Id(int id)
        {
            return _quantivDatabase;
        }
    }

    public class QuantivEntityIdentifierQueryBuilderBySearchCondition
    {
        private readonly QuantivDatabase _quantivDatabase;
        private readonly LookupTargetBuilder _lookupTargetBuilder;

        public QuantivEntityIdentifierQueryBuilderBySearchCondition(QuantivDatabase quantivDatabase)
        {
            _quantivDatabase = quantivDatabase;
            _lookupTargetBuilder = new LookupTargetBuilder(quantivDatabase, this);
        }

        public LookupTargetBuilder Lookup(Expression<Func<AttributeRefQuery, bool>> condition)
        {
            return _lookupTargetBuilder;
        }

        public QuantivDatabase ToList()
        {
            return _quantivDatabase;
        }
    }

    public class LookupTargetBuilder
    {
        private readonly QuantivDatabase _quantivDatabase;
        private readonly QuantivEntityIdentifierQueryBuilderBySearchCondition _quantivEntityIdentifierQueryBuilderBySearchCondition;

        public LookupTargetBuilder(QuantivDatabase quantivDatabase, QuantivEntityIdentifierQueryBuilderBySearchCondition quantivEntityIdentifierQueryBuilderBySearchCondition)
        {
            _quantivDatabase = quantivDatabase;
            _quantivEntityIdentifierQueryBuilderBySearchCondition = quantivEntityIdentifierQueryBuilderBySearchCondition;
        }

        public QuantivEntityIdentifierQueryBuilderBySearchCondition Where(Expression<Func<AttributeRefQueryTarget, bool>> condition)
        {
            return _quantivEntityIdentifierQueryBuilderBySearchCondition;
        }
    }

}
