
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
	/// RelationEmployeeRole --dto
	/// </summary>
    [ProtoContract]
    [Serializable]
	public class RelationEmployeeRoleRequestDto:LoginUser
	{

		//<summary>
		// 
		//<summary>
		public string RoleId { set; get; }

		//<summary>
		// 成员Id
		//<summary>
		public string EmployeeId { set; get; }
 
	}
}