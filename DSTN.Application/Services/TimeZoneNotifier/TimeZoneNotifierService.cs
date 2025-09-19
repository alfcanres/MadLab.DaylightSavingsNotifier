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
            _systemTimeZoneProvider = systemTimeZoneProvider;
        }

        public async Task<OperationResult<NotificationReadDTO>> GetNotificationByIdAsync(int id)
        {
            Validator.Clear();
            try
            {
                var notification = await UnitOfWork.Notifications.GetByIdAsync(id);
                if (notification == null)
                {
                    Validator.AddError($"Notification with ID {id} not found.");
                    return new OperationResult<NotificationReadDTO>
                    {
                        Result = null,
                        ValidatorResponse = Validator
                    };
                }

                var dto = NotificationReadDTO.FromEntity(notification);
                return new OperationResult<NotificationReadDTO>
                {
                    Result = dto,
                    ValidatorResponse = Validator
                };
            }
            catch (Exception ex)
            {
                Validator.AddError($"Unexpected error: {ex.Message}");
                _logger.LogError(ex, "Error in GetNotificationByIdAsync");
                Validator.IsValid = false;
                return new OperationResult<NotificationReadDTO>
                {
                    Result = null,
                    ValidatorResponse = Validator
                };
            }
        }
        public async Task<OperationResult<PagedList<NotificationReadDTO>>> ListNotificationsAsync(NotificationListParamsDTO listParametersDTO)
        {
            Validator.Clear();
            try
            {


                if (listParametersDTO.ObservedTimeZoneId > 0)
                    _queryBuilder.AddFilter(new TimeZoneIdFilter(listParametersDTO.ObservedTimeZoneId));

                if (listParametersDTO.StartDate.HasValue || listParametersDTO.EndDate.HasValue)
                    _queryBuilder.AddFilter(new DateRangeFilter(
                        listParametersDTO.StartDate!.Value,
                        listParametersDTO.EndDate!.Value
                    ));

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
                    Result = pagedList,
                    ValidatorResponse = Validator
                };
            }
            catch (Exception ex)
            {
                Validator.AddError($"Unexpected error: {ex.Message}");
                _logger.LogError(ex, "Error in ListNotificationsAsync");
                Validator.IsValid = false;
                return new OperationResult<PagedList<NotificationReadDTO>>
                {
                    Result = new PagedList<NotificationReadDTO>(new List<NotificationReadDTO>(), 0, listParametersDTO),
                    ValidatorResponse = Validator
                };
            }
        }
        public async Task<OperationResult<NotificationReadDTO>> MarkNotificationAsReadAsync(int id)
        {
            Validator.Clear();
            try
            {
                var notification = await UnitOfWork.Notifications.GetByIdAsync(id);
                if (notification == null)
                {
                    Validator.AddError($"Notification with ID {id} not found.");
                    return new OperationResult<NotificationReadDTO>
                    {
                        Result = null,
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
                    Result = dto,
                    ValidatorResponse = Validator
                };
            }
            catch (Exception ex)
            {
                Validator.AddError($"Unexpected error: {ex.Message}");
                _logger.LogError(ex, "Error in MarkNotificationAsReadAsync");
                Validator.IsValid = false;
                return new OperationResult<NotificationReadDTO>
                {
                    Result = null,
                    ValidatorResponse = Validator
                };
            }
        }
        public async Task<OperationResult<IEnumerable<ObservedTimeZoneDTO>>> UpdateDSTForObservedTimeZones(int year)
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

                    if (_systemTimeZoneProvider.SupportsDaylightSavingTime(tz.TimeZoneId, year))
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
                        tz.DSTStarts = _systemTimeZoneProvider.GetDSTTransitionDate(year, tz.TimeZoneId, true);
                        tz.DSTEnds = _systemTimeZoneProvider.GetDSTTransitionDate(year, tz.TimeZoneId, false);
                        tz.LastChanged = DateTime.UtcNow;                        

                        await UnitOfWork.ObservedTimeZones.UpdateAsync(tz);
                    }

                    var dto = ObservedTimeZoneDTO.FromEntity(tz);
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
                Result = result,
                ValidatorResponse = Validator
            };
        }
        public async Task<OperationResult<IEnumerable<ObservedTimeZoneDTO>>> ScanTimeZonesForNotification(DateTime today)
        {
            Validator.Clear();
            var result = new List<ObservedTimeZoneDTO>();

            var observedTimeZonesQuery = UnitOfWork.ObservedTimeZones
                .Query()
                .Where(tz => tz.IsActive && tz.TimeZoneObservesDST);

            var observedTimeZones = await UnitOfWork.ObservedTimeZones.ToListAsync(observedTimeZonesQuery);

            foreach (var currentTz in observedTimeZones)
            {

                var countNotificationsForTransitionsQuery = UnitOfWork.Notifications
                    .Query()
                    .Where(n =>
                            n.TimeZoneId == currentTz.Id
                            &&
                            (
                            n.DSTTransition == currentTz.DSTStarts
                            ||
                            n.DSTTransition == currentTz.DSTEnds
                            )
                    );
                int countNotificationsForTransitions = await UnitOfWork.Notifications.CountAsync(countNotificationsForTransitionsQuery);

                if (countNotificationsForTransitions != 2)
                {
                    result.Add(ObservedTimeZoneDTO.FromEntity(currentTz));
                }
            }

            return new OperationResult<IEnumerable<ObservedTimeZoneDTO>>
            {
                Result = result,
                ValidatorResponse = Validator,
            };
        }
        public async Task<OperationResult<IEnumerable<NotificationReadDTO>>> CreateNotificationAsync(ObservedTimeZoneDTO observedTimeZone)
        {

            Validator.Clear();
            var result = new List<NotificationReadDTO>();

            var queryNotificationsQry = UnitOfWork.Notifications
                .Query()
                .Where(n => n.TimeZoneId == observedTimeZone.Id);

            var queryNotifications = await UnitOfWork.Notifications.ToListAsync(queryNotificationsQry);


            if (queryNotifications.Count() == 0)
            {
                Notification notifyDSTStarts = new Notification();
                ConfigureNotification(
                    notifyDSTStarts,
                    ObservedTimeZoneDTO.ToEntity(observedTimeZone),
                    observedTimeZone.DSTStarts,
                    observedTimeZone.NotifyDaysBefore,
                    true);

                Notification notifyDSTEnds = new Notification();
                ConfigureNotification(
                    notifyDSTEnds,
                    ObservedTimeZoneDTO.ToEntity(observedTimeZone),
                    observedTimeZone.DSTEnds,
                    observedTimeZone.NotifyDaysBefore,
                    false);


                await UnitOfWork.Notifications.InsertAsync(notifyDSTStarts);

                var notifyDSTStartsDTO = NotificationReadDTO.FromEntity(notifyDSTStarts);
                result.Add(notifyDSTStartsDTO);
                var notifyDSTEndsDTO = NotificationReadDTO.FromEntity(notifyDSTEnds);
                result.Add(notifyDSTEndsDTO);
            }
            else
            {
                var notifyDSTStarts = queryNotifications.FirstOrDefault(n => n.DSTTransition == observedTimeZone.DSTStarts);
                var notifyDSTEnds = queryNotifications.FirstOrDefault(n => n.DSTTransition == observedTimeZone.DSTEnds);
                if (notifyDSTStarts == null)
                {
                    notifyDSTStarts = new Notification();
                    ConfigureNotification(
                        notifyDSTStarts,
                        ObservedTimeZoneDTO.ToEntity(observedTimeZone),
                        observedTimeZone.DSTStarts,
                        observedTimeZone.NotifyDaysBefore,
                        true);
                    await UnitOfWork.Notifications.InsertAsync(notifyDSTStarts);
                }
                else
                {
                    ConfigureNotification(
                        notifyDSTStarts,
                        ObservedTimeZoneDTO.ToEntity(observedTimeZone),
                        observedTimeZone.DSTStarts,
                        observedTimeZone.NotifyDaysBefore,
                        true);
                    await UnitOfWork.Notifications.UpdateAsync(notifyDSTStarts);
                }

                if (notifyDSTEnds == null)
                {
                    notifyDSTEnds = new Notification();
                    ConfigureNotification(
                        notifyDSTEnds,
                        ObservedTimeZoneDTO.ToEntity(observedTimeZone),
                        observedTimeZone.DSTEnds,
                        observedTimeZone.NotifyDaysBefore,
                        false);
                    await UnitOfWork.Notifications.InsertAsync(notifyDSTEnds);
                }
                else
                {
                    ConfigureNotification(
                        notifyDSTEnds,
                        ObservedTimeZoneDTO.ToEntity(observedTimeZone),
                        observedTimeZone.DSTEnds,
                        observedTimeZone.NotifyDaysBefore,
                        false);
                    await UnitOfWork.Notifications.UpdateAsync(notifyDSTEnds);
                }

            }

            return new OperationResult<IEnumerable<NotificationReadDTO>>
            {
                Result = result,
                ValidatorResponse = Validator,
            };

        }
        public async Task<OperationResult<IEnumerable<NotificationReadDTO>>> GetDueOrOverdueNotificationsAsync(DateTime today)
        {
            Validator.Clear();
            try
            {
                // Get notifications where NotifyDate is less than or equal to today and not read yet
                var dueNotificationsQry = UnitOfWork.Notifications
                    .Query()
                    .Where(n => n.NotifyDate <= today && !n.WasRead);

                var dueNotifications = await UnitOfWork.Notifications.ToListAsync(dueNotificationsQry);


                var result = dueNotifications
                    .Select(n => NotificationReadDTO.FromEntity(n))
                    .ToList();

                return new OperationResult<IEnumerable<NotificationReadDTO>>
                {
                    Result = result,
                    ValidatorResponse = Validator
                };
            }
            catch (Exception ex)
            {
                Validator.AddError($"Unexpected error: {ex.Message}");
                _logger.LogError(ex, "Error in GetDueOrOverdueNotificationsAsync");
                Validator.IsValid = false;
                return new OperationResult<IEnumerable<NotificationReadDTO>>
                {
                    Result = new List<NotificationReadDTO>(),
                    ValidatorResponse = Validator
                };
            }
        }


        #region Helper Methods

        private void ConfigureNotification(
            Notification notification,
            ObservedTimeZone observedTimeZone,
            DateTime? DSTTransition,
            int notifyDaysBefore,
            bool starts
            )
        {

            notification.TimeZoneId = observedTimeZone.Id;
            notification.DSTTransition = DSTTransition!.Value;
            notification.NotifyDate = _systemTimeZoneProvider.GetNotificationDate(DSTTransition, notifyDaysBefore)!.Value;
            if (starts)
            {
                notification.Message = $"DST starts on {DSTTransition.Value.ToString("dd/MM/yyyy")} in {observedTimeZone.DisplayName}.";
            }
            else
            {
                notification.Message = $"DST ends on {DSTTransition.Value.ToString("dd/MM/yyyy")} in {observedTimeZone.DisplayName}.";
            }


        }


        #endregion
    }
}
