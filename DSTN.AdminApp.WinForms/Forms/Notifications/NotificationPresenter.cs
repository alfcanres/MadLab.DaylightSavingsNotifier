using DSTN.AdminApp.WinForms.Interfaces;
using DSTN.Application.DTO;
using DSTN.Application.Services.TimeZoneConfigurator;
using DSTN.Application.Services.TimeZoneNotifier;

namespace DSTN.AdminApp.WinForms.TimeZones
{
    public class NotificationPresenter : IGenericPresenter
    {
        private IViewNotification _editView;
        private readonly IListNotifications _listView;
        private readonly ITimeZoneNotifierService _timeZoneNotifierService;
        private readonly NotificationListParamsDTO FilterParameters;

        public NotificationPresenter(
            IListNotifications listView,
            ITimeZoneNotifierService timeZoneNotifierService)
        {

            _listView = listView;
            _timeZoneNotifierService = timeZoneNotifierService;
            FilterParameters = new NotificationListParamsDTO();
        }

        public void SetEditor(IViewNotification editView)
        {
            _editView = editView;
        }

        internal async Task IntializeListForm()
        {
            _listView.ShowLoading("Loading time zones...");



            List<string> filters = new List<string>
            {
                "All",
                "DisplayName",
                "IsActive",
                "TimeZoneId"
            };

            _listView.Filters = filters;
            _listView.SelectedFilter = "All";
            _listView.Title = "Observed Time Zones";

            FilterParameters.DisplayName = null;
            FilterParameters.IsActive = null;
            FilterParameters.TimeZoneId = null;
            FilterParameters.CurrentPage = 1;
            FilterParameters.RecordsPerPage = 10;

            _listView.FilterParams = FilterParameters;

            await LoadListAsync();

            _listView.HideLoading();
        }
        public async Task CreateNewAsync()
        {
            _listView.Title = "Add New Time Zone";
            _listView.HideLoading();
            _editView.ShowDeleteButton = false;
            _editView.ShowSaveButtom = true;

            _editView.Id = 0;
            _editView.Color = string.Empty;
            _editView.DisplayName = string.Empty;
            _editView.Comments = string.Empty;
            _editView.SelectedTimeZoneId = string.Empty;
            _editView.DSTStarts = null;
            _editView.DSTEnds = null;
            _editView.LastChanged = null;
            _editView.TimeZoneObservesDST = "";
            _editView.NextNotifyDate = null;
            _editView.IsActive = true;
            _editView.NotifyDaysBefore = 0;

            var response = await _timeZoneConfiguratorService.GetSystemTimeZones();

            if (response.ValidatorResponse.IsValid)
            {
                _editView.SystemTimeZones = response.Result.ToList();
                _editView.Show();
            }
            else
            {
                _editView.CloseForm();
                _listView.ShowAlert("Failed to load system time zones.");
            }


                
            
        }

        public async Task EditSelectedAsync()
        {
            int timeZoneId = _listView.SelectedId;
            if (timeZoneId <= 0)
            {

                _listView.ShowAlert("No time zone was selected.");
                return;
            }
            else
            {



                _editView.ShowLoading("Loading time zone details...");
                
                var response = await _timeZoneConfiguratorService.GetForEdit(timeZoneId);

                if (response.ValidatorResponse.IsValid)
                {
                    
                    _editView.ShowDeleteButton = true;
                    _editView.ShowSaveButtom = true;
                    var timeZone = response.Result;
                    _listView.Title = timeZone.DisplayName;
                    _editView.Id = timeZone.Id;
                    _editView.Color = timeZone.Color;
                    _editView.DisplayName = timeZone.DisplayName;
                    _editView.Comments = timeZone.Comments;
                    _editView.SystemTimeZones = timeZone.SystemTimeZones;
                    _editView.SelectedTimeZoneId = timeZone.TimeZoneId;

                    string dstStarts = timeZone.DSTStarts.HasValue ? timeZone.DSTStarts.Value.ToString("yyyy-MM-dd HH:mm:ss") : "N/A";
                    string dstEnds = timeZone.DSTEnds.HasValue ? timeZone.DSTEnds.Value.ToString("yyyy-MM-dd HH:mm:ss") : "N/A";
                    string lastChanged = timeZone.LastChanged.HasValue ? timeZone.LastChanged.Value.ToString("yyyy-MM-dd HH:mm:ss") : "N/A";
                    string nextNotifyDate = timeZone.NextNotifyDate.HasValue ? timeZone.NextNotifyDate.Value.ToString("yyyy-MM-dd HH:mm:ss") : "N/A";


                    _editView.DSTStarts = dstStarts;
                    _editView.DSTEnds = dstEnds;
                    _editView.LastChanged = lastChanged;
                    _editView.TimeZoneObservesDST = timeZone.TimeZoneObservesDST ? "Yes" : "No";
                    _editView.NextNotifyDate = nextNotifyDate;


                    _editView.IsActive = timeZone.IsActive;
                    _editView.NotifyDaysBefore = timeZone.NotifyDaysBefore;
                    _editView.HideLoading();
                    _editView.Show();
                }
                else
                {
                    _editView.HideLoading();
                    _editView.ValidationErrors = response.ValidatorResponse.MessageList;
                    _editView.ShowErrors();
                }


            }

        }

