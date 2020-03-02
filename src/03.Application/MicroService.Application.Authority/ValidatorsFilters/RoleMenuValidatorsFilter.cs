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
    public class RoleMenuValidatorsFilter
    {
          /// <summary>
        ///异步验证
        /// </summary>
        public static async Task DoValidationAsync(IRoleMenuRespository roleMenuRespository,RoleMenu roleMenu, string validatorType)
        {
            var roleMenuValidator = new RoleMenuValidator(roleMenuRespository);
            var validatorReresult = await roleMenuValidator.DoValidateAsync(roleMenu, validatorType);
            if (!validatorReresult.IsValid)
            {
                throw new DomainException(validatorReresult);
            }
        }

        /// <summary>
        ///异步验证
        /// </summary>
        public static async Task DoValidationAsync(IRoleMenuRespository roleMenuRespository,IEnumerable<RoleMenu> roleMenus, string validatorType)
        {
            var roleMenuValidator = new RoleMenuValidator(roleMenuRespository);
            var domainException = new DomainException();
            foreach (var roleMenu in roleMenus)
            {
                var validatorReresult = await roleMenuValidator.DoValidateAsync(roleMenu, validatorType);
                if (!validatorReresult.IsValid)
                {
                    domainException.AddErrors(validatorReresult);
                }

            }

            if (domainException.ValidationErrors.ErrorItems.Any()) throw domainException;
        }
    }
}