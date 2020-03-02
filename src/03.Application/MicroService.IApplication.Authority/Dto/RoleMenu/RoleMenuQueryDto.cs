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
	/// RoleMenu --dto
	/// </summary>
    [ProtoContract]
    [Serializable]
	public class RoleMenuQueryDto:BaseDto
	{

		//<summary>
		// 角色Id
		//<summary>
		public string RoleId { set; get; }

		//<summary>
		// 菜单Id
		//<summary>
		public string MenuId { set; get; }
 
	}
}