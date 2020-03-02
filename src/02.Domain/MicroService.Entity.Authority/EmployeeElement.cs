using MicroService.Core;
using MicroService.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

 namespace MicroService.Entity.Authority
 {
	/// <summary>
	/// EmployeeElement --实体
	/// </summary>
	public class EmployeeElement:Entity<string>
	{
  		//<summary>
		// 员工Id
		//<summary>
		[StringLength(36)]
		[Required]
		public string EmployeeId { set; get; }
		//<summary>
		// 元素Id
		//<summary>
		[StringLength(36)]
		[Required]
		public string ElementId { set; get; }
 
	}
}