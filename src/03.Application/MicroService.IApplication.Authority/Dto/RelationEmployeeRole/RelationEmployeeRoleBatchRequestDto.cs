
using MicroService.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;


 namespace MicroService.IApplication.Authority.Dto
 {
	/// <summary>
	/// RelationEmployeeRole --dto
	/// </summary>
    [Serializable]
	public class RelationEmployeeRoleBatchRequestDto:LoginUser
	{
	      public List<RelationEmployeeRoleRequestDto> RelationEmployeeRoleRequestList { set; get; }
	}
}