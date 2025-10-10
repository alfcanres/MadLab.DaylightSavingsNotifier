using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSTN.AdminApp.WinForms.ViewModels.TimeZones
{
    public class ObservedTimeZoneForListParams
    {
        public string? DisplayName { get; set; }
        public bool? IsActive { get; set; }
        public string? TimeZoneId { get; set; }
        public int RecordsPerPage { get; set; } = 10;
        public int CurrentPage { get; set; } = 1;

        public string ToQueryString()
        {
            var query = new StringBuilder($"?CurrentPage={CurrentPage}&RecordsPerPage={RecordsPerPage}");

            if (!string.IsNullOrEmpty(DisplayName))
                query.Append($"&DisplayName={DisplayName}");

            if (IsActive.HasValue)
                query.Append($"&IsActive={IsActive.Value}");

            if (!string.IsNullOrEmpty(TimeZoneId))
                query.Append($"&TimeZoneId={TimeZoneId}");

            return query.ToString().ToLower();
        }
    }
}
