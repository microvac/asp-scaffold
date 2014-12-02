using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Linq.Dynamic;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Web.Http;
using System.Collections.Specialized;

namespace Scaffold
{
    public class ReadOnlyController<TModel, TId>: ModelController<TModel, TId>
        where TModel: class, IModel<TId>, new() 
    {
        public ReadOnlyController(DbContext dbContext) : base(dbContext) { IDField = "ID"; }
        public string IDField { get; set; }

        protected List<Expression<Func<TModel, Object>>> SingleIncludes = 
            new List<Expression<Func<TModel,object>>>();

        protected List<Expression<Func<TModel, Object>>> ListIncludes = 
            new List<Expression<Func<TModel,object>>>();

        private IEnumerable<KeyValuePair<string, string>> queryStrings;

        public virtual IQueryable<TModel> GetAll()
        {
            IQueryable<TModel> exp = dbSet;
            
            foreach (var include in ListIncludes)
            {
                exp = exp.Include(include);
            }
            
            exp = ApplyQuery(exp);
            exp = ApplyPageAndSort(exp);
            return exp;
        }

        public virtual long GetCount()
        {
            IQueryable<TModel> exp = dbSet;
            exp = ApplyQuery(exp);
            var result = exp.LongCount();
            return result;
        }

        public virtual TModel Get(TId id)
        {
            IQueryable<TModel> exp = null;
            
            if(typeof(TId) == typeof(String))
                exp = dbSet.Where(IDField + "=\"" + id +"\"");
            else
                exp = dbSet.Where(IDField + "=" + id);
            
            foreach (var include in SingleIncludes)
            {
                exp = exp.Include(include);
            }

            var result = exp.SingleOrDefault();
            return result;
        }

        protected virtual IQueryable<TModel> ApplyQuery(IQueryable<TModel> query)
        {
            return query;
        }

        protected virtual IQueryable<TModel> ApplyPageAndSort(IQueryable<TModel> query)
        {
            var pageBegin = GetQueryString<int>("PageBegin", 1);
            var pageLength = GetQueryString<int>("PageLength", 0);
            var sortOrder = GetQueryString<string>("SortOrder", "ASC");
            var sortField = GetQueryString<string>("SortField", IDField);
            
            query = Sort(query, sortField, sortOrder);
            query = Page(query, pageBegin, pageLength);
            return query;
        }

        protected virtual TResult GetQueryString<TResult>(String key, TResult defaultValue = default(TResult))
        {
            if (queryStrings == null)
                queryStrings = Request.GetQueryNameValuePairs();

            var match = queryStrings.FirstOrDefault(kv => string.Compare(kv.Key, key, true) == 0);
            if (string.IsNullOrWhiteSpace(match.Value))
                return defaultValue;

            return (TResult)Convert.ChangeType(match.Value, typeof(TResult));
        }

        protected virtual IQueryable<TModel> Sort(IQueryable<TModel> query, string sortField, string sortOrder)
        {
            if (sortOrder != "ASC" && sortOrder != "DESC")
                sortOrder = "ASC";
            return query.OrderBy(sortField.Trim() + " " + sortOrder.Trim());
        }

        protected virtual IQueryable<TModel> Page(IQueryable<TModel> query, int pageBegin, int pageLength)
        {
            if (pageBegin > 0)
                query = query.Skip((pageBegin - 1) * pageLength);
            if (pageLength > 0)
                query = query.Take(pageLength);
            return query;
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
   
}
