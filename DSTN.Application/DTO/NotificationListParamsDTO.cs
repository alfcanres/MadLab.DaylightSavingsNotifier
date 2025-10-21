using DSTN.Application.Helpers;

namespace DSTN.Application.DTO
{
    public class NotificationListParamsDTO : IPagerParams
    {
        public int? ObservedTimeZoneId { get; set; }
        public bool? WasRead { get; set; }
        public int RecordsPerPage { get; set; }
        public int CurrentPage { get; set; }

    }
}
