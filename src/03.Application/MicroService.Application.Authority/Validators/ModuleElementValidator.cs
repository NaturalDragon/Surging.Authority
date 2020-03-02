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
	 public  class ModuleElementValidator : AbstractValidator<ModuleElement>
	 {
        public ModuleElementValidator(IModuleElementRespository moduleElementRespository)
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
			
		
										
			RuleFor(m => m.ModuleId).NotNull().WithMessage(x=>string.Format(ErrorMessage.IsRequired,"模块Id"));
							RuleFor(m => m.ModuleId).Length(0,36).WithMessage(x=>string.Format(ErrorMessage.IsLengthError, 36));
											
			RuleFor(m => m.Name).NotNull().WithMessage(x=>string.Format(ErrorMessage.IsRequired,"元素名称"));
							RuleFor(m => m.Name).Length(0,64).WithMessage(x=>string.Format(ErrorMessage.IsLengthError, 64));
											
			RuleFor(m => m.RoutePath).NotNull().WithMessage(x=>string.Format(ErrorMessage.IsRequired,"操作元素"));
							RuleFor(m => m.RoutePath).Length(0,128).WithMessage(x=>string.Format(ErrorMessage.IsLengthError, 128));
											
			RuleFor(m => m.sortIndex).NotNull().WithMessage(x=>string.Format(ErrorMessage.IsRequired,"排序"));
				         }
	   }

 }