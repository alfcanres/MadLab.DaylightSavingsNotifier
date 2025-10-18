using DSTN.Application.DTO;
using DSTN.Application.Helpers;

namespace DSTN.Application.Services.TimeZoneNotifier
{
    public interface ITimeZoneNotifierService
    {
        /// <summary>
        //Step 1: Update DST start date and DST end date for all observed time zones within the specified date range.
        //usually this will be called once a year, at the beginning of the year.
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        Task<OperationResult<IEnumerable<ObservedTimeZoneDTO>>> UpdateDSTForObservedTimeZones(int year);

        /// <summary>
        //Step 2: Scan all observed time zones for the next change in DST in order to create notifications.
        //this will be called periodically, e.g., once a day.
        //It will look for time zones that does not have a notification for the next change in DST.
        //for this year.
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
        Task<OperationResult<IEnumerable<NotificationReadDTO>>> CreateNotificationAsync(ObservedTimeZoneDTO observedTimeZone);

        /// <summary>
        /// Step 4: Get all due or overdue notifications that have not been read yet.
        /// </summary>
        /// <param name="today"></param>
        /// <returns></returns>
        Task<OperationResult<IEnumerable<NotificationReadDTO>>> GetDueOrOverdueNotificationsAsync(DateTime today);


        Task<OperationResult<NotificationReadDTO>> GetNotificationByIdAsync(int id);
        Task<OperationResult<PagedList<NotificationReadDTO>>> ListNotificationsAsync(NotificationListParamsDTO listParametersDTO);
        Task<OperationResult<NotificationReadDTO>> MarkNotificationAsReadAsync(int id);

        Task<OperationResult<int>> CountUnreadNotifications(); 
    }
}
