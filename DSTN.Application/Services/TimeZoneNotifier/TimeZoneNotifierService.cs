using DSTN.Application.DTO;
using DSTN.Application.Helpers;
using DSTN.Application.Services.TimeZoneNotifier.Filters;
using DSTN.Domain.Entities;
using DSTN.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System.Text;

namespace DSTN.Application.Services.TimeZoneNotifier
{
    public class TimeZoneNotifierService :
        BaseService,
        ITimeZoneNotifierService
    {
        private readonly IQueryBuilder<Notification> _queryBuilder;
        private readonly ISystemTimeZoneProvider _systemTimeZoneProvider;
        private readonly IEmailService _emailService;

        public TimeZoneNotifierService(
            IUnitOfWork unitOfWork,
            ILogger<TimeZoneNotifierService> logger,
            IQueryBuilder<Notification> queryBuilder,
            ISystemTimeZoneProvider systemTimeZoneProvider,
            IEmailService emailService)
            : base(unitOfWork, logger)
        {
            _queryBuilder = queryBuilder;

            _queryBuilder.Include("TimeZone");

            _systemTimeZoneProvider = systemTimeZoneProvider;

            _emailService = emailService;

        }

        public async Task<OperationResult<NotificationReadDTO>> GetNotificationByIdAsync(int id)
        {
            Validator.Clear();
            try
            {
                var query = UnitOfWork.Notifications
                    .QueryInclude("TimeZone")
                    .Where(t => t.Id == id);

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
                var query = UnitOfWork.Notifications
                    .QueryInclude("TimeZone")
                    .Where(t => t.Id == id);

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
                        tz.DSTStarts = _systemTimeZoneProvider.GetDSTStartDate(today.Year, tz.TimeZoneId);
                        tz.DSTEnds = _systemTimeZoneProvider.GetDSTEndDate(today.Year, tz.TimeZoneId);
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
                            n.NotifyDate.Day == nextNotificationDate.Day
                            &&
                            n.NotifyDate.Year == nextNotificationDate.Year
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
                    NotifyDate = observedTimeZone.NextNotificationDate!.Value,
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

        public async Task<OperationResult<Dictionary<string, bool>>> SendEmailNotificationAsync(NotificationReadDTO readDTO)
        {
            Dictionary<string, bool> emailResults = new Dictionary<string, bool>();
            Validator.Clear();

            try
            {
                var emailConfQuery = UnitOfWork.EmailConfigurations
                    .Query()
                    .Where(t => t.IsDefault && t.IsActive);

                var emailConf = await UnitOfWork.EmailConfigurations.FirstOrDefaultAsync(emailConfQuery);

                if (emailConf == null)
                {
                    Validator.AddError("No default email configuration found.");
                    return new OperationResult<Dictionary<string, bool>>
                    {
                        Data = emailResults,
                        ValidatorResponse = Validator.CrateNewCopy(),
                    };
                }

                var observedTimeZoneQuery = UnitOfWork.ObservedTimeZones
                    .Query()
                    .Where(tz => tz.Id == readDTO.TimeZoneId);

                var observedTimeZone = await UnitOfWork.ObservedTimeZones.FirstOrDefaultAsync(observedTimeZoneQuery);

                if (String.IsNullOrEmpty(observedTimeZone!.ForwardEmailList))
                {
                    Validator.AddError($"No email list found for timezone {observedTimeZone.DisplayName}");
                    return new OperationResult<Dictionary<string, bool>>
                    {
                        Data = emailResults,
                        ValidatorResponse = Validator.CrateNewCopy(),
                    };
                }

                string emailList = observedTimeZone.ForwardEmailList;

                _emailService.ConfigureCredentials(emailConf.SmtpHost, emailConf.SmtpPort, emailConf.Username, emailConf.Password, emailConf.UseSsl);

                string[] arrEmails = emailList.Split(";");

                foreach (string toEmail in arrEmails)
                {

                    (bool success, string message) = await _emailService.SendEmailAsync(
                        emailConf.SenderEmail,
                        toEmail,
                        $"DST {observedTimeZone.DisplayName} about to change",
                        readDTO.Message,
                        false);

                    emailResults.Add(toEmail, success);

                    if (!success)
                    {
                        Validator.AddError($"Failed to send email to {toEmail}: {message}  for timezone {observedTimeZone.DisplayName} - {observedTimeZone.TimeZoneId}");
                    }
                }

            }
            catch (Exception ex)
            {
                Validator.AddError($"Unexpected error: {ex.Message}");
                _logger.LogError(ex, $"Unable to send email notification for TimeZone with ID {readDTO.TimeZoneId}");
                Validator.IsValid = false;
            }

            return new OperationResult<Dictionary<string, bool>>
            {
                Data = emailResults,
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

        public async Task<OperationResult<IEnumerable<EmailTimeZoneNotificationDTO>>> GetEmailsToNotifyAsync()
        {
            Validator.Clear();
            try
            {
                var emails = new List<EmailTimeZoneNotificationDTO>();

                var observedTimeZones = UnitOfWork.ObservedTimeZones
                    .Query()
                    .Where(t => t.IsActive && !String.IsNullOrEmpty(t.ForwardEmailList.Trim()))
                    .AsEnumerable();

                HashSet<string> emailsHash = new HashSet<string>();

                foreach (var timeZone in observedTimeZones)
                {
                    string[] arrEmails = timeZone.ForwardEmailList.Split(";");
                    foreach (var email in arrEmails)
                    {
                        emailsHash.Add(email);
                    }
                }

                foreach (var email in emailsHash)
                {
                    var emailToNotify = new EmailTimeZoneNotificationDTO();
                    emailToNotify.Email = email;
                    List<int> timeZonesIds = new List<int>();

                    foreach (var timeZone in observedTimeZones)
                    {
                        var emailFound = timeZone.ForwardEmailList
                            .Split(";")
                            .Where(t => t == email).Any();

                        if (emailFound)
                            timeZonesIds.Add(timeZone.Id);
                    }

                    emailToNotify.ObservedTimeZoneIds = timeZonesIds;

                    emails.Add(emailToNotify);
                }


                return new OperationResult<IEnumerable<EmailTimeZoneNotificationDTO>>
                {
                    Data = emails,
                    ValidatorResponse = Validator.CrateNewCopy()
                };
            }
            catch (Exception ex)
            {
                Validator.AddError($"Unexpected error: {ex.Message}");

                _logger.LogError(ex, "Error in GetEmailsToNotifyAsync");

                Validator.IsValid = false;

                return new OperationResult<IEnumerable<EmailTimeZoneNotificationDTO>>
                {
                    Data = null,
                    ValidatorResponse = Validator.CrateNewCopy()
                };
            }
        }

        public async Task<OperationResult<Dictionary<string, bool>>> SendEmailForTimezoneSummary(EmailTimeZoneNotificationDTO emailToNotify)
        {
            Dictionary<string, bool> emailResults = new Dictionary<string, bool>();
            Validator.Clear();

            try
            {
                var emailConfQuery = UnitOfWork.EmailConfigurations
                    .Query()
                    .Where(t => t.IsDefault && t.IsActive);

                var emailConf = await UnitOfWork.EmailConfigurations.FirstOrDefaultAsync(emailConfQuery);

                if (emailConf == null)
                {
                    Validator.AddError("No default email configuration found.");
                    return new OperationResult<Dictionary<string, bool>>
                    {
                        Data = emailResults,
                        ValidatorResponse = Validator.CrateNewCopy(),
                    };
                }


                _emailService.ConfigureCredentials(emailConf.SmtpHost, emailConf.SmtpPort, emailConf.Username, emailConf.Password, emailConf.UseSsl);

                var observedTimeZones = UnitOfWork.ObservedTimeZones
                    .Query()
                    .Where(t => emailToNotify.ObservedTimeZoneIds.Contains(t.Id))
                    .AsEnumerable();


                (bool success, string message) = await _emailService.SendEmailAsync(
                     emailConf.SenderEmail,
                     emailToNotify.Email,
                     $"DST Summary for {DateTime.Now.Year}",
                     GetTimeZoneSummary(observedTimeZones),
                     false);

                emailResults.Add(emailToNotify.Email, success);

                if (!success)
                {
                    Validator.AddError($"Failed to send time zone summary for {emailToNotify.Email}: {message}");
                }

            }
            catch (Exception ex)
            {
                Validator.AddError($"Unexpected error: {ex.Message}");
                _logger.LogError(ex, $"Unable to send email notification summary to {emailToNotify.Email}");
                Validator.IsValid = false;
            }

            return new OperationResult<Dictionary<string, bool>>
            {
                Data = emailResults,
                ValidatorResponse = Validator.CrateNewCopy(),
            };
        }

        private string GetTimeZoneSummary(IEnumerable<ObservedTimeZone> observedTimeZones)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("=".PadRight(80, '='));
            stringBuilder.AppendLine("OBSERVED TIME ZONES SUMMARY");
            stringBuilder.AppendLine("=".PadRight(80, '='));
            stringBuilder.AppendLine();

            int count = 0;
            foreach (var tz in observedTimeZones)
            {
                count++;
                stringBuilder.AppendLine($"Time Zone #{count}");
                stringBuilder.AppendLine("-".PadRight(80, '-'));

                stringBuilder.AppendLine($"Display Name:           {tz.DisplayName}");
                stringBuilder.AppendLine($"Time Zone ID:           {tz.TimeZoneId}");
                stringBuilder.AppendLine($"Observes DST:           {(tz.TimeZoneObservesDST ? "Yes" : "No")}");

                if (tz.TimeZoneObservesDST)
                {
                    stringBuilder.AppendLine($"DST Starts:             {(tz.DSTStarts.HasValue ? tz.DSTStarts.Value.ToString("yyyy-MM-dd HH:mm:ss") : "N/A")}");
                    stringBuilder.AppendLine($"DST Ends:               {(tz.DSTEnds.HasValue ? tz.DSTEnds.Value.ToString("yyyy-MM-dd HH:mm:ss") : "N/A")}");
                    stringBuilder.AppendLine($"Next Transition:        {(tz.NextTransitionDate.HasValue ? tz.NextTransitionDate.Value.ToString("yyyy-MM-dd HH:mm:ss") : "N/A")}");
                    stringBuilder.AppendLine($"Next Notification:      {(tz.NextNotificationDate.HasValue ? tz.NextNotificationDate.Value.ToString("yyyy-MM-dd HH:mm:ss") : "N/A")}");
                    stringBuilder.AppendLine($"Notify Days Before:     {tz.NotifyDaysBefore}");
                }

                stringBuilder.AppendLine($"Comments:               {(String.IsNullOrWhiteSpace(tz.Comments) ? "None" : tz.Comments)}");
                stringBuilder.AppendLine($"Last Changed:           {(tz.LastChanged.HasValue ? tz.LastChanged.Value.ToString("yyyy-MM-dd HH:mm:ss") : "N/A")}");
                stringBuilder.AppendLine($"Created At:             {tz.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss")}");

                stringBuilder.AppendLine();
            }

            if (count == 0)
            {
                stringBuilder.AppendLine("No observed time zones found.");
                stringBuilder.AppendLine();
            }
            else
            {
                stringBuilder.AppendLine("=".PadRight(80, '='));
                stringBuilder.AppendLine($"Total Time Zones: {count}");
                stringBuilder.AppendLine("=".PadRight(80, '='));
            }

            return stringBuilder.ToString();
        }
    }
}
