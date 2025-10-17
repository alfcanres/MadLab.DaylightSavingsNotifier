namespace DSTN.AdminApp.WinForms.Interfaces
{
    public interface IListForm
    {
        void ShowPager();
        void HidePager();
        string Title { get; set; }
        int SelectedId { get;}
        void ShowErrors(IEnumerable<string> errors);
        void ShowErrors(string error);
        void ShowAlert(string alert);
        void ShowLoading(string message = "");
        void HideLoading();
        bool CloseForm();
        void HideEditor();
        bool ConfirmDelete(string alert);
    }
}
