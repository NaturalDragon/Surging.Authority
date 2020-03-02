using MicroService.Core;
using MicroService.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

 namespace MicroService.Entity.Authority
 {
	/// <summary>
	/// RelationEmployeeRole --实体
	/// </summary>
	public class RelationEmployeeRole:Entity<string>
	{
  		//<summary>
		// 
		//<summary>
		[StringLength(36)]
		[Required]
		public string RoleId { set; get; }
		//<summary>
		// 成员Id
		//<summary>
		[StringLength(36)]
		[Required]
		public string EmployeeId { set; get; }
 
	}
}