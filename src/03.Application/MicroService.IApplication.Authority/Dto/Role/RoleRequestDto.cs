
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
	/// Role --dto
	/// </summary>
    [ProtoContract]
    [Serializable]
	public class RoleRequestDto:LoginUser
	{

		//<summary>
		// 名称
		//<summary>
		public string Name { set; get; }

		//<summary>
		// 公司Id
		//<summary>
		public string CompanyId { set; get; }

		//<summary>
		// 职责类型 取值对应RoleType 0:职员职责(针对职员) 1:用户职责(针对管理员) 
		//<summary>
		public int RoleType { set; get; }
 
	}
}