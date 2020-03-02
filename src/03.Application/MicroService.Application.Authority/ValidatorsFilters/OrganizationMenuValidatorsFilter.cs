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
    public class OrganizationMenuValidatorsFilter
    {
          /// <summary>
        ///异步验证
        /// </summary>
        public static async Task DoValidationAsync(IOrganizationMenuRespository organizationMenuRespository,OrganizationMenu organizationMenu, string validatorType)
        {
            var organizationMenuValidator = new OrganizationMenuValidator(organizationMenuRespository);
            var validatorReresult = await organizationMenuValidator.DoValidateAsync(organizationMenu, validatorType);
            if (!validatorReresult.IsValid)
            {
                throw new DomainException(validatorReresult);
            }
        }

        /// <summary>
        ///异步验证
        /// </summary>
        public static async Task DoValidationAsync(IOrganizationMenuRespository organizationMenuRespository,IEnumerable<OrganizationMenu> organizationMenus, string validatorType)
        {
            var organizationMenuValidator = new OrganizationMenuValidator(organizationMenuRespository);
            var domainException = new DomainException();
            foreach (var organizationMenu in organizationMenus)
            {
                var validatorReresult = await organizationMenuValidator.DoValidateAsync(organizationMenu, validatorType);
                if (!validatorReresult.IsValid)
                {
                    domainException.AddErrors(validatorReresult);
                }

            }

            if (domainException.ValidationErrors.ErrorItems.Any()) throw domainException;
        }
    }
}