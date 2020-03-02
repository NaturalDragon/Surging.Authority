
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
	/// EmployeeElement --dto
	/// </summary>
    [ProtoContract]
    [Serializable]
	public class EmployeeElementRequestDto:LoginUser
	{

        public EmployeeElementRequestDto() 
        {
            ElementIds = new List<string>();
        }

		//<summary>
		// 员工Id
		//<summary>
		public string EmployeeId { set; get; }

		//<summary>
		// 元素Id
		//<summary>
		public string ElementId { set; get; }
        public IList<string> ElementIds { get; set; }
    }
}