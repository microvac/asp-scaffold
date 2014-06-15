using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Scaffold
{
    public class ModelController<TModel>: ApiController
        where TModel: Model, new()
    {
        protected DbContext dbContext;
        protected DbSet<TModel> dbSet;

        public ModelController(DbContext dbContext)
        {
            this.dbContext = dbContext;
            this.dbSet = dbContext.Set<TModel>();
        }

    }
}
