using DSTN.AdminApp.WinForms.Interfaces;
using DSTN.Domain.Entities;


namespace DSTN.AdminApp.WinForms.TimeZones
{
    public interface IViewNotification : IEditorForm
    {

        public int Id { get; set; }
        public int TimeZoneId { get; set; }
        public ObservedTimeZone TimeZone { get; set; }
        public DateTime DSTTransition { get; set; }
        public DateTime NotifyDate { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; } 
        public bool WasRead { get; set; }
        public DateTime? ReadAt { get; set; }


        void Show();
    }
}
