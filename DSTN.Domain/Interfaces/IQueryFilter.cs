namespace DSTN.Domain.Interfaces
{
    public interface IQueryFilter<T> where T : class
    {
        IQueryable<T> ApplyFilter(IQueryable<T> queryable);

    }
}
