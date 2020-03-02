using MicroService.Core;
using MicroService.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

 namespace MicroService.Entity.Authority
 {
	/// <summary>
	/// OrganizationMenu --实体
	/// </summary>
	public class OrganizationMenu:Entity<string>
	{
  		//<summary>
		// 
		//<summary>
		[StringLength(36)]
		[Required]
		public string OrganizationId { set; get; }
		//<summary>
		// 
		//<summary>
		[StringLength(36)]
		[Required]
		public string MenuId { set; get; }
 
	}
}