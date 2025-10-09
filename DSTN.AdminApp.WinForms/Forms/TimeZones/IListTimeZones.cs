using DSTN.AdminApp.WinForms.Interfaces;
using DSTN.AdminApp.WinForms.ViewModels.TimeZones;



namespace DSTN.AdminApp.WinForms.Forms.TimeZones
{
    public interface IListTimeZones : IListForm
    {
        IEnumerable<ObservedTimeZoneForListVM> TimeZones { get; set; }
        ObservedTimeZoneForListParamsVM FilterParams { set; get; }
        IEditTimeZone EditorForm { get; set; }

        IEnumerable<string> Filters { get; set; }

        string SelectedFilter { get; set; }

    }
}
