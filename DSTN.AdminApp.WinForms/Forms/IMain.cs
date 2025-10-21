
namespace DSTN.AdminApp.WinForms.Forms
{
    public interface IMain
    {
        string NotificationsText { set; get; }
        string NotificationsTitleMenu { set; get; }
        int PendingNotifications {  set; get; }
    }
}