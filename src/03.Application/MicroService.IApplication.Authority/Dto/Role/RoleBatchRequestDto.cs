
using MicroService.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;


 namespace MicroService.IApplication.Authority.Dto
 {
	/// <summary>
	/// Role --dto
	/// </summary>
    [Serializable]
	public class RoleBatchRequestDto:LoginUser
	{
	      public List<RoleRequestDto> RoleRequestList { set; get; }
	}
}