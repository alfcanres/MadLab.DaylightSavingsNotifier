using DSTN.AdminApp.WinForms.Interfaces;
using DSTN.Application.DTO;


namespace DSTN.AdminApp.WinForms.TimeZones
{
    public interface IListNotifications : IListForm
    {
        IEnumerable<NotificationReadDTO> TimeZones { get; set; }
        NotificationListParamsDTO FilterParams { set; get; }
        IViewNotification EditorForm { get; set; }

        IEnumerable<string> Filters { get; set; }

        string SelectedFilter { get; set; }

    }
}
