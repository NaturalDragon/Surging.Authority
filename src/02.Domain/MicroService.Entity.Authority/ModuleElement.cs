using MicroService.Core;
using MicroService.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

 namespace MicroService.Entity.Authority
 {
	/// <summary>
	/// ModuleElement --实体
	/// </summary>
	public class ModuleElement : Entity<string>
	{
  		//<summary>
		// 模块Id
		//<summary>
		[StringLength(36)]
		[Required]
		public string ModuleId { set; get; }
		//<summary>
		// 元素名称
		//<summary>
		[StringLength(64)]
		[Required]
		public string Name { set; get; }
		//<summary>
		// 操作元素
		//<summary>
		[StringLength(128)]
		[Required]
		public string RoutePath { set; get; }
		//<summary>
		// 排序
		//<summary>
		[Required]
		public int sortIndex { set; get; }
 
	}
}