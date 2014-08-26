using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Scaffold
{
    public class ReadOnlyController<TModel, TId>: ModelController<TModel, TId>
        where TModel: Model<TId>, new()
    {
        public ReadOnlyController(DbContext dbContext): base(dbContext) { }

        private List<Expression<Func<TModel, Object>>> singleIncludes = 
            new List<Expression<Func<TModel,object>>>();

        private List<Expression<Func<TModel, Object>>> listIncludes = 
            new List<Expression<Func<TModel,object>>>();

        public IEnumerable<TModel> GetAll()
        {
            IQueryable<TModel> exp = dbSet;
            foreach (var include in listIncludes)
            {
                exp = exp.Include(include);
            }
            return exp;
        }

        public TModel Get(TId id)
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

        protected ReadOnlyController<TModel, TId> SingleInclude(Expression<Func<TModel, Object>> include)
        {
            singleIncludes.Add(include);
            return this;
        }

        protected ReadOnlyController<TModel, TId> ListInclude(Expression<Func<TModel, Object>> include)
        {
            listIncludes.Add(include);
            return this;
        }

    }
}
