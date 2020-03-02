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
    public class MenuValidatorsFilter
    {
          /// <summary>
        ///异步验证
        /// </summary>
        public static async Task DoValidationAsync(IMenuRespository menuRespository,Menu menu, string validatorType)
        {
            var menuValidator = new MenuValidator(menuRespository);
            var validatorReresult = await menuValidator.DoValidateAsync(menu, validatorType);
            if (!validatorReresult.IsValid)
            {
                throw new DomainException(validatorReresult);
            }
        }

        /// <summary>
        ///异步验证
        /// </summary>
        public static async Task DoValidationAsync(IMenuRespository menuRespository,IEnumerable<Menu> menus, string validatorType)
        {
            var menuValidator = new MenuValidator(menuRespository);
            var domainException = new DomainException();
            foreach (var menu in menus)
            {
                var validatorReresult = await menuValidator.DoValidateAsync(menu, validatorType);
                if (!validatorReresult.IsValid)
                {
                    domainException.AddErrors(validatorReresult);
                }

            }

            if (domainException.ValidationErrors.ErrorItems.Any()) throw domainException;
        }
    }
}