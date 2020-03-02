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
    public class RelationEmployeeRoleValidatorsFilter
    {
          /// <summary>
        ///异步验证
        /// </summary>
        public static async Task DoValidationAsync(IRelationEmployeeRoleRespository relationEmployeeRoleRespository,RelationEmployeeRole relationEmployeeRole, string validatorType)
        {
            var relationEmployeeRoleValidator = new RelationEmployeeRoleValidator(relationEmployeeRoleRespository);
            var validatorReresult = await relationEmployeeRoleValidator.DoValidateAsync(relationEmployeeRole, validatorType);
            if (!validatorReresult.IsValid)
            {
                throw new DomainException(validatorReresult);
            }
        }

        /// <summary>
        ///异步验证
        /// </summary>
        public static async Task DoValidationAsync(IRelationEmployeeRoleRespository relationEmployeeRoleRespository,IEnumerable<RelationEmployeeRole> relationEmployeeRoles, string validatorType)
        {
            var relationEmployeeRoleValidator = new RelationEmployeeRoleValidator(relationEmployeeRoleRespository);
            var domainException = new DomainException();
            foreach (var relationEmployeeRole in relationEmployeeRoles)
            {
                var validatorReresult = await relationEmployeeRoleValidator.DoValidateAsync(relationEmployeeRole, validatorType);
                if (!validatorReresult.IsValid)
                {
                    domainException.AddErrors(validatorReresult);
                }

            }

            if (domainException.ValidationErrors.ErrorItems.Any()) throw domainException;
        }
    }
}