using Scaffold.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.Validation;
using System.Net.Http;

namespace Scaffold.ControllerExtensions
{
    public static class ApiControllerExtension
    {
        public static void ValidateWith(this ApiController controller, IList<Invalid> invalids)
        {
            if (invalids.Count > 0)
            {
                ModelStateDictionary modelState = new ModelStateDictionary();
                foreach (var invalid in invalids)
                    modelState.AddModelError(invalid.MemberName, invalid.Message);
                throw new HttpResponseException(controller.Request.CreateErrorResponse(HttpStatusCode.BadRequest, modelState));
            }
        }
    }
}
