using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSTN.Application.Helpers.QueryFilter
{
    public interface IQueryFilter<T> where T : class
    {
        IQueryable<T> ApplyFilter(IQueryable<T> queryable);

    }
}
