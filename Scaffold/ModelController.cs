using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Scaffold
{
    public class ModelController<TModel, TId>: ApiController
        where TModel: class, IModel<TId>, new()
    {
        protected DbContext dbContext;
        protected DbSet<TModel> dbSet;

        public ModelController(DbContext dbContext)
        {
            this.dbContext = dbContext;
            this.dbSet = dbContext.Set<TModel>();
        }

        public class Updator<TModel, TID>
            where TModel: class, IModel<TId>, new()
        {
            DbContext dbContext;
            DbSet<TModel> dbSet;
            TModel  model;
            public Updator(DbContext dbContext, DbSet<TModel> dbSet, TModel model)
            {
                this.dbContext = dbContext;
                this.dbSet = dbSet;
                this.model = model;
            }

            public Updator<TModel, TID> Set<TProperty>(Expression<Func<TModel, TProperty>> memberLamda, TProperty value)
            {
                var memberSelectorExpression = memberLamda.Body as MemberExpression;
                var property = memberSelectorExpression.Member as PropertyInfo;
                property.SetValue(model, value, null);

                dbContext.Entry<TModel>(model).Property(memberLamda).IsModified = true;

                return this;
            }
            public Updator<TModel, TID> Set<TProperty>(Expression<Func<TModel, TProperty>> memberLamda)
            {
                dbContext.Entry<TModel>(model).Property(memberLamda).IsModified = true;
                return this;
            }

            public void Save()
            {
                dbContext.SaveChanges();
            }
        }

        protected Updator<TModel, TId> Update(TId id)
        {
            return Update(dbContext, id);
        }

        public static Updator<TModel, TId> Update(DbContext dbContext, TId id)
        {
            var model = new TModel();
            model.Id = id;
            dbContext.Set<TModel>().Attach(model);
            return new Updator<TModel, TId>(dbContext, dbContext.Set<TModel>(), model);
        }
        protected Updator<TModel, TId> Update(TModel model)
        {
            dbSet.Attach(model);
            return new Updator<TModel, TId>(dbContext, dbSet, model);
        }
    }
}
