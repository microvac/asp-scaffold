﻿using System;
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
        where TModel: class, IModel<TId>, new()
        where TQuery: IQuery<TModel>, new()
    {
        public CRUDController(DbContext dbContext): base(dbContext) { }


        public virtual void Delete(TId id)
        {
            var model = new TModel { ID = id };            
            dbContext.Entry(model).State = EntityState.Deleted;
            dbContext.SaveChanges();
        }

        public virtual TId Post([FromBody] TModel model)
        {
            PrePersist(model);
            dbSet.Add(model);
            dbContext.SaveChanges();
            PostPersist(model);
            return model.ID;
        }

        public virtual void Put([FromBody] TModel model)
        {
            PrePersist(model);
            dbContext.Entry(model).State = EntityState.Modified;
            dbContext.SaveChanges();
            PostPersist(model);
        }

        protected virtual void PrePersist(TModel model) { }
        protected virtual void PostPersist(TModel model) { }
    }

    public class CRUDController<TModel, TId >: CRUDController<TModel, TId, DefaultQuery<TModel>>
        where TModel: class, IModel<TId>, new()
    {
        public CRUDController(DbContext dbContext): base(dbContext) { }
    }
}
