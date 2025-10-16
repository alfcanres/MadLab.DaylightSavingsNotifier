using DSTN.Domain.Entities;

namespace DSTN.Application.DTO
{
    public class EditTimeZoneToObserveDTO
    {
        public int Id { get; set; }
        public string Color { get; set; }
        public string DisplayName { get; set; }
        public string Comments { get; set; }
        public string TimeZoneId { get; set; }
        public bool IsActive { get; set; }
        public int NotifyDaysBefore { get; set; }
        public DateTime LastChanged { get; set; }

        public static void ToEntity(EditTimeZoneToObserveDTO dto, ObservedTimeZone entity)
        {
            entity.Color = dto.Color;
            entity.DisplayName = dto.DisplayName;
            entity.Comments = dto.Comments;
            entity.TimeZoneId = dto.TimeZoneId;
            entity.IsActive = dto.IsActive;
            entity.NotifyDaysBefore = dto.NotifyDaysBefore;
        }

        public static EditTimeZoneToObserveDTO FromEntity(ObservedTimeZone entity)
        {
            return new EditTimeZoneToObserveDTO
            {
                Id = entity.Id,
                Color = entity.Color,
                DisplayName = entity.DisplayName,
                Comments = entity.Comments,
                TimeZoneId = entity.TimeZoneId,
                IsActive = entity.IsActive,
                NotifyDaysBefore = entity.NotifyDaysBefore,
                LastChanged = DateTime.UtcNow
            };
        }
    }
}
