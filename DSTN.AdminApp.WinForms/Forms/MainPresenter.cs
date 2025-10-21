using DSTN.AdminApp.WinForms.Properties;
using DSTN.AdminApp.WinForms.Repository.Notifications;


namespace DSTN.AdminApp.WinForms.Forms
{
    public class MainPresenter
    {
        private readonly IMain _mainForm;
        private readonly INotficationsService _notficationsService;
        private readonly System.Windows.Forms.Timer _timer;

        public MainPresenter(IMain mainForm, INotficationsService notficationsService)
        {
            _mainForm = mainForm;
            _notficationsService = notficationsService;
            _timer = new System.Windows.Forms.Timer();
            _timer.Tick += timerNotifications_Tick;
            _mainForm.NotificationsText = "";
            _mainForm.NotificationsTitleMenu = "Notifications";
            _mainForm.PendingNotifications = 0;
        }

        public void InitNotifier()
        {
            _timer.Interval = Settings.Default.ScanForNotificationsIntervalMlsc;
            _timer.Start();
        }

        public void StopNotifier()
        {
            _timer.Interval = Settings.Default.ScanForNotificationsIntervalMlsc;
            _timer.Stop();
        }

        private async void timerNotifications_Tick(object sender, EventArgs e)
        {
            var res = await _notficationsService.CountUnread();
            if (res.Status == ViewModels.ResultStatus.Success)
            {
                if (res.Data == 0)
                {
                    _mainForm.NotificationsText = "";
                    _mainForm.NotificationsTitleMenu = "Notifications";
                    _mainForm.PendingNotifications = 0;
                }
                else
                {

                    if (res.Data != _mainForm.PendingNotifications)
                    {
                        int totalNewNotifications = res.Data - _mainForm.PendingNotifications;
                        _mainForm.PendingNotifications = res.Data;
                        _mainForm.NotificationsText = $"{totalNewNotifications} new notifications pending to read!";
                        _mainForm.NotificationsTitleMenu = $"Notifications {_mainForm.PendingNotifications}";
                    }
                    else
                    {
                        _mainForm.NotificationsText = "";
                        _mainForm.NotificationsTitleMenu = "Notifications";
                    }
                }

            }
        }
    }
}
