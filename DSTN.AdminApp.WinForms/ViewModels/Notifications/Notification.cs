

namespace DSTN.AdminApp.WinForms.ViewModels.Notifications
{
    public record Notification(
        int Id,
        int TimeZoneId,
        string TimeZoneColor,
        string TimeZoneDisplayName,
        DateTime DSTTransition,
        DateTime NotifyDate,
        string Message,
        DateTime CreatedAt,
        bool WasRead,
        DateTime? ReadAt
    );
}
