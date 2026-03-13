using DSTN.Application.Helpers;

namespace DSTN.Application.DTO
{
    public class EmailConfigurationListParamsDTO : IPagerParams
    {
        public string? Name { get; set; }
        public bool? IsActive { get; set; }
        public int RecordsPerPage { get; set; }
        public int CurrentPage { get; set; }
    }
}
