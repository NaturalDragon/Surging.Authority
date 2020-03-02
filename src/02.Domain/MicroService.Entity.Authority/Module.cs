using MicroService.Core;
using MicroService.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

 namespace MicroService.Entity.Authority
 {
	/// <summary>
	/// Module --实体
	/// </summary>
	public class Module:Entity<string>
	{
  		//<summary>
		// 名称
		//<summary>
		[StringLength(100)]
			public string Name { set; get; }
		//<summary>
		// 路由路径
		//<summary>
		[StringLength(255)]
			public string Url { set; get; }
		//<summary>
		// 启用
		//<summary>
		[Required]
		public bool IsEnabled { set; get; }
 
	}
}