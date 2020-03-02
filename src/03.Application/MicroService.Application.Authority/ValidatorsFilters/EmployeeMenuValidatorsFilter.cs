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
    public class EmployeeMenuValidatorsFilter
    {
          /// <summary>
        ///异步验证
        /// </summary>
        public static async Task DoValidationAsync(IEmployeeMenuRespository employeeMenuRespository,EmployeeMenu employeeMenu, string validatorType)
        {
            var employeeMenuValidator = new EmployeeMenuValidator(employeeMenuRespository);
            var validatorReresult = await employeeMenuValidator.DoValidateAsync(employeeMenu, validatorType);
            if (!validatorReresult.IsValid)
            {
                throw new DomainException(validatorReresult);
            }
        }

        /// <summary>
        ///异步验证
        /// </summary>
        public static async Task DoValidationAsync(IEmployeeMenuRespository employeeMenuRespository,IEnumerable<EmployeeMenu> employeeMenus, string validatorType)
        {
            var employeeMenuValidator = new EmployeeMenuValidator(employeeMenuRespository);
            var domainException = new DomainException();
            foreach (var employeeMenu in employeeMenus)
            {
                var validatorReresult = await employeeMenuValidator.DoValidateAsync(employeeMenu, validatorType);
                if (!validatorReresult.IsValid)
                {
                    domainException.AddErrors(validatorReresult);
                }

            }

            if (domainException.ValidationErrors.ErrorItems.Any()) throw domainException;
        }
    }
}