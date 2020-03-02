
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
	/// RoleElement --dto
	/// </summary>
    [ProtoContract]
    [Serializable]
	public class RoleElementRequestDto:LoginUser
	{
        public RoleElementRequestDto()
        {
            ElementIds = new List<string>();
        }

        //<summary>
        // 元素Id
        //<summary>
        public string ElementId { set; get; }

        public IList<string> ElementIds { set; get; }


    }
}