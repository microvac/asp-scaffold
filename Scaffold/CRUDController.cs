using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Scaffold
{
    public class CRUDController<TModel>: ReadOnlyController<TModel>
        where TModel: Model
    {
        public CRUDController(IDictionary<long, TModel> models): base(models) { }

        public void Delete(long id)
        {
            models.Remove(id);
        }

        public void Post([FromBody] TModel model)
        {
            models[model.ID] = model;
        }

        public void Put(long id, [FromBody] TModel model)
        {
            models[id] = model;
        }
    }
}
