using DSTN.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;


namespace DSTN.Infrastructure.Persistence
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        private readonly AppDbContext _appDbContext;
        private readonly DbSet<TEntity> _dbSet;

        public Repository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _dbSet = _appDbContext.Set<TEntity>();
        }

        public async Task<bool> AnyAsync(IQueryable<TEntity> query)
        {
            return await query.AnyAsync();
        }

        public async Task<int> CountAsync(IQueryable<TEntity> query)
        {
            return await query.CountAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _appDbContext.SaveChangesAsync();
            }
        }

        public async Task<TEntity?> FirstOrDefaultAsync(IQueryable<TEntity> query)
        {
            return await query.FirstOrDefaultAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);

            return entity;
        }

        public async Task InsertAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            await _appDbContext.SaveChangesAsync();
        }

        public IQueryable<TEntity> Query()
        {
            return _dbSet.AsQueryable();
        }

        public IQueryable<TEntity> QueryInclude(IEnumerable<string> navigationProperties)
        {
            if (!navigationProperties.Any())
                throw new Exception("Must provide list of navigation properties or use Query() method instead");

            string navProps = "";
            foreach (var navigationProperty in navigationProperties)
            {
                navProps += navigationProperty + ".";
            }
            navProps = navProps.TrimEnd('.');

            return _dbSet.Include(navProps).AsQueryable();
        }

        public IQueryable<TEntity> QueryInclude(string navigationProperty)
        {
            if (string.IsNullOrEmpty(navigationProperty))
                throw new Exception("Must provide a navigation property or use Query() method instead");

            return _dbSet.Include(navigationProperty).AsQueryable();
        }

        public void SetForInsert(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public void SetForUpdate(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        public async Task<IEnumerable<TEntity>> ToListAsync(IQueryable<TEntity> query)
        {
            return await query.ToListAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _dbSet.Update(entity);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
