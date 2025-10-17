using DSTN.AdminApp.WinForms.Interfaces;
using DSTN.AdminApp.WinForms.ViewModels.TimeZones;



namespace DSTN.AdminApp.WinForms.Forms.TimeZones
{
    public interface IListTimeZones : IPagedListForm
    {
        IEnumerable<ObservedTimeZoneForList> TimeZones { get; set; }
        IEditTimeZone EditorForm { get; set; }
        IEnumerable<string> Filters { get; set; }
        string SelectedFilter { get; set; }

    }
}
