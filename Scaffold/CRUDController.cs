using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Scaffold
{
    public class CRUDController<TModel, TId, TQuery>: ReadOnlyController<TModel, TId, TQuery>
        where TModel: Model<TId>, new()
        where TQuery: IQuery<TModel>, new()
    {
        public CRUDController(DbContext dbContext): base(dbContext) { }

        public void Delete(TId id)
        {
            var model = new TModel { ID = id };
            dbContext.Entry(model).State = EntityState.Deleted;
            dbContext.SaveChanges();
        }

        public TId Post([FromBody] TModel model)
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

    public class CRUDController<TModel, TId >: CRUDController<TModel, TId, DefaultQuery<TModel>>
        where TModel: Model<TId>, new()
    {
        public CRUDController(DbContext dbContext): base(dbContext) { }
    }
}
