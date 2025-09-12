namespace DSTN.Application.Helpers
{
    public interface IPagerParams
    {
        public int RecordsPerPage { set; get; }
        public int CurrentPage { set; get; }
    }
}
