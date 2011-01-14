using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Quantiv.Runtime;
using Quantiv.Runtime.Support.Enumerations;

namespace QuantivContrib.Core.FluentQuantiv
{
    public class QuantivEntityQueryBuilder
    {
        private readonly string _controllerPool;
        private readonly string _activityRef;
        private readonly string _entityClassRef;

        private readonly EntityListRetriever _retriever;
        private readonly ActivityController _activityController;
        private readonly Activity _activity;

        private Dictionary<int, SearchConditionList> _searchConditions;

        public QuantivEntityQueryBuilder(string entityClassRef):this("BB01_Donation", "_EntityActivity", entityClassRef)
        {
        }

        public QuantivEntityQueryBuilder(string controllerPool, string activityRef, string entityClassRef)
        {

            _controllerPool = controllerPool;
            _activityRef = activityRef;
            _entityClassRef = entityClassRef;
/*
            _activityController = ActivityControllerPooler.AllocateController(_controllerPool);
            _activity = _activityController.StartActivity(_activityRef);

            var manager = _activity.GetEntityManager(_entityClassRef);
            _retriever = manager.CreateEntityListRetriever();*/ //TODO: Uncomment this when connected to QTV again
        }
        
        public void OpenQueryPart()
        {
            if(_searchConditions.Count == 0)
            {
                _searchConditions[0] = _retriever.SearchConditionList;
            }
            else
            {
                _searchConditions[_searchConditions.Count] = _searchConditions[_searchConditions.Count - 1].CreateChildConditionList();
            }
        }

        public void AddQueryPart(MemberExpression leftItem, ConstantExpression rightValue, ExpressionType nodeType)
        {
            if(leftItem.Member == typeof(QueryableEntity).GetMember("EntityId")[0])
            {
                AddCriteriaToActiveSearchCondition(_entityClassRef + "Id", rightValue.Value, GetSearchRelationType(nodeType));
            }
            else
            {
                throw new InvalidOperationException("Queries based on " + leftItem.Member.Name + " are not yet supported.");
            }
        }

        public void AddQueryPart(MethodCallExpression leftItem, ConstantExpression rightValue, ExpressionType nodeType)
        {
            if (leftItem.Method.Name == "Get" || leftItem.Method.Name == "GetAttributeValue")
            {
                var exp = (ConstantExpression)leftItem.Arguments[1];
                AddCriteriaToActiveSearchCondition(exp.Value.ToString(), rightValue.Value, GetSearchRelationType(nodeType));
            }
            else
            {
                throw new InvalidOperationException("Queries based on " + leftItem.Method.Name + " are not yet supported.");
            }
        }

        public void AddQueryPart(UnaryExpression leftItem, ConstantExpression rightValue, ExpressionType nodeType)
        {
            if(leftItem.Operand is MethodCallExpression)
            {
                var unaryOperation = (MethodCallExpression)leftItem.Operand;

                if(unaryOperation.Method.Name != "get_Item")
                {
                    throw new InvalidOperationException("Queries based on " + leftItem.Method.Name + " are not yet supported.");
                }

                if(unaryOperation.Arguments[0] is ConstantExpression)
                {
                    var attributeRef = ((ConstantExpression) unaryOperation.Arguments[0]).Value;
                    AddCriteriaToActiveSearchCondition(attributeRef.ToString(), rightValue.Value, GetSearchRelationType(nodeType));
                }
                else
                {
                    throw new InvalidOperationException("Only constant expressions are currently supported.");
                }

            }
            else
            {
                throw new InvalidOperationException("Queries based on " + leftItem.Method.Name + " are not yet supported.");
            }
        }

        private void AddCriteriaToActiveSearchCondition(string attributeRef, object value, SearchRelationType searchRelationType)
        {
            return; // TODO: Remove me to enable building of criteria


            _retriever.SearchConditionList.AddCondition(attributeRef, value, searchRelationType, true);
        }

        public IList<Entity> ExecuteQuery()
        {
            try
            {
                return _retriever.Retrieve();
            }
            finally
            {
                try
                {
                    _activityController.EndCurrentActivity();
                    ActivityControllerPooler.ReleaseController(_activityController);
                }
                catch
                {
                    // Can't fix this.
                }
            }
        }

        private static SearchRelationType GetSearchRelationType(ExpressionType expressionType)
        {
            switch (expressionType)
            {
                case ExpressionType.Equal:
                    return SearchRelationType.Equal;
                case ExpressionType.GreaterThan:
                    return SearchRelationType.GreaterThan;
                case ExpressionType.GreaterThanOrEqual:
                    return SearchRelationType.GreaterThanOrEqual;
                case ExpressionType.LessThan:
                    return SearchRelationType.LessThan;
                case ExpressionType.LessThanOrEqual:
                    return SearchRelationType.LessThanOrEqual;
                case ExpressionType.NotEqual:
                case ExpressionType.Not:
                    return SearchRelationType.NotEqual;
            }

            throw new InvalidOperationException("Expression type not supported.");
        }
    }
}