using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSTN.AdminApp.WinForms.Forms.TimeZones
{
    public interface IEmailNotificationSummary
    {

        void ClearMessages();
        void AddMessage(string message);

        string Progress {  get; set; }
        void Show();

        bool IsEnabled { get; set; }

        string SendButtonText { get; set; }
    }
}
