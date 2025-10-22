using DSTN.AdminApp.WinForms.Interfaces;


namespace DSTN.AdminApp.WinForms.Forms.Notifications
{
    public interface IEditNotification : IEditorForm
    {
        int Id { get; set; }

        string NextTransition { set; get; }

        string Message { set; get; }

        string TimeZoneColor { set; get; }

        string TimeZoneDisplayName { set; get; }

        void Show();
    }
}