        public async Task SaveAsync()
        {
            _editView.ShowLoading("Saving time zone...");
            if (_editView.Id == 0)
            {
                var addModel = new AddTimeZoneToObserveDTO
                {
                    Color = _editView.Color,
                    DisplayName = _editView.DisplayName,
                    Comments = _editView.Comments,
                    TimeZoneId = _editView.SelectedTimeZoneId,
                    IsActive = _editView.IsActive,
                    NotifyDaysBefore = _editView.NotifyDaysBefore
                };
                var response = await _timeZoneConfiguratorService.AddTimeZoneToObserveAsync(addModel);
                if (response.ValidatorResponse.IsValid)
                {
                    _editView.HideLoading();
                    _listView.ShowAlert("Time zone added successfully.");
                }
                else
                {
                    _editView.HideLoading();
                    _editView.ValidationErrors = response.ValidatorResponse.MessageList;
                    _editView.ShowErrors();
                }
            }
            else
            {
                var editModel = new EditTimeZoneToObserveDTO
                {
                    Id = _editView.Id,
                    Color = _editView.Color,
                    DisplayName = _editView.DisplayName,
                    Comments = _editView.Comments,
                    TimeZoneId = _editView.SelectedTimeZoneId,
                    IsActive = _editView.IsActive,
                    NotifyDaysBefore = _editView.NotifyDaysBefore
                };
                var response = await _timeZoneConfiguratorService.EditTimeZoneToObserveAsync(editModel);
                if (response.ValidatorResponse.IsValid)
                {
                    _editView.HideLoading();
                    _listView.ShowAlert("Time zone updated successfully.");
                }
                else
                {
                    _editView.HideLoading();
                    _editView.ValidationErrors = response.ValidatorResponse.MessageList;
                    _editView.ShowErrors();
                }
            }

        }

        public async Task DeleteFromListAsync()
        {
            _listView.HideLoading();
            if (_editView.ConfirmDelete("Are you sure you want to delete this time zone?"))
            {
                var response = await _timeZoneConfiguratorService.DeleteZoneToObserveAsync(_listView.SelectedId);
                _editView.ShowLoading();
                if (response.ValidatorResponse.IsValid)
                {
                    _listView.ShowAlert("Time zone deleted successfully.");
                    await LoadListAsync();

                }
                else
                {
                    _listView.ValidationErrors = response.ValidatorResponse.MessageList;
                    _listView.ShowError();
                }
                _listView.HideLoading();
            }

        }

        public async Task DeleteFromEditorAsync()
        {
            _editView.HideLoading();
            //if (_editView.ConfirmDelete("Are you sure you want to delete this time zone?"))
            //{
            //    var response = await _timeZoneConfiguratorService.DeleteZoneToObserveAsync(_editView.Id);
            //    _editView.ShowLoading();
            //    if (response.ValidatorResponse.IsValid)
            //    {
            //        _editView.ShowAlert("Time zone deleted successfully.");
            //        _editView.CloseForm();
            //        await LoadListAsync();
            //    }
            //    else
            //    {
            //        _editView.ValidationErrors = response.ValidatorResponse.MessageList;
            //        _editView.ShowErrors();
            //    }
            //    _editView.HideLoading();
            //}

        }

        public async Task LoadListAsync()
        {
            //_listView.ShowLoading("Loading time zones...");
            //_listView.HidePager();
            //var response = await _timeZoneConfiguratorService.ListObservedTimeZones(_listView.FilterParams);
            //if (response.ValidatorResponse.IsValid == false)
            //{
            //    _listView.ValidationErrors = response.ValidatorResponse.MessageList;
            //    _listView.ShowError();
            //    _listView.HideLoading();

            //    return;
            //}
            //_listView.TimeZones = response?.Result?.List;

            //if (response?.Result?.PageCount > 1)
            //{
            //    _listView.ShowPager();
            //    _listView.PageCount = $"Page {response?.Result?.CurrentPage} of {response?.Result?.PageCount}";
            //}
            //else
            //{
            //    _listView.HidePager();
            //}


            //_listView.HideLoading();


        }

        public void ConfigureFilterParameters()
        {
            if (_listView.SelectedFilter == "All")
            {
                FilterParameters.DisplayName = null;
                FilterParameters.IsActive = null;
                FilterParameters.TimeZoneId = null;
            }
            else if (_listView.SelectedFilter == "DisplayName")
            {
                FilterParameters.DisplayName = _listView.SearchKeyWord;
                FilterParameters.IsActive = null;
                FilterParameters.TimeZoneId = null;
            }
            else if (_listView.SelectedFilter == "IsActive")
            {
                if (bool.TryParse(_listView.SearchKeyWord, out bool isActive))
                {
                    FilterParameters.IsActive = isActive;
                }
                else
                {
                    FilterParameters.IsActive = null;
                }
                FilterParameters.DisplayName = null;
                FilterParameters.TimeZoneId = null;
            }
            else if (_listView.SelectedFilter == "TimeZoneId")
            {
                FilterParameters.TimeZoneId = _listView.SearchKeyWord;
                FilterParameters.DisplayName = null;
                FilterParameters.IsActive = null;
            }
            FilterParameters.CurrentPage = _listView.CurrentPage;
            FilterParameters.RecordsPerPage = _listView.RecordsPerPage;
        }


    }
}
