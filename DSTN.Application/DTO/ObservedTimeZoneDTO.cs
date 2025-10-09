using DSTN.Domain.Entities;

namespace DSTN.Application.DTO
{
    public class ObservedTimeZoneDTO
    {
        public int Id { get; set; }
        public string Color { get; set; }
        public string DisplayName { get; set; }
        public string Comments { get; set; }
        public string TimeZoneId { get; set; }
        public DateTime? DSTStarts { get; set; }
        public DateTime? DSTEnds { get; set; }
        public DateTime? LastChanged { get; set; }
        public bool TimeZoneObservesDST { get; set; }
        public DateTime? NextTransitionDate { get; set; }
        public bool IsActive { get; set; }
        public int NotifyDaysBefore { get; set; }

        public static ObservedTimeZoneDTO FromEntity(ObservedTimeZone entity)
        {
            return new ObservedTimeZoneDTO
            {
                Id = entity.Id,
                Color = entity.Color,
                DisplayName = entity.DisplayName,
                Comments = entity.Comments,
                TimeZoneId = entity.TimeZoneId,
                DSTStarts = entity.DSTStarts,
                DSTEnds = entity.DSTEnds,
                LastChanged = entity.LastChanged,
                TimeZoneObservesDST = entity.TimeZoneObservesDST,
                NextTransitionDate = entity.NextTransitionDate,
                IsActive = entity.IsActive,
                NotifyDaysBefore = entity.NotifyDaysBefore
            };
        }

        public static ObservedTimeZone ToEntity(ObservedTimeZoneDTO dto)
        {
            return new ObservedTimeZone
            {
                Id = dto.Id,
                Color = dto.Color,
                DisplayName = dto.DisplayName,
                Comments = dto.Comments,
                TimeZoneId = dto.TimeZoneId,
                DSTStarts = dto.DSTStarts,
                DSTEnds = dto.DSTEnds,
                LastChanged = dto.LastChanged,
                TimeZoneObservesDST = dto.TimeZoneObservesDST,
                NextTransitionDate = dto.NextTransitionDate,
                IsActive = dto.IsActive,
                NotifyDaysBefore = dto.NotifyDaysBefore
            };
        }
    }
}
