using FluentValidation;
using Scaffold.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Validation;

namespace Scaffold.Validation
{
    public class MethodModelValidator: ModelValidator
    {
        public MethodModelValidator(IEnumerable<ModelValidatorProvider> validatorProviders)
            : base(validatorProviders)
        {

        }

        public class FluentValidator<T>: AbstractValidator<T>
        {
        }

        public override IEnumerable<ModelValidationResult> Validate(System.Web.Http.Metadata.ModelMetadata metadata, object container)
        {
            if (metadata.Model != null &&(metadata.ModelType.IsInstanceOfType(metadata.Model)))
            {
                var methods = metadata.ModelType.GetMethods();
                foreach (var method in methods)
                {
                    if (method.CustomAttributes.Any(e => e.AttributeType == typeof(ValidatorAttribute)))
                    {
                        var results = (IEnumerable<ModelValidationResult>)method.Invoke(metadata.Model, new object[0]);
                        foreach (var result in results)
                        {
                            yield return result;
                        }
                    }
                }
            }
        }
    }
}
