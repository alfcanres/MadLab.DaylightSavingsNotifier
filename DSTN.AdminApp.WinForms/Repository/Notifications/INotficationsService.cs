using DSTN.AdminApp.WinForms.ViewModels;
using DSTN.AdminApp.WinForms.ViewModels.Notifications;
using DSTN.AdminApp.WinForms.ViewModels.TimeZones;


namespace DSTN.AdminApp.WinForms.Repository.Notifications
{
    public interface INotficationsService
    {
        Task<ServiceResult<NotificationRead>> GetNotificationByIdAsync(int id);
        Task<ServiceResult<PagedListResponse<NotificationRead>>> ListNotificationsAsync(NotificationListParams listParametersVM);
        Task<ServiceResult<NotificationRead>> MarkNotificationAsReadAsync(int id);
    }
}
