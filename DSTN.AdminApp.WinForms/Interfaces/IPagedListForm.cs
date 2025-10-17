namespace DSTN.AdminApp.WinForms.Interfaces
{
    public interface IPagedListForm : IListForm
    {
        int RecordsPerPage { get; set; }
        int CurrentPage { get; set; }
        string SearchKeyWord { get; set; }
        int PageCount { get; set; }
        int TotalRecords { set; get; }
    }
}
