using MicroService.Core;
using MicroService.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

 namespace MicroService.Entity.Authority
 {
	/// <summary>
	/// EmployeeMenu --实体
	/// </summary>
	public class EmployeeMenu:Entity<string>
	{
  		//<summary>
		// 员工Id
		//<summary>
		[StringLength(36)]
		[Required]
		public string EmployeeId { set; get; }
		//<summary>
		// 菜单Id
		//<summary>
		[StringLength(36)]
		[Required]
		public string MenuId { set; get; }
 
	}
}