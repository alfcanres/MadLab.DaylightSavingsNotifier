using DSTN.Domain.Entities;

namespace DSTN.Application.DTO
{
    public class ObservedTimeZoneForListDTO
    {
        public int Id { get; set; }
        public string Color { get; set; }
        public string DisplayName { get; set; }
        public string Comments { get; set; }
        public DateTime? DSTStarts { get; set; }
        public DateTime? DSTEnds { get; set; }
        public DateTime? LastChanged { get; set; }
        public bool TimeZoneObservesDST { get; set; }
        public DateTime? NextTransitionDate { get; set; }
        public bool IsActive { get; set; }
        public string NotificationSchedule { get; set; }
        public int NotificationsCount { get; set; }

        public static ObservedTimeZoneForListDTO FromEntity(ObservedTimeZone entity)
        {
            return new ObservedTimeZoneForListDTO
            {
                Id = entity.Id,
                Color = entity.Color,
                DisplayName = entity.DisplayName,
                Comments = entity.Comments,
                DSTStarts = entity.DSTStarts,
                DSTEnds = entity.DSTEnds,
                LastChanged = entity.LastChanged,
                TimeZoneObservesDST = entity.TimeZoneObservesDST,
                NextTransitionDate = entity.NextTransitionDate,
                IsActive = entity.IsActive,
                NotificationSchedule = $"{entity.NotifyDaysBefore} days before",
                NotificationsCount = entity.Notifications?.Count ?? 0
            };
        }
    }
}
