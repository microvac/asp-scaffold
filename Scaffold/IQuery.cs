using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scaffold
{
    public interface IQuery<TModel>
    {
        IQueryable<TModel> Sort(IQueryable<TModel> query);

        IQueryable<TModel> Page(IQueryable<TModel> query);

        IQueryable<TModel> Filter(IQueryable<TModel> query);
    }
}
