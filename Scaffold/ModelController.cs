using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Scaffold
{
    public class ModelController<TModel>: ApiController
        where TModel: Model
    {
        protected IDictionary<long, TModel> models;

        public ModelController(IDictionary<long, TModel> models)
        {
            this.models = models;
        }

    }
}
