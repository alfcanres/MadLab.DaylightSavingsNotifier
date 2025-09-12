namespace DSTN.Domain.Entities
{
    public class Notification
    {
        public int Id { get; set; }
        public int TimeZoneId { get; set; }
        public ObservedTimeZone TimeZone { get; set; }
        public DateTime DSTTransition { get; set; }
        public DateTime NotifyDate { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool WasRead { get; set; }
        public DateTime? ReadAt { get; set; }
    }
}
