
namespace DSTN.Application.Helpers.QueryFilter
{
    public interface IQueryBuilder<Entitty> where Entitty : class
    {
        Task<int> CountAsync();
        IQueryBuilder<Entitty> AddFilter(IQueryFilter<Entitty> queryFilter);
        IQueryBuilder<Entitty> AddPaging(int pageNumber, int pageSize);
        Task<IEnumerable<Entitty>> BuildAsync();
    }
}