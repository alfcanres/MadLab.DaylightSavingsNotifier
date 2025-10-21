namespace DSTN.Domain.Interfaces
{
    public interface IRepository<TEntity>
    {
        Task<bool> AnyAsync(IQueryable<TEntity> query);
        Task<int> CountAsync(IQueryable<TEntity> query);
        Task<IEnumerable<TEntity>> ToListAsync(IQueryable<TEntity> query);
        Task <TEntity?> FirstOrDefaultAsync(IQueryable<TEntity> query);   
        
        IQueryable<TEntity> Query();
        IQueryable<TEntity> QueryInclude(IEnumerable<String> navigationProperties);
        IQueryable<TEntity> QueryInclude(string navigationProperty);

        Task<TEntity> GetByIdAsync(int id);
        Task InsertAsync(TEntity entity);
        void SetForInsert(TEntity entity);
        Task DeleteAsync(int id);
        void SetForUpdate(TEntity entity);
        Task UpdateAsync(TEntity entity);
    }
}
