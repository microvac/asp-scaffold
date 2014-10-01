using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Linq.Dynamic;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Scaffold
{
    public class ReadOnlyController<TModel, TId, TQuery>: ModelController<TModel, TId>
        where TModel: class, IModel<TId>, new() 
        where TQuery: IQuery<TModel>, new()
    {
        public ReadOnlyController(DbContext dbContext) : base(dbContext) { IDField = "ID"; }

        public string IDField { get; set; }

        private List<Expression<Func<TModel, Object>>> SingleIncludes = 
            new List<Expression<Func<TModel,object>>>();

        private List<Expression<Func<TModel, Object>>> ListIncludes = 
            new List<Expression<Func<TModel,object>>>();

        public virtual IEnumerable<TModel> GetAll([FromUri] TQuery query)
        {
            IQueryable<TModel> exp = dbSet;
            foreach (var include in ListIncludes)
            {
                exp = exp.Include(include);
            }
            if (query != null)
                exp = query.Page(query.Sort(query.Filter(exp)));
            return exp;
        }

        public virtual long GetCount([FromUri] TQuery query)
        {
            IQueryable<TModel> exp = dbSet;
            if (query != null)
                exp = query.Sort(exp);
            return exp.LongCount();
        }

        public virtual TModel Get(TId id)
        {
            var exp = dbSet.Where(IDField + "=" + id);
            foreach (var include in SingleIncludes)
            {
                exp = exp.Include(include);
            }
            return exp.Single();
        }

        protected void SingleInclude(params Expression<Func<TModel, Object>>[] includes)
        {
            foreach(var include in includes)
                SingleIncludes.Add(include);
        }

        protected void ListInclude(params Expression<Func<TModel, Object>>[] includes)
        {
            foreach(var include in includes)
                ListIncludes.Add(include);
        }
        protected void Include(params Expression<Func<TModel, Object>>[] includes)
        {
            foreach (var include in includes) { 
                SingleIncludes.Add(include);
                ListIncludes.Add(include);
            }
        }

    }

    public class ReadOnlyController<TModel, TId>: ReadOnlyController<TModel, TId, DefaultQuery<TModel>>
        where TModel: class, IModel<TId>, new() 
    {
        public ReadOnlyController(DbContext dbContext): base(dbContext) { }
    }
}
