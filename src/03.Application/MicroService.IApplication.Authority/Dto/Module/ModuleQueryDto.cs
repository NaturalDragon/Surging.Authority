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
	/// Module --dto
	/// </summary>
    [ProtoContract]
    [Serializable]
	public class ModuleQueryDto:BaseDto
	{
        public ModuleQueryDto()
        {
            ModuleElementActionRequests = new List<ModuleElementQueryDto>();
        }

        public IList<ModuleElementQueryDto> ModuleElementActionRequests { set; get; }
        //<summary>
        // 名称
        //<summary>
        public string Name { set; get; }

        //<summary>
        // 路由路径
        //<summary>
        public string Url { set; get; }
        //<summary>
        // 启用
        //<summary>
        public bool IsEnabled { set; get; }

    }
}