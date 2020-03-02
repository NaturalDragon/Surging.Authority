
using MicroService.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;


 namespace MicroService.IApplication.Authority.Dto
 {
	/// <summary>
	/// EmployeeMenu --dto
	/// </summary>
    [Serializable]
	public class EmployeeMenuBatchRequestDto:LoginUser
	{
	      public List<EmployeeMenuRequestDto> EmployeeMenuRequestList { set; get; }
	}
}