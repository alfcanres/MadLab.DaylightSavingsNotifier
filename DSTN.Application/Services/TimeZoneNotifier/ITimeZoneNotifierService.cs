using DSTN.Application.DTO;
using DSTN.Application.Helpers;

namespace DSTN.Application.Services.TimeZoneNotifier
{
    public interface ITimeZoneNotifierService
    {
        /// <summary>
        //Step 1: Update DST start date and DST end date for all observed time zones within the specified date range.
        //usually this will be called once a day.
        /// </summary>
        /// <param name="today"></param>
        /// <returns></returns>
        Task<OperationResult<IEnumerable<ObservedTimeZoneDTO>>> UpdateDSTForObservedTimeZones(DateTime today);

        /// <summary>
        //Step 2: Scan all observed time zones for the next change in DST in order to create notifications.
        //this will be called periodically, e.g., once a day.
        /// </summary>
        /// <param name="today"></param>
        /// <returns></returns>
        Task<OperationResult<IEnumerable<ObservedTimeZoneDTO>>> ScanTimeZonesForNotification(DateTime today);

        /// <summary>
        //Step 3: Create a notification for the next change in DST for the specified observed time zone.
        //the notification date will be taking in consideration how many days in advance the user wants to be notified.
        /// </summary>
        /// <param name="ObservedTimeZoneId"></param>
        /// <returns></returns>
        Task<OperationResult<NotificationReadDTO>> CreateNotificationAsync(int ObservedTimeZoneId);

        /// <summary>
        /// Step 4: Send email notifications to users for the upcoming DST change.
        /// </summary>
        /// <param name="notificationDto"></param>
        /// <returns></returns>
        Task<OperationResult<Dictionary<string, bool>>> SendEmailNotificationAsync(NotificationReadDTO notificationDto);

        Task<OperationResult<NotificationReadDTO>> GetNotificationByIdAsync(int id);
        Task<OperationResult<PagedList<NotificationReadDTO>>> ListNotificationsAsync(NotificationListParamsDTO listParametersDTO);
        Task<OperationResult<NotificationReadDTO>> MarkNotificationAsReadAsync(int id);
        Task<OperationResult<int>> CountUnreadNotifications();

        Task<OperationResult<IEnumerable<EmailTimeZoneNotificationDTO>>> GetEmailsToNotifyAsync();

        Task<OperationResult<Dictionary<string, bool>>> SendEmailForTimezoneSummary(EmailTimeZoneNotificationDTO emailToNotify);

    }
}
