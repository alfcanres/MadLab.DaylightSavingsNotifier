using DSTN.AdminApp.WinForms.Forms.Notifications;
using DSTN.AdminApp.WinForms.Interfaces;
using DSTN.AdminApp.WinForms.ViewModels.Notifications;


namespace DSTN.AdminApp.WinForms.TimeZones
{
    public interface IListNotifications : IPagedListForm
    {
        IEnumerable<Notification> Notifications { get; set; }

        IEditNotification EditorForm { get; set; }

        IEnumerable<string> Filters { get; set; }

        string SelectedFilter { get; set; }

    }
}
