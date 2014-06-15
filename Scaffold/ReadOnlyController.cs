using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scaffold
{
    public class ReadOnlyController<TModel>: ModelController<TModel>
        where TModel: Model
    {
        public ReadOnlyController(IDictionary<long, TModel> models): base(models) { }

        public IEnumerable<TModel> GetAll()
        {
            return models.Values;
        }

        public TModel Get(long id)
        {
            return models[id];
        }

    }
}
