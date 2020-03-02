using MicroService.Core;
using MicroService.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

 namespace MicroService.Entity.Authority
 {
	/// <summary>
	/// OrganizationElement --实体
	/// </summary>
	public class OrganizationElement:Entity<string>
	{
  		//<summary>
		// 机构Id
		//<summary>
		[StringLength(36)]
		[Required]
		public string OrganizationId { set; get; }
		//<summary>
		// 元素Id
		//<summary>
		[StringLength(36)]
		[Required]
		public string ElementId { set; get; }
 
	}
}