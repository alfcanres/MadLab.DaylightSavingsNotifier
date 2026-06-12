namespace DSTN.AdminApp.WinForms.ViewModels.TimeZones;

public record EmailTimeZoneNotification(
    string Email, 
    IEnumerable<int> ObservedTimeZoneIds);
