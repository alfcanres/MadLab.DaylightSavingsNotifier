using System.Text;

namespace DSTN.AdminApp.WinForms.ViewModels.EmailConfigurator;

public class EmailConfigurationListParams
{
    public string? Name { get; set; }
    public bool? IsActive { get; set; }
    public int RecordsPerPage { get; set; } = 10;
    public int CurrentPage { get; set; } = 1;

    public string ToQueryString()
    {
        var query = new StringBuilder($"?CurrentPage={CurrentPage}&RecordsPerPage={RecordsPerPage}");

        if (!string.IsNullOrEmpty(Name))
            query.Append($"&Name={Name}");

        if (IsActive.HasValue)
            query.Append($"&IsActive={IsActive.Value}");

        return query.ToString();
    }
}
