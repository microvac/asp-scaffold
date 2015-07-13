using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Validation;

namespace Scaffold.Validation
{
    public class Invalid: ModelValidationResult
    {
        public Invalid(String memberName, String message)
        {
            MemberName = memberName;
            Message = message;
        }
    }
}
