using DSTN.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace DSTN.Infrastructure.Persistence.Helpers
{
    public class QueryBuilder<Entitty> : IQueryBuilder<Entitty> where Entitty : class
    {

        private WithPaging<Entitty> _withPaging;
        private IQueryable<Entitty> _query;
        private List<IQueryFilter<Entitty>> _queryFilters = new List<IQueryFilter<Entitty>>();

        public QueryBuilder(AppDbContext appDbContext)
        {
            _query = appDbContext.Set<Entitty>().AsQueryable();
        }

        public IQueryBuilder<Entitty> AddFilter(IQueryFilter<Entitty> queryFilter)
        {

            if (_queryFilters.Any(f => f.GetType().Name == queryFilter.GetType().Name))
            {
                throw new InvalidOperationException($"Filter {queryFilter.GetType().Name} has already been added.");
            }

            _queryFilters.Add(queryFilter);

            _query = queryFilter.ApplyFilter(_query);

            return this;
        }

        public async Task<IEnumerable<Entitty>> GetListAsync()
        {

            if (_withPaging != null)
            {
                _query = _withPaging.GetPaged(_query);
            }

            return await _query.AsNoTracking().ToListAsync();
        }

        public IQueryBuilder<Entitty> AddPaging(int pageNumber, int pageSize)
        {
            if (_withPaging != null)
            {
                throw new InvalidOperationException("Paging has already been set. You cannot set it again.");
            }

            _withPaging = new WithPaging<Entitty>(pageNumber, pageSize);

            return this;
        }

        public async Task<int> CountAsync()
        {
            return await _query.CountAsync();
        }

        public void Include(IEnumerable<string> navigationProperties)
        {
            if (!navigationProperties.Any())
                throw new Exception("Must provide list of navigation properties");

            string navProps = "";
            foreach (var navigationProperty in navigationProperties)
            {
                navProps += navigationProperty + ".";
            }
            navProps = navProps.TrimEnd('.');

            _query = _query.Include(navProps);

        }

        public void Include(string navigationProperty)
        {
            if (string.IsNullOrEmpty(navigationProperty))
                throw new Exception("Must provide a navigation property");

            _query = _query.Include(navigationProperty);

        }


        public class WithPaging<T>
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
}