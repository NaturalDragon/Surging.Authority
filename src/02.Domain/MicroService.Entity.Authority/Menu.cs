using MicroService.Core;
using MicroService.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

 namespace MicroService.Entity.Authority
 {
	/// <summary>
	/// Menu --实体
	/// </summary>
	public class Menu:Entity<string>
	{
  		//<summary>
		// 名称
		//<summary>
		[StringLength(100)]
			public string Name { set; get; }
		//<summary>
		// 菜单图标
		//<summary>
		[StringLength(32)]
			public string Icon { set; get; }
		//<summary>
		// 模块Id
		//<summary>
		[StringLength(36)]
			public string ModuleId { set; get; }
		//<summary>
		// 父级
		//<summary>
		[StringLength(36)]
			public string ParentId { set; get; }

        /// <summary>
        /// 排序
        /// </summary>
        [Required]
        public int SortIndex { set; get; }


    }
}