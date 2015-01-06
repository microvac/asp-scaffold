using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Metadata;
using System.Web.Http.Validation;

namespace Scaffold.Validation
{
    public class MethodModelValidatorProvider: ModelValidatorProvider
    {

        public override IEnumerable<ModelValidator> GetValidators(ModelMetadata metadata, IEnumerable<ModelValidatorProvider> validatorProviders)
        {
            if (metadata.Model == null)
                return new ModelValidator[0];
            return new ModelValidator[] { new MethodModelValidator(validatorProviders) };
        }
    }
}
