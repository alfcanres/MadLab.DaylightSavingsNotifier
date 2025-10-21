using DSTN.Application.DTO;
using DSTN.Application.Helpers;
using DSTN.Application.Services.TimeZoneNotifier.Filters;
using DSTN.Domain.Entities;
using DSTN.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace DSTN.Application.Services.TimeZoneNotifier
{
    public class TimeZoneNotifierService :
        BaseService,
        ITimeZoneNotifierService
    {
        private readonly IQueryBuilder<Notification> _queryBuilder;
        private readonly ISystemTimeZoneProvider _systemTimeZoneProvider;
        public TimeZoneNotifierService(
            IUnitOfWork unitOfWork,
            ILogger<TimeZoneNotifierService> logger,
            IQueryBuilder<Notification> queryBuilder,
            ISystemTimeZoneProvider systemTimeZoneProvider)
            : base(unitOfWork, logger)
        {
            _queryBuilder = queryBuilder;

            _queryBuilder.Include("TimeZone");

            _systemTimeZoneProvider = systemTimeZoneProvider;
        }

        public async Task<OperationResult<NotificationReadDTO>> GetNotificationByIdAsync(int id)
        {
            Validator.Clear();
            try
            {
                var query = UnitOfWork.Notifications.QueryInclude("TimeZone");

                var notification = await UnitOfWork.Notifications.FirstOrDefaultAsync(query);

                if (notification == null)
                {
                    Validator.AddError($"Notification with ID {id} not found.");
                    return new OperationResult<NotificationReadDTO>
                    {
                        Data = null,
                        ValidatorResponse = Validator.CrateNewCopy()
                    };
                }

                var dto = NotificationReadDTO.FromEntity(notification);

                return new OperationResult<NotificationReadDTO>
                {
                    Data = dto,
                    ValidatorResponse = Validator.CrateNewCopy()
                };
            }
            catch (Exception ex)
            {
                Validator.AddError($"Unexpected error: {ex.Message}");
                _logger.LogError(ex, "Error in GetNotificationByIdAsync");
                Validator.IsValid = false;
                return new OperationResult<NotificationReadDTO>
                {
                    Data = null,
                    ValidatorResponse = Validator.CrateNewCopy()
                };
            }
        }
        public async Task<OperationResult<PagedList<NotificationReadDTO>>> ListNotificationsAsync(NotificationListParamsDTO listParametersDTO)
        {
            Validator.Clear();
            try
            {


                if (listParametersDTO.ObservedTimeZoneId.HasValue)
                    _queryBuilder.AddFilter(new TimeZoneIdFilter(listParametersDTO.ObservedTimeZoneId.Value));

                if (listParametersDTO.WasRead.HasValue)
                    _queryBuilder.AddFilter(new SeenFilter(listParametersDTO.WasRead.Value));


                int totalRecords = await _queryBuilder.CountAsync();


                _queryBuilder.AddPaging(listParametersDTO.CurrentPage, listParametersDTO.RecordsPerPage);

                var result = await _queryBuilder.GetListAsync();

                var pagedList = new PagedList<NotificationReadDTO>(
                    result.Select(n => NotificationReadDTO.FromEntity(n)),
                    totalRecords,
                    listParametersDTO
                );

                return new OperationResult<PagedList<NotificationReadDTO>>
                {
                    Data = pagedList,
                    ValidatorResponse = Validator.CrateNewCopy()
                };
            }
            catch (Exception ex)
            {
                Validator.AddError($"Unexpected error: {ex.Message}");
                _logger.LogError(ex, "Error in ListNotificationsAsync");
                Validator.IsValid = false;
                return new OperationResult<PagedList<NotificationReadDTO>>
                {
                    Data = new PagedList<NotificationReadDTO>(new List<NotificationReadDTO>(), 0, listParametersDTO),
                    ValidatorResponse = Validator.CrateNewCopy()
                };
            }
        }
        public async Task<OperationResult<NotificationReadDTO>> MarkNotificationAsReadAsync(int id)
        {
            Validator.Clear();
            try
            {
                var query = UnitOfWork.Notifications.QueryInclude("TimeZone");



                var notification = await UnitOfWork.Notifications.FirstOrDefaultAsync(query);

                if (notification == null)
                {
                    Validator.AddError($"Notification with ID {id} not found.");
                    return new OperationResult<NotificationReadDTO>
                    {
                        Data = null,
                        ValidatorResponse = Validator
                    };
                }

                if (!notification.WasRead)
                {
                    notification.WasRead = true;
                    notification.ReadAt = DateTime.UtcNow;
                    await UnitOfWork.Notifications.UpdateAsync(notification);
                }

                var dto = NotificationReadDTO.FromEntity(notification);
                return new OperationResult<NotificationReadDTO>
                {
                    Data = dto,
                    ValidatorResponse = Validator.CrateNewCopy()
                };
            }
            catch (Exception ex)
            {
                Validator.AddError($"Unexpected error: {ex.Message}");
                _logger.LogError(ex, "Error in MarkNotificationAsReadAsync");
                Validator.IsValid = false;
                return new OperationResult<NotificationReadDTO>
                {
                    Data = null,
                    ValidatorResponse = Validator.CrateNewCopy()
                };
            }
        }
        public async Task<OperationResult<IEnumerable<ObservedTimeZoneDTO>>> UpdateDSTForObservedTimeZones(DateTime today)
        {
            Validator.Clear();
            var result = new List<ObservedTimeZoneDTO>();
            try
            {


                var observedTimeZonesQuery = UnitOfWork.ObservedTimeZones
                    .Query()
                    .Where(tz => tz.IsActive);

                var observedTimeZones = await UnitOfWork.ObservedTimeZones.ToListAsync(observedTimeZonesQuery);

                foreach (var tz in observedTimeZones)
                {

                    if (!_systemTimeZoneProvider.SupportsDaylightSavingTime(tz.TimeZoneId, today.Year))
                    {
                        tz.TimeZoneObservesDST = false;
                        tz.DSTStarts = null;
                        tz.DSTEnds = null;
                        tz.LastChanged = DateTime.UtcNow;
                        await UnitOfWork.ObservedTimeZones.UpdateAsync(tz);
                    }
                    else
                    {
                        tz.TimeZoneObservesDST = true;
                        tz.DSTStarts = _systemTimeZoneProvider.GetDSTTransitionDate(today.Year, tz.TimeZoneId, true);
                        tz.DSTEnds = _systemTimeZoneProvider.GetDSTTransitionDate(today.Year, tz.TimeZoneId, false);
                        tz.NextTransitionDate = _systemTimeZoneProvider.GetNextTransitionDate(today, tz.TimeZoneId);

                        if (tz.NextTransitionDate.HasValue)
                            tz.NextNotificationDate = tz.NextTransitionDate.Value.AddDays(-tz.NotifyDaysBefore);

                        tz.LastChanged = DateTime.UtcNow;

                        await UnitOfWork.ObservedTimeZones.UpdateAsync(tz);
                    }
                    var dto = ObservedTimeZoneDTO.FromEntity(tz);
                    result.Add(dto);
                }

                await UnitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                Validator.AddError($"Unexpected error: {ex.Message}");
                _logger.LogError(ex, "Unexpected error in UpdateDSTForObservedTimeZones");
                Validator.IsValid = false;
            }

            return new OperationResult<IEnumerable<ObservedTimeZoneDTO>>()
            {
                Data = result,
                ValidatorResponse = Validator.CrateNewCopy()
            };
        }
        public async Task<OperationResult<IEnumerable<ObservedTimeZoneDTO>>> ScanTimeZonesForNotification(DateTime today)
        {
            Validator.Clear();
            var result = new List<ObservedTimeZoneDTO>();

            var observedTimeZonesQuery = UnitOfWork.ObservedTimeZones
                .Query()
                .Where(tz => tz.IsActive && tz.TimeZoneObservesDST && tz.NextNotificationDate <= today);

            var observedTimeZones = await UnitOfWork.ObservedTimeZones.ToListAsync(observedTimeZonesQuery);

            foreach (var currentTz in observedTimeZones)
            {

                DateTime nextNotificationDate = currentTz.NextNotificationDate!.Value;

                var countNotificationsForTransitionsQuery = UnitOfWork.Notifications
                    .Query()
                    .Where(n =>
                            n.TimeZoneId == currentTz.Id
                            &&
                            (
                            n.NotifyDate.Month == nextNotificationDate.Month
                            &&
                            n.NotifyDate.Month == nextNotificationDate.Day
                            &&
                            n.NotifyDate.Month == nextNotificationDate.Year
                            )
                    );
                int countNotificationsForTransitions = await UnitOfWork.Notifications.CountAsync(countNotificationsForTransitionsQuery);

                if (countNotificationsForTransitions == 0)
                {
                    result.Add(ObservedTimeZoneDTO.FromEntity(currentTz));
                }
            }

            return new OperationResult<IEnumerable<ObservedTimeZoneDTO>>
            {
                Data = result,
                ValidatorResponse = Validator.CrateNewCopy(),
            };
        }
        public async Task<OperationResult<NotificationReadDTO>> CreateNotificationAsync(int observedTimeZoneId)
        {
            Validator.Clear();
            var notificationDTO = new NotificationReadDTO();
            try
            {
                var observedTimeZone = await UnitOfWork.ObservedTimeZones.GetByIdAsync(observedTimeZoneId);

                var notification = new Notification()
                {
                    WasRead = false,
                    TimeZoneId = observedTimeZone.Id,
                    CreatedAt = DateTime.UtcNow,
                    DSTTransition = observedTimeZone.NextTransitionDate!.Value,
                    Message = observedTimeZone.Comments is not null ? observedTimeZone.Comments : observedTimeZone.DisplayName,
                    NotifyDate = DateTime.UtcNow,
                    ReadAt = DateTime.UtcNow,
                    TimeZone = observedTimeZone
                };

                await UnitOfWork.Notifications.InsertAsync(notification);

                notificationDTO = NotificationReadDTO.FromEntity(notification);

            }
            catch (Exception ex)
            {
                Validator.AddError($"Unexpected error: {ex.Message}");
                _logger.LogError(ex, $"Unable to create notification for TimeZone with ID {observedTimeZoneId}");
                Validator.IsValid = false;
            }
            return new OperationResult<NotificationReadDTO>
            {
                Data = notificationDTO,
                ValidatorResponse = Validator.CrateNewCopy(),
            };

        }
        public async Task<OperationResult<int>> CountUnreadNotifications()
        {
            Validator.Clear();
            try
            {

                var count = UnitOfWork.Notifications.Query()
                    .Where(t => t.WasRead == false).Count();

                return new OperationResult<int>
                {
                    Data = count,
                    ValidatorResponse = Validator.CrateNewCopy()
                };
            }
            catch (Exception ex)
            {
                Validator.AddError($"Unexpected error: {ex.Message}");
                _logger.LogError(ex, "Error in ListNotificationsAsync");
                Validator.IsValid = false;
                return new OperationResult<int>
                {
                    Data = 0,
                    ValidatorResponse = Validator.CrateNewCopy()
                };
            }
        }
    }
}
