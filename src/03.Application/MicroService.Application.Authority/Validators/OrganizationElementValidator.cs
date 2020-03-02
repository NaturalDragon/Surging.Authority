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
	 public  class OrganizationElementValidator : AbstractValidator<OrganizationElement>
	 {
        public OrganizationElementValidator(IOrganizationElementRespository organizationElementRespository)
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
			
		
										
			RuleFor(m => m.OrganizationId).NotNull().WithMessage(x=>string.Format(ErrorMessage.IsRequired,"机构Id"));
							RuleFor(m => m.OrganizationId).Length(0,36).WithMessage(x=>string.Format(ErrorMessage.IsLengthError, 36));
											
			RuleFor(m => m.ElementId).NotNull().WithMessage(x=>string.Format(ErrorMessage.IsRequired,"元素Id"));
							RuleFor(m => m.ElementId).Length(0,36).WithMessage(x=>string.Format(ErrorMessage.IsLengthError, 36));
			         }
	   }

 }