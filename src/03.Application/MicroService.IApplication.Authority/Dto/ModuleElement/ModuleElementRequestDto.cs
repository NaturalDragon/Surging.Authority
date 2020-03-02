
using MicroService.Core;
using MicroService.Data.Common;
using MicroService.Data.Enums;
using MicroService.Common.Models;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

 namespace MicroService.IApplication.Authority.Dto
 {
	/// <summary>
	/// ModuleElement --dto
	/// </summary>
    [ProtoContract]
    [Serializable]
	public class ModuleElementRequestDto:LoginUser
	{

		//<summary>
		// 模块Id
		//<summary>
		public string ModuleId { set; get; }

		//<summary>
		// 元素名称
		//<summary>
		public string Name { set; get; }

		//<summary>
		// 操作元素
		//<summary>
		public string RoutePath { set; get; }

		//<summary>
		// 排序
		//<summary>
		public int sortIndex { set; get; }
 
	}
}