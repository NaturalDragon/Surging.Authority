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
    public class EmployeeValidatorsFilter
    {
          /// <summary>
        ///异步验证
        /// </summary>
        public static async Task DoValidationAsync(IEmployeeRespository employeeRespository,Employee employee, string validatorType)
        {
            var employeeValidator = new EmployeeValidator(employeeRespository);
            var validatorReresult = await employeeValidator.DoValidateAsync(employee, validatorType);
            if (!validatorReresult.IsValid)
            {
                throw new DomainException(validatorReresult);
            }
        }

        /// <summary>
        ///异步验证
        /// </summary>
        public static async Task DoValidationAsync(IEmployeeRespository employeeRespository,IEnumerable<Employee> employees, string validatorType)
        {
            var employeeValidator = new EmployeeValidator(employeeRespository);
            var domainException = new DomainException();
            foreach (var employee in employees)
            {
                var validatorReresult = await employeeValidator.DoValidateAsync(employee, validatorType);
                if (!validatorReresult.IsValid)
                {
                    domainException.AddErrors(validatorReresult);
                }

            }

            if (domainException.ValidationErrors.ErrorItems.Any()) throw domainException;
        }
    }
}