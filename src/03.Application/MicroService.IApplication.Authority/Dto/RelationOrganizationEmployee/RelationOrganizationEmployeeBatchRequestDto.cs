
using MicroService.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;


 namespace MicroService.IApplication.Authority.Dto
 {
	/// <summary>
	/// RelationOrganizationEmployee --dto
	/// </summary>
    [Serializable]
	public class RelationOrganizationEmployeeBatchRequestDto:LoginUser
	{
	      public List<RelationOrganizationEmployeeRequestDto> RelationOrganizationEmployeeRequestList { set; get; }
	}
}