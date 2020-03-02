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
    public class RoleValidatorsFilter
    {
          /// <summary>
        ///异步验证
        /// </summary>
        public static async Task DoValidationAsync(IRoleRespository roleRespository,Role role, string validatorType)
        {
            var roleValidator = new RoleValidator(roleRespository);
            var validatorReresult = await roleValidator.DoValidateAsync(role, validatorType);
            if (!validatorReresult.IsValid)
            {
                throw new DomainException(validatorReresult);
            }
        }

        /// <summary>
        ///异步验证
        /// </summary>
        public static async Task DoValidationAsync(IRoleRespository roleRespository,IEnumerable<Role> roles, string validatorType)
        {
            var roleValidator = new RoleValidator(roleRespository);
            var domainException = new DomainException();
            foreach (var role in roles)
            {
                var validatorReresult = await roleValidator.DoValidateAsync(role, validatorType);
                if (!validatorReresult.IsValid)
                {
                    domainException.AddErrors(validatorReresult);
                }

            }

            if (domainException.ValidationErrors.ErrorItems.Any()) throw domainException;
        }
    }
}