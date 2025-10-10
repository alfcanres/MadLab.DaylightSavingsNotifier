namespace DSTN.AdminApp.WinForms.ViewModels
{
    public record PagedListResponse<T>(
        IEnumerable<T> List, 
        int RecordCount, 
        int CurrentPage, 
        int PageCount, 
        int RecordsPerPage) where T : class;
}