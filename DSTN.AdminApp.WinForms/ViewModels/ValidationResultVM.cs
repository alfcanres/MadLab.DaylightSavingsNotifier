using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSTN.AdminApp.WinForms.ViewModels
{
    public record ValidationResultVM(bool IsValid = true, List<string> MessageList = null);
}
