using DSTN.AdminApp.WinForms.Interfaces;


namespace DSTN.AdminApp.WinForms.Forms.TimeZones
{
    public interface IEditTimeZone : IEditorForm
    {

        int Id { get; set; }
        string Color { get; set; }
        string DisplayName { get; set; }
        string Comments { get; set; }
        string DSTStarts { get; set; }
        string DSTEnds { get; set; }
        string LastChanged { get; set; }
        string TimeZoneObservesDST { get; set; }
        bool IsActive { get; set; }
        int NotifyDaysBefore { get; set; }
        string SelectedTimeZoneId { get; set; }
        List<string> SystemTimeZones { get; set; }
        List<string> EmailList { get; set; }
        string EmailToAdd {  get; set; }
        void Show();
    }
}
