using DSTN.AdminApp.WinForms.Interfaces;


namespace DSTN.AdminApp.WinForms.Forms.Notifications
{
    public interface IEditNotification : IEditorForm
    {
        int Id { get; set; }
        int TimeZoneId { get; set; }
        string TimeZoneDisplayName { get; set; }
        string DSTTransition { get; set; }
        string NotifyDate { get; set; }
        string Message { get; set; }
        string CreatedAt { get; set; }
        bool WasRead { get; set; }
        string ReadAt { get; set; }
        void Show();
    }
}
