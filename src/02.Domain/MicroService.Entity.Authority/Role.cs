using MicroService.Core;
using MicroService.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

 namespace MicroService.Entity.Authority
 {
	/// <summary>
	/// Role --实体
	/// </summary>
	public class Role:Entity<string>
	{
  		//<summary>
		// 名称
		//<summary>
		[StringLength(100)]
			public string Name { set; get; }
		//<summary>
		// 公司Id
		//<summary>
		[StringLength(36)]
			public string CompanyId { set; get; }
		//<summary>
		// 职责类型 取值对应RoleType 0:职员职责(针对职员) 1:用户职责(针对管理员) 
		//<summary>
		[Required]
		public int RoleType { set; get; }
 
	}
}