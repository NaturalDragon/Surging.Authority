
using MicroService.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;


 namespace MicroService.IApplication.Authority.Dto
 {
	/// <summary>
	/// Organization --dto
	/// </summary>
    [Serializable]
	public class OrganizationBatchRequestDto:LoginUser
	{
	      public List<OrganizationRequestDto> OrganizationRequestList { set; get; }
	}
}