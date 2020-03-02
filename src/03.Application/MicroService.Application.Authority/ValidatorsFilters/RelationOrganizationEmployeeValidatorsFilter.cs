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
    public class RelationOrganizationEmployeeValidatorsFilter
    {
          /// <summary>
        ///异步验证
        /// </summary>
        public static async Task DoValidationAsync(IRelationOrganizationEmployeeRespository relationOrganizationEmployeeRespository,RelationOrganizationEmployee relationOrganizationEmployee, string validatorType)
        {
            var relationOrganizationEmployeeValidator = new RelationOrganizationEmployeeValidator(relationOrganizationEmployeeRespository);
            var validatorReresult = await relationOrganizationEmployeeValidator.DoValidateAsync(relationOrganizationEmployee, validatorType);
            if (!validatorReresult.IsValid)
            {
                throw new DomainException(validatorReresult);
            }
        }

        /// <summary>
        ///异步验证
        /// </summary>
        public static async Task DoValidationAsync(IRelationOrganizationEmployeeRespository relationOrganizationEmployeeRespository,IEnumerable<RelationOrganizationEmployee> relationOrganizationEmployees, string validatorType)
        {
            var relationOrganizationEmployeeValidator = new RelationOrganizationEmployeeValidator(relationOrganizationEmployeeRespository);
            var domainException = new DomainException();
            foreach (var relationOrganizationEmployee in relationOrganizationEmployees)
            {
                var validatorReresult = await relationOrganizationEmployeeValidator.DoValidateAsync(relationOrganizationEmployee, validatorType);
                if (!validatorReresult.IsValid)
                {
                    domainException.AddErrors(validatorReresult);
                }

            }

            if (domainException.ValidationErrors.ErrorItems.Any()) throw domainException;
        }
    }
}