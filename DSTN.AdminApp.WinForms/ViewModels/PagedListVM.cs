using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSTN.AdminApp.WinForms.ViewModels
{
    public record PagedListVM<T>(
        IEnumerable<T> List, 
        int RecordCount, 
        int CurrentPage, 
        int PageCount, 
        int RecordsPerPage) where T : class;
}
