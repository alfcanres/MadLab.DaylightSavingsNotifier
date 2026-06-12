namespace DSTN.AdminApp.WinForms.ViewModels.TimeZones;

internal class EmailNotificationProgress
{
    public string Message { get; init; }
    public int ProgressPercentage { get; init; }
    public int TotalEmails { get; init; }
    public int ProcessedEmails { get; init; }
}
