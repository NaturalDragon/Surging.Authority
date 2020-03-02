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
	 public  class OrganizationValidator : AbstractValidator<Organization>
	 {
        public OrganizationValidator(IOrganizationRespository organizationRespository)
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
			
		
										
			RuleFor(m => m.Name).NotNull().WithMessage(x=>string.Format(ErrorMessage.IsRequired,"名称"));
							RuleFor(m => m.Name).Length(0,255).WithMessage(x=>string.Format(ErrorMessage.IsLengthError, 255));
											
			RuleFor(m => m.ParentId).NotNull().WithMessage(x=>string.Format(ErrorMessage.IsRequired,"父级Id"));
							RuleFor(m => m.ParentId).Length(0,36).WithMessage(x=>string.Format(ErrorMessage.IsLengthError, 36));
											
			RuleFor(m => m.Order).NotNull().WithMessage(x=>string.Format(ErrorMessage.IsRequired,"次序值"));
							RuleFor(m => m.Order).Length(0,10).WithMessage(x=>string.Format(ErrorMessage.IsLengthError, 10));
											
			RuleFor(m => m.DepartmentId).NotNull().WithMessage(x=>string.Format(ErrorMessage.IsRequired,"部门Id"));
																			RuleFor(m => m.DeptManagerUserIdList).Length(0,255).WithMessage(x=>string.Format(ErrorMessage.IsLengthError, 255));
														RuleFor(m => m.DeptPerimits).Length(0,255).WithMessage(x=>string.Format(ErrorMessage.IsLengthError, 255));
										RuleFor(m => m.UserPerimits).Length(0,255).WithMessage(x=>string.Format(ErrorMessage.IsLengthError, 255));
														RuleFor(m => m.OuterPermitDepts).Length(0,255).WithMessage(x=>string.Format(ErrorMessage.IsLengthError, 255));
										RuleFor(m => m.OuterPerimitUsers).Length(0,255).WithMessage(x=>string.Format(ErrorMessage.IsLengthError, 255));
										RuleFor(m => m.OrgDeptOwner).Length(0,255).WithMessage(x=>string.Format(ErrorMessage.IsLengthError, 255));
			         }
	   }

 }