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
	/// RoleElement --dto
	/// </summary>
    [ProtoContract]
    [Serializable]
	public class RoleElementQueryDto:BaseDto
	{

		//<summary>
		// 角色Id
		//<summary>
		public string RoleId { set; get; }

		//<summary>
		// 元素Id
		//<summary>
		public string ElementId { set; get; }
 
	}
}