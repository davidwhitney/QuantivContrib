using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Quantiv.Runtime;

namespace QuantivContrib.Core.FluentQuantiv
{
    public class FluentQueryBuilder
    {
        private const string ControllerPool = "BB01_Donation";
        private const string ActivityRef = "_EntityActivity";

        private bool _queryById;
        private int _id;
        private string _entityClassRef;

        public FluentQueryBuilder(string entityClassRef)
        {
            _entityClassRef = entityClassRef;
        }

        public void AddQueryPart(MemberExpression leftItem, ConstantExpression rightValue, ExpressionType nodeType)
        {
            if(leftItem.Member == typeof(Entity).GetMember("EntityName")[0])
            {
                if(nodeType != ExpressionType.Equal)
                {
                    throw new InvalidOperationException("You may only specify equals for EntityName.");
                }

                _entityClassRef = rightValue.Value.ToString();
            }

            if(leftItem.Member == typeof(Entity).GetMember("EntityId")[0])
            {
                _queryById = true;
                if(nodeType != ExpressionType.Equal)
                {
                    throw new InvalidOperationException("You may only specify equals for EntityId.");
                }

                _id = (int)rightValue.Value;
            }
        }

        public IList<Entity> ExecuteQuery()
        {
            var activityController = ActivityControllerPooler.AllocateController(ControllerPool);

            try
            {
                var activity = activityController.StartActivity(ActivityRef);

                var manager = activity.GetEntityManager(_entityClassRef);
                var retriever = manager.CreateEntityRetriever();

                return _queryById ? ExecuteQueryById(retriever) : ExecuteSearchQuery(retriever);
            }
            finally
            {
                try
                {
                    activityController.EndCurrentActivity();
                    ActivityControllerPooler.ReleaseController(activityController);
                }
                catch
                {
                    // Can't fix this.
                }
            }
        }

        private IList<Entity> ExecuteQueryById(EntityRetriever retriever)
        {
            var one = retriever.Retrieve(_entityClassRef + "Id", _id);
            return new List<Entity> { one };
        }

        private IList<Entity> ExecuteSearchQuery(EntityRetriever retriever)
        {
            var one = retriever.Retrieve(_entityClassRef + "Id", _id);
            return new List<Entity> { one };
        }
    }
}