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
    public class RoleElementValidatorsFilter
    {
          /// <summary>
        ///异步验证
        /// </summary>
        public static async Task DoValidationAsync(IRoleElementRespository roleElementRespository,RoleElement roleElement, string validatorType)
        {
            var roleElementValidator = new RoleElementValidator(roleElementRespository);
            var validatorReresult = await roleElementValidator.DoValidateAsync(roleElement, validatorType);
            if (!validatorReresult.IsValid)
            {
                throw new DomainException(validatorReresult);
            }
        }

        /// <summary>
        ///异步验证
        /// </summary>
        public static async Task DoValidationAsync(IRoleElementRespository roleElementRespository,IEnumerable<RoleElement> roleElements, string validatorType)
        {
            var roleElementValidator = new RoleElementValidator(roleElementRespository);
            var domainException = new DomainException();
            foreach (var roleElement in roleElements)
            {
                var validatorReresult = await roleElementValidator.DoValidateAsync(roleElement, validatorType);
                if (!validatorReresult.IsValid)
                {
                    domainException.AddErrors(validatorReresult);
                }

            }

            if (domainException.ValidationErrors.ErrorItems.Any()) throw domainException;
        }
    }
}