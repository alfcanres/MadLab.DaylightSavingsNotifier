using DSTN.AdminApp.WinForms.Interfaces;
using DSTN.Application.DTO;


namespace DSTN.AdminApp.WinForms.Forms.TimeZones
{
    public interface IListTimeZones : IListForm
    {
        IEnumerable<ObservedTimeZoneForListDTO> TimeZones { get; set; }
        ObservedTimeZoneForListParamsDTO FilterParams { set; get; }
        IEditTimeZone EditorForm { get; set; }

        IEnumerable<string> Filters { get; set; }

        string SelectedFilter { get; set; }

    }
}
