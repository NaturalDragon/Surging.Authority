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
	/// OrganizationElement --dto
	/// </summary>
    [ProtoContract]
    [Serializable]
	public class OrganizationElementQueryDto:BaseDto
	{

		//<summary>
		// 机构Id
		//<summary>
		public string OrganizationId { set; get; }

		//<summary>
		// 元素Id
		//<summary>
		public string ElementId { set; get; }
 
	}
}