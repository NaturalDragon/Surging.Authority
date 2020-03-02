
using MicroService.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;


 namespace MicroService.IApplication.Authority.Dto
 {
	/// <summary>
	/// Module --dto
	/// </summary>
    [Serializable]
	public class ModuleBatchRequestDto:LoginUser
	{
	      public List<ModuleRequestDto> ModuleRequestList { set; get; }
	}
}