using DSTN.AdminApp.WinForms.Forms.Notifications;
using DSTN.AdminApp.WinForms.Interfaces;
using DSTN.AdminApp.WinForms.Repository.Notifications;
using DSTN.AdminApp.WinForms.ViewModels;
using DSTN.AdminApp.WinForms.ViewModels.Notifications;

namespace DSTN.AdminApp.WinForms.TimeZones
{
    public class NotificationPresenter : IGenericPresenter
    {
        private IEditNotification _editView;
        private readonly IListNotifications _listView;
        private readonly INotficationsService _service;

        public NotificationPresenter(
            IListNotifications listView,
            INotficationsService service
            )
        {

            _listView = listView;
            _service = service;
        }

        public void SetEditor(IEditNotification editView)
        {
            _editView = editView;
        }

        internal async Task IntializeListForm()
        {
            _listView.ShowLoading("Loading notifications...");

            List<string> filters = new List<string>
            {
                "[SELECT]",
                "Pending",
                "Date Range",
                "Time Zone",
            };

            _listView.Filters = filters;
            _listView.SelectedFilter = "Pending";
            _listView.Title = "Notifications";



            await LoadListAsync();

            _listView.HideLoading();
        }
        public async Task CreateNewAsync()
        {
            throw new Exception("Cannot create Notifications, only view");
        }

        public async Task EditSelectedAsync()
        {
            int notificationId = _listView.SelectedId;
            if (notificationId <= 0)
            {

                _listView.ShowAlert("No notification was selected.");
                return;
            }
            else
            {
                _editView.ShowLoading("Loading notification details...");

                var response = await _service.GetNotificationByIdAsync(notificationId);


                if (response.Status == ResultStatus.Success)
                {

                    _editView.ShowDeleteButton = true;
                    _editView.ShowSaveButtom = false;
                    var data = response.Data;
                    _editView.Id = data.Id;
                    _editView.TimeZoneId = data.TimeZoneId;
                    _editView.TimeZoneDisplayName = data.TimeZoneDisplayName;
                    _editView.DSTTransition = data.DSTTransition.ToString();
                    _editView.NotifyDate = data.NotifyDate.ToString();
                    _editView.Message = data.Message;
                    _editView.CreatedAt = data.CreatedAt.ToString();
                    _editView.WasRead = data.WasRead;
                    _editView.ReadAt = data.ReadAt.ToString();

                    _editView.HideLoading();
                    _editView.Show();
                }
                else
                {
                    _editView.HideLoading();
                    _editView.ShowErrors(response.Messages);
                }


            }

        }

        public async Task SaveAsync()
        {
            throw new Exception("Cannot modify Notification");

        }

        public async Task DeleteFromListAsync()
        {
            throw new Exception("Cannot delete Notification");
        }

        public async Task DeleteFromEditorAsync()
        {
            throw new Exception("Cannot delete Notification");
        }

        public async Task LoadListAsync()
        {
            _listView.ShowLoading("Loading time zones...");
            _listView.HidePager();

            var filterParams = ConfigureFilterParameters();

            var response = await _service.ListNotificationsAsync(filterParams);
            if (response.Status != ResultStatus.Success)
            {
                _listView.ShowErrors(response.Messages);
                _listView.HideLoading();

                return;
            }

            _listView.Notifications = response.Data.List;
            _listView.CurrentPage = response.Data.CurrentPage;
            _listView.PageCount = response.Data.PageCount;
            _listView.TotalRecords = response.Data.RecordCount;



            if (response?.Data?.PageCount > 1)
            {
                _listView.ShowPager();
            }
            else
            {
                _listView.HidePager();
            }


            _listView.HideLoading();


        }

        public NotificationListParams ConfigureFilterParameters()
        {
            //if (_listView.SelectedFilter == "[SELECT]")
            //{
            //    FilterParameters.DisplayName = null;
            //    FilterParameters.IsActive = null;
            //    FilterParameters.TimeZoneId = null;
            //}
            //else if (_listView.SelectedFilter == "DisplayName")
            //{
            //    FilterParameters.DisplayName = _listView.SearchKeyWord;
            //    FilterParameters.IsActive = null;
            //    FilterParameters.TimeZoneId = null;
            //}
            //else if (_listView.SelectedFilter == "TimeZoneId")
            //{
            //    FilterParameters.TimeZoneId = _listView.SearchKeyWord;
            //    FilterParameters.DisplayName = null;
            //    FilterParameters.IsActive = null;
            //}

            //FilterParameters.CurrentPage = _listView.CurrentPage;
            //FilterParameters.RecordsPerPage = _listView.RecordsPerPage;

            return new NotificationListParams(0, null, _listView.RecordsPerPage, _listView.CurrentPage);
        }


    }
}

