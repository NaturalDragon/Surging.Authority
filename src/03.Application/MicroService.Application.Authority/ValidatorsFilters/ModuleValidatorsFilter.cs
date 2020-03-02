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
    public class ModuleValidatorsFilter
    {
          /// <summary>
        ///异步验证
        /// </summary>
        public static async Task DoValidationAsync(IModuleRespository moduleRespository,Module module, string validatorType)
        {
            var moduleValidator = new ModuleValidator(moduleRespository);
            var validatorReresult = await moduleValidator.DoValidateAsync(module, validatorType);
            if (!validatorReresult.IsValid)
            {
                throw new DomainException(validatorReresult);
            }
        }

        /// <summary>
        ///异步验证
        /// </summary>
        public static async Task DoValidationAsync(IModuleRespository moduleRespository,IEnumerable<Module> modules, string validatorType)
        {
            var moduleValidator = new ModuleValidator(moduleRespository);
            var domainException = new DomainException();
            foreach (var module in modules)
            {
                var validatorReresult = await moduleValidator.DoValidateAsync(module, validatorType);
                if (!validatorReresult.IsValid)
                {
                    domainException.AddErrors(validatorReresult);
                }

            }

            if (domainException.ValidationErrors.ErrorItems.Any()) throw domainException;
        }
    }
}