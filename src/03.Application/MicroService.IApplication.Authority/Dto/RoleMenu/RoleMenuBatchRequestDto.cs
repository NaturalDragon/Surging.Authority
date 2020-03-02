
using MicroService.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;


 namespace MicroService.IApplication.Authority.Dto
 {
	/// <summary>
	/// RoleMenu --dto
	/// </summary>
    [Serializable]
	public class RoleMenuBatchRequestDto:LoginUser
	{
	      public List<RoleMenuRequestDto> RoleMenuRequestList { set; get; }
	}
}