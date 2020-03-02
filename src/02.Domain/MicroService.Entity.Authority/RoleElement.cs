using MicroService.Core;
using MicroService.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

 namespace MicroService.Entity.Authority
 {
	/// <summary>
	/// RoleElement --实体
	/// </summary>
	public class RoleElement:Entity<string>
	{
  		//<summary>
		// 角色Id
		//<summary>
		[StringLength(36)]
		[Required]
		public string RoleId { set; get; }
		//<summary>
		// 元素Id
		//<summary>
		[StringLength(36)]
		[Required]
		public string ElementId { set; get; }
 
	}
}