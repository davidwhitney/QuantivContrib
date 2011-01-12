namespace QuantivContrib.Core.FluentQuantiv
{
    public class FluentQueryBuilder : IQueryBuilderLoadType, IQuantivEntityIdentifierQueryBuilder, IBuilderSearchConditions,ILookupTargetBuilder
    {
        private readonly QuantivDatabase _quantivDatabase;
        private bool _queryById;
        private int _id;

        public FluentQueryBuilder(QuantivDatabase quantivDatabase)
        {
            _quantivDatabase = quantivDatabase;
        }

        FluentQueryBuilder IQueryBuilderLoadType.Id(int id)
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

        ILookupTargetBuilder IBuilderSearchConditions.AttributeRef(string condition)
        {
            return this;
        }

        public QuantivDatabase Fetch()
        {
            return _quantivDatabase;
        }

        IBuilderSearchConditions ILookupTargetBuilder.EqualTo<T>(T condition)
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
        FluentQueryBuilder Id(int id);
        IBuilderSearchConditions SearchConditions { get; }
    }

    public interface IBuilderSearchConditions
    {
        ILookupTargetBuilder AttributeRef(string condition);
        QuantivDatabase Fetch();
    }

    public interface ILookupTargetBuilder
    {
        IBuilderSearchConditions EqualTo<T>(T condition);
    }
}