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
	 public  class EmployeeValidator : AbstractValidator<Employee>
	 {
        public EmployeeValidator(IEmployeeRespository employeeRespository)
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
			
		
										
			RuleFor(m => m.UserId).NotNull().WithMessage(x=>string.Format(ErrorMessage.IsRequired,"如果职员是通过第三方系统导入的，这个字段记录第三方系统中的用户唯一标识"));
							RuleFor(m => m.UserId).Length(0,255).WithMessage(x=>string.Format(ErrorMessage.IsLengthError, 255));
											
			RuleFor(m => m.Name).NotNull().WithMessage(x=>string.Format(ErrorMessage.IsRequired,"姓名"));
							RuleFor(m => m.Name).Length(0,100).WithMessage(x=>string.Format(ErrorMessage.IsLengthError, 100));
											
			RuleFor(m => m.Password).NotNull().WithMessage(x=>string.Format(ErrorMessage.IsRequired,"密码"));
							RuleFor(m => m.Password).Length(0,255).WithMessage(x=>string.Format(ErrorMessage.IsLengthError, 255));
											
			RuleFor(m => m.Department).NotNull().WithMessage(x=>string.Format(ErrorMessage.IsRequired,"部门"));
							RuleFor(m => m.Department).Length(0,200).WithMessage(x=>string.Format(ErrorMessage.IsLengthError, 200));
											
			RuleFor(m => m.Order).NotNull().WithMessage(x=>string.Format(ErrorMessage.IsRequired,"部门内的排序值"));
							RuleFor(m => m.Order).Length(0,200).WithMessage(x=>string.Format(ErrorMessage.IsLengthError, 200));
										RuleFor(m => m.Position).Length(0,100).WithMessage(x=>string.Format(ErrorMessage.IsLengthError, 100));
											
			RuleFor(m => m.Mobile).NotNull().WithMessage(x=>string.Format(ErrorMessage.IsRequired,"手机"));
							RuleFor(m => m.Mobile).Length(0,64).WithMessage(x=>string.Format(ErrorMessage.IsLengthError, 64));
										RuleFor(m => m.Telephone).Length(0,64).WithMessage(x=>string.Format(ErrorMessage.IsLengthError, 64));
											
			RuleFor(m => m.Gender).NotNull().WithMessage(x=>string.Format(ErrorMessage.IsRequired,"性别"));
											RuleFor(m => m.Email).Length(0,64).WithMessage(x=>string.Format(ErrorMessage.IsLengthError, 64));
										RuleFor(m => m.WorkPlace).Length(0,255).WithMessage(x=>string.Format(ErrorMessage.IsLengthError, 255));
										RuleFor(m => m.EnglishName).Length(0,64).WithMessage(x=>string.Format(ErrorMessage.IsLengthError, 64));
										RuleFor(m => m.Lang).Length(0,64).WithMessage(x=>string.Format(ErrorMessage.IsLengthError, 64));
										RuleFor(m => m.Jobnumber).Length(0,64).WithMessage(x=>string.Format(ErrorMessage.IsLengthError, 64));
										RuleFor(m => m.Salt).Length(0,32).WithMessage(x=>string.Format(ErrorMessage.IsLengthError, 32));
																						RuleFor(m => m.Extra).Length(0,255).WithMessage(x=>string.Format(ErrorMessage.IsLengthError, 255));
																						RuleFor(m => m.Avatar).Length(0,255).WithMessage(x=>string.Format(ErrorMessage.IsLengthError, 255));
										RuleFor(m => m.LastLoginRoleId).Length(0,36).WithMessage(x=>string.Format(ErrorMessage.IsLengthError, 36));
			         }
	   }

 }