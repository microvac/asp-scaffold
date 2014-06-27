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

        public IEnumerable<TModel> GetAll()
        {
            return dbSet;
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
            return dbSet.Where(whereExpression).Single();
        }

    }
}
