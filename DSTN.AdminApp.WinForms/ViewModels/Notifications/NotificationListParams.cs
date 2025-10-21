using System.Text;

namespace DSTN.AdminApp.WinForms.ViewModels.Notifications;


public class NotificationListParams
{

  public int? ObservedTimeZoneId { get; set; }


  public bool? WasRead { get; set; }


  public int RecordsPerPage { get; set; }


  public int CurrentPage { get; set; }


  public string ToQueryString()
  {
      var query = new StringBuilder("?");

      if (ObservedTimeZoneId.HasValue)
      {
          query.Append($"ObservedTimeZoneId={ObservedTimeZoneId.Value}&");
      }

      if (WasRead.HasValue)
      {
          query.Append($"WasRead={WasRead.Value}&");
      }

      query.Append($"RecordsPerPage={RecordsPerPage}&");
      query.Append($"CurrentPage={CurrentPage}");

      return query.ToString().TrimEnd('&');
  }
}
