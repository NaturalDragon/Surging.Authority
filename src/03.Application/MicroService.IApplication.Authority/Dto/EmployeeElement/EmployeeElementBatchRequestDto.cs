
using MicroService.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;


 namespace MicroService.IApplication.Authority.Dto
 {
	/// <summary>
	/// EmployeeElement --dto
	/// </summary>
    [Serializable]
	public class EmployeeElementBatchRequestDto:LoginUser
	{
	      public List<EmployeeElementRequestDto> EmployeeElementRequestList { set; get; }
	}
}