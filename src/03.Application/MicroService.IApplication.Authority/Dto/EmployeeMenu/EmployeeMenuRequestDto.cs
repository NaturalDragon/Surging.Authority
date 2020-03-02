
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
	/// EmployeeMenu --dto
	/// </summary>
    [ProtoContract]
    [Serializable]
	public class EmployeeMenuRequestDto:LoginUser
	{
        public EmployeeMenuRequestDto()
        {
            MenuIds = new List<string>();
        }
        //<summary>
        // 员工Id
        //<summary>
        public string EmployeeId { set; get; }

		//<summary>
		// 菜单Id
		//<summary>
		public string MenuId { set; get; }
        public IList<string> MenuIds { get; set; }
    }
}