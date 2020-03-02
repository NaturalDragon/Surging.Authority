
using MicroService.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;


 namespace MicroService.IApplication.Authority.Dto
 {
	/// <summary>
	/// OrganizationElement --dto
	/// </summary>
    [Serializable]
	public class OrganizationElementBatchRequestDto:LoginUser
	{
	      public List<OrganizationElementRequestDto> OrganizationElementRequestList { set; get; }
	}
}