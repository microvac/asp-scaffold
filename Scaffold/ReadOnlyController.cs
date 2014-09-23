using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Scaffold
{
    public class ReadOnlyController<TModel, TId, TQuery>: ModelController<TModel, TId>
        where TModel: Model<TId>, new() 
        where TQuery: IQuery<TModel>, new()
    {
        public ReadOnlyController(DbContext dbContext): base(dbContext) { }

        private List<Expression<Func<TModel, Object>>> singleIncludes = 
            new List<Expression<Func<TModel,object>>>();

        private List<Expression<Func<TModel, Object>>> listIncludes = 
            new List<Expression<Func<TModel,object>>>();

        public IEnumerable<TModel> GetAll([FromUri] TQuery query)
        {
            IQueryable<TModel> exp = dbSet;
            foreach (var include in listIncludes)
            {
                exp = exp.Include(include);
            }
            if (query != null)
                exp = query.Page(query.Sort(query.Filter(exp)));
            return exp;
        }

        public long GetCount([FromUri] TQuery query)
        {
            IQueryable<TModel> exp = dbSet;
            if (query != null)
                exp = query.Sort(exp);
            return exp.LongCount();
        }

        public TModel Get(TId id)
        {
            try
            {
                var itemParameter = Expression.Parameter(typeof(TModel), "item");
                var whereExpression = Expression.Lambda<Func<TModel, bool>>
                    (
                    Expression.Equal(
                        Expression.Property(
                            itemParameter,
                            "ID"
                            ),
                        Expression.Constant(id)
                        ),
                    new[] { itemParameter }
                    );
                var exp = dbSet.Where(whereExpression);
                foreach (var include in singleIncludes)
                {
                    exp = exp.Include(include);
                }

                return exp.Single();
            }

            catch (Exception ex)
            {
                return null;
            }
        }

        protected void SingleInclude(params Expression<Func<TModel, Object>>[] includes)
        {
            foreach(var include in includes)
                singleIncludes.Add(include);
        }

        protected void ListInclude(params Expression<Func<TModel, Object>>[] includes)
        {
            foreach(var include in includes)
                listIncludes.Add(include);
        }
        protected void Include(params Expression<Func<TModel, Object>>[] includes)
        {
            foreach (var include in includes) { 
                singleIncludes.Add(include);
                listIncludes.Add(include);
            }
        }

    }

    public class ReadOnlyController<TModel, TId>: ReadOnlyController<TModel, TId, DefaultQuery<TModel>>
        where TModel: Model<TId>, new() 
    {
        public ReadOnlyController(DbContext dbContext): base(dbContext) { }
    }
}
