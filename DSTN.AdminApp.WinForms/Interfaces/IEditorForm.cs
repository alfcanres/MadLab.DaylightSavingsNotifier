namespace DSTN.AdminApp.WinForms.Interfaces
{
    public interface IEditorForm
    {
        string Title { get; set; }
        int Id { get; set; }
        void ShowErrors(IEnumerable<string> errors);
        void ShowErrors(string error);
        void ShowAlert(string alert);
        bool ConfirmDelete(string alert);
        void ShowLoading(string message = "");
        void HideLoading();
        void CloseForm();
        void ShowForm();
        bool ShowSaveButtom { get; set; }
        bool ShowDeleteButton { get; set; }

    }
}
