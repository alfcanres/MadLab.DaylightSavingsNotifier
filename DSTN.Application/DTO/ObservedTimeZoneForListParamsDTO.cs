using DSTN.Application.Helpers;

namespace DSTN.Application.DTO
{
    public class ObservedTimeZoneForListParamsDTO : IPagerParams
    {
        public string? DisplayName { get; set; }
        public bool? IsActive { get; set; }
        public string? TimeZoneId { get; set; }
        public int RecordsPerPage { get; set; }
        public int CurrentPage { get; set; }
    }
}
