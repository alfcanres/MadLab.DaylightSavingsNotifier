using DSTN.AdminApp.WinForms.Forms.Notifications;
using DSTN.AdminApp.WinForms.Interfaces;
using DSTN.AdminApp.WinForms.ViewModels;
using DSTN.AdminApp.WinForms.ViewModels.Notifications;
using DSTN.AdminApp.WinForms.ViewModels.TimeZones;


namespace DSTN.AdminApp.WinForms.TimeZones
{
    public interface IListNotifications : IPagedListForm
    {
        IEnumerable<Notification> Notifications { get; set; }

        IEditNotification EditorForm { get; set; }

        IEnumerable<ItemForCombo> ObservedTimeZones { get; set; }

        int SelectedTimeZoneId { get; set; }

        bool ShowAll { get; set; }
        bool ShowSeen { get; set; }
        bool ShowNotSeen { get; set; }

        public int NotSeenNotificationsCount { get; set; }



    }
}
