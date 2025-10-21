namespace DSTN.Domain.Interfaces
{
    public interface IQueryBuilder<TEntitty> where TEntitty : class
    {
        Task<int> CountAsync();
        IQueryBuilder<TEntitty> AddFilter(IQueryFilter<TEntitty> queryFilter);
        IQueryBuilder<TEntitty> AddPaging(int pageNumber, int pageSize);
        Task<IEnumerable<TEntitty>> GetListAsync();
        void Include(IEnumerable<string> navigationProperties);
        void Include(string navigationProperty);
    }
}