
using MicroService.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;


 namespace MicroService.IApplication.Authority.Dto
 {
	/// <summary>
	/// OrganizationMenu --dto
	/// </summary>
    [Serializable]
	public class OrganizationMenuBatchRequestDto:LoginUser
	{
	      public List<OrganizationMenuRequestDto> OrganizationMenuRequestList { set; get; }
	}
}