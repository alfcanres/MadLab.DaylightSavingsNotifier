namespace DSTN.Application.DTO;

/// <summary>
/// Represents an email notification request for one or more observed time zones.
/// </summary>
/// <example>
/// <code>
/// var dto = new EmailTimeZoneNotificationDTO
/// {
///     Email = "user@example.com",
///     ObservedTimeZoneIds = new[] { 1, 2, 3 }
/// };
/// </code>
/// </example>
public class EmailTimeZoneNotificationDTO
{
    /// <summary>
    /// Gets the recipient email address for the notification.
    /// </summary>
    public required string Email { get; init; }

    /// <summary>
    /// Gets the observed time zone identifiers associated with the notification.
    /// Defaults to an empty collection to avoid null handling by consumers.
    /// </summary>
    public IEnumerable<int> ObservedTimeZoneIds { get; init; } = Array.Empty<int>();
}
