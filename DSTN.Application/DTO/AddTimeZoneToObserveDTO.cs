using DSTN.Domain.Entities;

namespace DSTN.Application.DTO
{
    public class AddTimeZoneToObserveDTO
    {
        public int CountryId { get; set; }
        public string Color { get; set; }
        public string DisplayName { get; set; }
        public string Comments { get; set; }
        public string TimeZoneId { get; set; }
        public bool IsActive { get; set; }
        public int NotifyDaysBefore { get; set; }
        public DateTime CreatedAt { get; set; }

        public static ObservedTimeZone ToEntity(AddTimeZoneToObserveDTO dto)
        {
            return new ObservedTimeZone
            {
                Color = dto.Color,
                DisplayName = dto.DisplayName,
                Comments = dto.Comments,
                TimeZoneId = dto.TimeZoneId,
                IsActive = dto.IsActive,
                NotifyDaysBefore = dto.NotifyDaysBefore
            };

        }
    }
}
