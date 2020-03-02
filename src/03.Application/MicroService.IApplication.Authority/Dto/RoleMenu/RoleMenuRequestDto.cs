
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
	/// RoleMenu --dto
	/// </summary>
    [ProtoContract]
    [Serializable]
	public class RoleMenuRequestDto:LoginUser
	{
        public RoleMenuRequestDto()
        {
            MenuIds = new List<string>();
        }
        //<summary>
        // 菜单Id
        //<summary>
        public string MenuId { set; get; }
        public IList<string> MenuIds { get; set; }
    }
}