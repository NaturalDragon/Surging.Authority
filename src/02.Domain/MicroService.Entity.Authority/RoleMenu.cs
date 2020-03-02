using MicroService.Core;
using MicroService.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

 namespace MicroService.Entity.Authority
 {
	/// <summary>
	/// RoleMenu --实体
	/// </summary>
	public class RoleMenu:Entity<string>
	{
  		//<summary>
		// 角色Id
		//<summary>
		[StringLength(36)]
		[Required]
		public string RoleId { set; get; }
		//<summary>
		// 菜单Id
		//<summary>
		[StringLength(36)]
		[Required]
		public string MenuId { set; get; }
 
	}
}