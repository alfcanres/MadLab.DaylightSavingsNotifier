namespace DSTN.AdminApp.WinForms.Interfaces
{
    public interface IListForm
    {
        void ShowPager();
        void HidePager();
        int RecordsPerPage { get; set; }
        int CurrentPage { get; set; }
        string SearchKeyWord { get; set; }
        string PageCount { get; set; }
        string Title { get; set; }
        int SelectedId { get; set; }
        void ShowError();
        List<String> ValidationErrors { get; set; }
        void ShowAlert(string alert);
        void ShowLoading(string message = "");
        void HideLoading();
        bool CloseForm();

        void ShowEditor(int id = 0);
        void HideEditor();
    }
}
