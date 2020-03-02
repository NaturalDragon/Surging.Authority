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
    public class OrganizationElementValidatorsFilter
    {
          /// <summary>
        ///异步验证
        /// </summary>
        public static async Task DoValidationAsync(IOrganizationElementRespository organizationElementRespository,OrganizationElement organizationElement, string validatorType)
        {
            var organizationElementValidator = new OrganizationElementValidator(organizationElementRespository);
            var validatorReresult = await organizationElementValidator.DoValidateAsync(organizationElement, validatorType);
            if (!validatorReresult.IsValid)
            {
                throw new DomainException(validatorReresult);
            }
        }

        /// <summary>
        ///异步验证
        /// </summary>
        public static async Task DoValidationAsync(IOrganizationElementRespository organizationElementRespository,IEnumerable<OrganizationElement> organizationElements, string validatorType)
        {
            var organizationElementValidator = new OrganizationElementValidator(organizationElementRespository);
            var domainException = new DomainException();
            foreach (var organizationElement in organizationElements)
            {
                var validatorReresult = await organizationElementValidator.DoValidateAsync(organizationElement, validatorType);
                if (!validatorReresult.IsValid)
                {
                    domainException.AddErrors(validatorReresult);
                }

            }

            if (domainException.ValidationErrors.ErrorItems.Any()) throw domainException;
        }
    }
}