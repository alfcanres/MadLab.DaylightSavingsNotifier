using DSTN.AdminApp.WinForms.ViewModels;
using DSTN.AdminApp.WinForms.ViewModels.Notifications;


namespace DSTN.AdminApp.WinForms.Repository.Notifications
{
    public interface INotficationsService
    {
        Task<ServiceResult<Notification>> GetNotificationByIdAsync(int id);
        Task<ServiceResult<PagedListResponse<Notification>>> ListNotificationsAsync(NotificationListParams listParametersVM);
        Task<ServiceResult<Notification>> MarkNotificationAsReadAsync(int id);
    }
}
