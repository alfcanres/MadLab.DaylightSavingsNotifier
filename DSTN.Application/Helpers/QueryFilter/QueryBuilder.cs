

namespace DSTN.Application.Helpers.QueryFilter
{
    internal class QueryBuilder<Entitty> : IQueryBuilder<Entitty> where Entitty : class
    {

        private WithPaging<Entitty> _withPaging;
        private IQueryable<Entitty> _query;
        private List<IQueryFilter<Entitty>> _queryFilters = new List<IQueryFilter<Entitty>>();

        public QueryBuilder(IQueryable<Entitty> query)
        {
            _query = query ?? throw new ArgumentNullException(nameof(query), "Query cannot be null");
        }

        public QueryBuilder<Entitty> AddFilter(IQueryFilter<Entitty> queryFilter)
        {

            if (_queryFilters.Any(f => f.GetType().Name == queryFilter.GetType().Name))
            {
                throw new InvalidOperationException($"Filter {queryFilter.GetType().Name} has already been added.");
            }

            _queryFilters.Add(queryFilter);

            return this;
        }

        public async Task<IEnumerable<Entitty>> BuildAsync()
        {

            foreach (var filter in _queryFilters)
            {
                _query = filter.ApplyFilter(_query);
            }

            if (_withPaging != null)
            {
                _query = _withPaging.GetPaged(_query);
            }

            return await _query.ToListAsync();
        }

        public QueryBuilder<Entitty> AddPaging(int pageNumber, int pageSize)
        {
            if (_withPaging != null)
            {
                throw new InvalidOperationException("Paging has already been set. You cannot set it again.");
            }

            _withPaging = new WithPaging<Entitty>(pageNumber, pageSize);

            return this;
        }

        internal async Task<int> CountAsync()
        {
            return await _query.CountAsync();
        }
    }

    internal class WithPaging<T>
    {

        public WithPaging(int pageNumber = 1, int pageSize = 10)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public IQueryable<T> GetPaged(IQueryable<T> queryable)
        {
            int skip = (PageNumber - 1) * PageSize;

            return queryable.Skip((PageNumber - 1) * PageSize).Take(PageSize);
        }
    }
}
