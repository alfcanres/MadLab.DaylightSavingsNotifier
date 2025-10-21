using DSTN.AdminApp.WinForms.Forms.Notifications;
using DSTN.AdminApp.WinForms.Interfaces;
using DSTN.AdminApp.WinForms.Repository.Notifications;
using DSTN.AdminApp.WinForms.Repository.TimeZoneConfigurator;
using DSTN.AdminApp.WinForms.ViewModels;
using DSTN.AdminApp.WinForms.ViewModels.Notifications;
using DSTN.AdminApp.WinForms.ViewModels.TimeZones;

namespace DSTN.AdminApp.WinForms.TimeZones
{
    public class NotificationPresenter : IGenericPresenter
    {
        private IEditNotification _editView;
        private readonly IListNotifications _listView;
        private readonly INotficationsService _notficationsService;
        private readonly ITimeZoneConfiguratorService _timeZoneConfiguratorService;


        public NotificationPresenter(
            IListNotifications listView,
            INotficationsService notficationsService,
            ITimeZoneConfiguratorService timeZoneConfiguratorService
            )
        {

            _listView = listView;
            _notficationsService = notficationsService;
            _timeZoneConfiguratorService = timeZoneConfiguratorService;
        }

        public void SetEditor(IEditNotification editView)
        {
            _editView = editView;
        }

        internal async Task IntializeListForm()
        {
            _listView.ShowLoading("Loading notifications...");


            await LoadTimeZones();

            _listView.ShowNotSeen = true;


            await LoadListAsync();

            _listView.HideLoading();
        }

        private async Task LoadTimeZones()
        {
            var response = await _timeZoneConfiguratorService.GetAllForCombo();

            if (response.Status == ResultStatus.Success)
            {

                List<ItemForCombo> itemForCombos = new List<ItemForCombo>();
                itemForCombos.Add(new ItemForCombo(0, "[SELECT]"));
                itemForCombos.AddRange(response.Data);
                _listView.ObservedTimeZones = itemForCombos;
            }
            else
            {
                _listView.ShowErrors(response.Messages);
            }
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

                var response = await _notficationsService.GetNotificationByIdAsync(notificationId);


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
            _listView.ShowLoading("Loading notifications...");
            _listView.HidePager();

            var filterParams = ConfigureFilterParameters();

            var response = await _notficationsService.ListNotificationsAsync(filterParams);

            await LoadNotSeenNotificationsCount();

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

        private async Task LoadNotSeenNotificationsCount()
        {
            var response = await _notficationsService.CountUnread();
            if (response.Status == ResultStatus.Success)
            {
                _listView.NotSeenNotificationsCount = response.Data;
            }
            else
            {
                _listView.NotSeenNotificationsCount = 0;
            }
        }

        public NotificationListParams ConfigureFilterParameters()
        {

            var filterParams = new NotificationListParams();

            if (_listView.ShowAll)
            {
                filterParams.WasRead = null;
            }
            else if (_listView.ShowNotSeen)
            {
                filterParams.WasRead = false;
            }
            else if (_listView.ShowSeen)
            {
                filterParams.WasRead = true;
            }

            if (_listView.SelectedTimeZoneId != 0)
            {
                filterParams.ObservedTimeZoneId = _listView.SelectedTimeZoneId;
            }
            else
            {
                filterParams.ObservedTimeZoneId = null;
            }


            filterParams.CurrentPage = _listView.CurrentPage;
            filterParams.RecordsPerPage = _listView.RecordsPerPage;

            return filterParams;
        }


    }
}

