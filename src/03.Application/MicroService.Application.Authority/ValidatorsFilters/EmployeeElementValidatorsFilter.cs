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
    public class EmployeeElementValidatorsFilter
    {
          /// <summary>
        ///异步验证
        /// </summary>
        public static async Task DoValidationAsync(IEmployeeElementRespository employeeElementRespository,EmployeeElement employeeElement, string validatorType)
        {
            var employeeElementValidator = new EmployeeElementValidator(employeeElementRespository);
            var validatorReresult = await employeeElementValidator.DoValidateAsync(employeeElement, validatorType);
            if (!validatorReresult.IsValid)
            {
                throw new DomainException(validatorReresult);
            }
        }

        /// <summary>
        ///异步验证
        /// </summary>
        public static async Task DoValidationAsync(IEmployeeElementRespository employeeElementRespository,IEnumerable<EmployeeElement> employeeElements, string validatorType)
        {
            var employeeElementValidator = new EmployeeElementValidator(employeeElementRespository);
            var domainException = new DomainException();
            foreach (var employeeElement in employeeElements)
            {
                var validatorReresult = await employeeElementValidator.DoValidateAsync(employeeElement, validatorType);
                if (!validatorReresult.IsValid)
                {
                    domainException.AddErrors(validatorReresult);
                }

            }

            if (domainException.ValidationErrors.ErrorItems.Any()) throw domainException;
        }
    }
}