using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
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

        public virtual HttpResponseMessage Delete(TId id)
        {
            var model = new TModel { ID = id };
            dbContext.Entry(model).State = EntityState.Deleted;
            dbContext.SaveChanges();
            return new HttpResponseMessage();
        }

        public virtual HttpResponseMessage Post([FromBody] TModel model)
        {
            PrePersist(model);
            dbSet.Add(model);
            dbContext.SaveChanges();
            PostPersist(model);
            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, model.ID);
        }

        public virtual HttpResponseMessage Put([FromBody] TModel model)
        {
            PrePersist(model);
            dbContext.Entry(model).State = EntityState.Modified;
            dbContext.SaveChanges();
            PostPersist(model);
            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, model.ID);
        }

        protected virtual void PrePersist(TModel model) { }
        protected virtual void PostPersist(TModel model) { }
    }

    public class CRUDController<TModel, TId >: CRUDController<TModel, TId, DefaultQuery<TModel>>
        where TModel: Model<TId>, new()
    {
        public CRUDController(DbContext dbContext): base(dbContext) { }
    }
}
