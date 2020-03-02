
using MicroService.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;


 namespace MicroService.IApplication.Authority.Dto
 {
	/// <summary>
	/// ModuleElement --dto
	/// </summary>
    [Serializable]
	public class ModuleElementBatchRequestDto:LoginUser
	{
	      public List<ModuleElementRequestDto> ModuleElementRequestList { set; get; }
	}
}