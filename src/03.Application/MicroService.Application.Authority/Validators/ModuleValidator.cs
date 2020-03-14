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
	 public  class ModuleValidator : AbstractValidator<Module>
	 {
        public ModuleValidator(IModuleRespository moduleRespository)
        {
            RuleSet(ValidatorTypeConstants.Create, () =>
            {
                RuleFor(e => e.Name).Must((e, val) => !moduleRespository.Any(ea => ea.Name == val))
                  .WithMessage((e, val) => string.Format(ErrorMessage.IsNameRepeat, val));
                BaseValidator();
            });
            RuleSet(ValidatorTypeConstants.Modify, () =>
            {
                RuleFor(e => e.Name).Must((e, val) =>!moduleRespository.Any(ea => ea.Name == val && ea.Id != e.Id))
                   .WithMessage((e, val) => string.Format(ErrorMessage.IsNameRepeat, val));
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
										RuleFor(m => m.Url).Length(0,255).WithMessage(x=>string.Format(ErrorMessage.IsLengthError, 255));
											
			RuleFor(m => m.IsEnabled).NotNull().WithMessage(x=>string.Format(ErrorMessage.IsRequired,"启用"));
				         }
	   }

 }