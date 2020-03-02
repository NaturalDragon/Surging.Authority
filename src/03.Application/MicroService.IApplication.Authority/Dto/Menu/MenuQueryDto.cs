using MicroService.Core;
using MicroService.Data.Enums;
using MicroService.Common.Models;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

 namespace MicroService.IApplication.Authority.Dto
 {
	/// <summary>
	/// Menu --dto
	/// </summary>
    [ProtoContract]
    [Serializable]
	public class MenuQueryDto:BaseDto
	{

		//<summary>
		// 名称
		//<summary>
		public string Name { set; get; }

		//<summary>
		// 菜单图标
		//<summary>
		public string Icon { set; get; }

		//<summary>
		// 模块Id
		//<summary>
		public string ModuleId { set; get; }

		//<summary>
		// 父级
		//<summary>
		public string ParentId { set; get; }

        public string Url { set; get; }


        /// <summary>
        /// 排序
        /// </summary>
        public int SortIndex { set; get; }

    }
}