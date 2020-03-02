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
	/// OrganizationMenu --dto
	/// </summary>
    [ProtoContract]
    [Serializable]
	public class OrganizationMenuQueryDto:BaseDto
	{

		//<summary>
		// 
		//<summary>
		public string OrganizationId { set; get; }

		//<summary>
		// 
		//<summary>
		public string MenuId { set; get; }
 
	}
}