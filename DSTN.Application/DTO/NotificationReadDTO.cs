using DSTN.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSTN.Application.DTO
{
    public class NotificationReadDTO
    {
        public int Id { get; set; }
        public int TimeZoneId { get; set; }
        public string TimeZoneDisplayName { get; set; }
        public DateTime DSTTransition { get; set; }
        public DateTime NotifyDate { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool WasRead { get; set; }
        public DateTime? ReadAt { get; set; }

        public static NotificationReadDTO FromEntity(Notification entity)
        {
            return new NotificationReadDTO
            {
                Id = entity.Id,
                TimeZoneId = entity.TimeZoneId,
                TimeZoneDisplayName = entity.TimeZone.DisplayName,
                DSTTransition = entity.DSTTransition,
                NotifyDate = entity.NotifyDate,
                Message = entity.Message,
                CreatedAt = entity.CreatedAt,
                WasRead = entity.WasRead,
                ReadAt = entity.ReadAt
            };
        }
    }
}
