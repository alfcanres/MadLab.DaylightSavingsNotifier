using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSTN.AdminApp.WinForms.ViewModels
{
    public record OperationResultVM<T>(T Result, ValidationResultVM ValidatorResponse);
}
