using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroService.Application.Authority.Validators;
using MicroService.Data.Extensions;
using MicroService.Data.Validation;
using MicroService.Entity.Authority;
using MicroService.IRespository.Authority;

namespace MicroService.Application.Authority.ValidatorsFilters
{
    public class ModuleElementValidatorsFilter
    {
          /// <summary>
        ///异步验证
        /// </summary>
        public static async Task DoValidationAsync(IModuleElementRespository moduleElementRespository,ModuleElement moduleElement, string validatorType)
        {
            var moduleElementValidator = new ModuleElementValidator(moduleElementRespository);
            var validatorReresult = await moduleElementValidator.DoValidateAsync(moduleElement, validatorType);
            if (!validatorReresult.IsValid)
            {
                throw new DomainException(validatorReresult);
            }
        }

        /// <summary>
        ///异步验证
        /// </summary>
        public static async Task DoValidationAsync(IModuleElementRespository moduleElementRespository,IEnumerable<ModuleElement> moduleElements, string validatorType)
        {
            var moduleElementValidator = new ModuleElementValidator(moduleElementRespository);
            var domainException = new DomainException();
            foreach (var moduleElement in moduleElements)
            {
                var validatorReresult = await moduleElementValidator.DoValidateAsync(moduleElement, validatorType);
                if (!validatorReresult.IsValid)
                {
                    domainException.AddErrors(validatorReresult);
                }

            }

            if (domainException.ValidationErrors.ErrorItems.Any()) throw domainException;
        }
    }
}