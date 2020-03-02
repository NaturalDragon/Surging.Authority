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
	/// RelationOrganizationEmployee --dto
	/// </summary>
    [ProtoContract]
    [Serializable]
	public class RelationOrganizationEmployeeQueryDto:BaseDto
	{

		//<summary>
		// 
		//<summary>
		public string OrganizationId { set; get; }

		//<summary>
		// 
		//<summary>
		public string EmployeeId { set; get; }


        public string OrganizationNames { set; get; }


    }
}