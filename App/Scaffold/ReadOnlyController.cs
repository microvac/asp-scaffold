using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scaffold
{
    public class ReadOnlyController<TModel>: ModelController<TModel>
        where TModel: Model, new()
    {
        public ReadOnlyController(DbContext dbContext): base(dbContext) { }

        public IEnumerable<TModel> GetAll()
        {
            return dbSet;
        }

        public TModel Get(long id)
        {
            return dbSet.FirstOrDefault(m => m.ID == id);
        }

    }
}
