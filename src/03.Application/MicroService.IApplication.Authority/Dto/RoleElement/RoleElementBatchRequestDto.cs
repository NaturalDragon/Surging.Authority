
using MicroService.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;


 namespace MicroService.IApplication.Authority.Dto
 {
	/// <summary>
	/// RoleElement --dto
	/// </summary>
    [Serializable]
	public class RoleElementBatchRequestDto:LoginUser
	{
	      public List<RoleElementRequestDto> RoleElementRequestList { set; get; }
	}
}