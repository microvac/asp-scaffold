using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Scaffold
{
    public class CRUDController<TModel>: ReadOnlyController<TModel>
        where TModel: Model, new()
    {
        public CRUDController(DbContext dbContext): base(dbContext) { }

        public void Delete(String id)
        {
            var model = new TModel { ID = id };
            dbContext.Entry(model).State = EntityState.Deleted;
            dbContext.SaveChanges();
        }

        public String Post([FromBody] TModel model)
        {
            dbSet.Add(model);
            dbContext.SaveChanges();
            return model.ID;
        }

        public void Put([FromBody] TModel model)
        {
            dbContext.Entry(model).State = EntityState.Modified;
            dbContext.SaveChanges();
        }
    }
}
