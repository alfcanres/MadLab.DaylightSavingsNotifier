namespace DSTN.Application.Mappers
{
    public interface IMapper
    {
        TTarget Map<TSource, TTarget>(TSource source);
    }

}
