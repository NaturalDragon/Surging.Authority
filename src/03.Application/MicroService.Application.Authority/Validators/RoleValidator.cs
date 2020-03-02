using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using MicroService.Data.Constant;
using MicroService.Data.Validation;
using MicroService.Entity.Authority;
using MicroService.IRespository.Authority;
 namespace MicroService.Application.Authority.Validators
 {
	 public  class RoleValidator : AbstractValidator<Role>
	 {
        public RoleValidator(IRoleRespository roleRespository)
        {
            RuleSet(ValidatorTypeConstants.Create, () =>
            {
                BaseValidator();
            });
            RuleSet(ValidatorTypeConstants.Modify, () =>
            {
                BaseValidator();
            });

        }

        void BaseValidator()
	     {
		    RuleFor(per => per.Id).NotNull().WithMessage(x => string.Format(ErrorMessage.IsRequired, "主键Id"));
            RuleFor(per => per.CreateDate).NotNull().WithMessage(x => string.Format(ErrorMessage.IsRequired, "创建时间CreateDate"));
            RuleFor(per => per.IsDelete).NotNull().WithMessage(x => string.Format(ErrorMessage.IsRequired, "删除状态IsDelete"));
		   
		    RuleFor(per => per.CreateUserId).NotNull().WithMessage(x => string.Format(ErrorMessage.IsRequired, "创建人CreateUserId"));
            RuleFor(per => per.CreateUserName).NotNull().WithMessage(x => string.Format(ErrorMessage.IsRequired, "创建人姓名CreateUserName"));
			
		
									RuleFor(m => m.Name).Length(0,100).WithMessage(x=>string.Format(ErrorMessage.IsLengthError, 100));
										RuleFor(m => m.CompanyId).Length(0,36).WithMessage(x=>string.Format(ErrorMessage.IsLengthError, 36));
											
			RuleFor(m => m.RoleType).NotNull().WithMessage(x=>string.Format(ErrorMessage.IsRequired,"职责类型 取值对应RoleType 0:职员职责(针对职员) 1:用户职责(针对管理员) "));
				         }
	   }

 }