namespace DSTN.Application.DTO;

public class EmailTimeZoneNotificationDTO
{
    public string Email { set; get; }
    public IEnumerable<int> ObservedTimeZoneIds { get; set; }
}
