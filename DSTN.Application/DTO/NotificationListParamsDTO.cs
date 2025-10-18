using DSTN.Application.Helpers;

namespace DSTN.Application.DTO
{
    public class NotificationListParamsDTO : IPagerParams
    {
        public int ObservedTimeZoneId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? WasRead { get; set; }
        public int RecordsPerPage { get; set; }
        public int CurrentPage { get; set; }

        public NotificationListParamsDTO()
        {
            StartDate = DateTime.UtcNow.Date;
            EndDate = DateTime.UtcNow.Date.AddDays(1).AddTicks(-1);
        }
    }
}
