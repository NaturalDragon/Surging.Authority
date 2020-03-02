
using MicroService.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;


 namespace MicroService.IApplication.Authority.Dto
 {
	/// <summary>
	/// Employee --dto
	/// </summary>
    [Serializable]
	public class EmployeeBatchRequestDto:LoginUser
	{
	      public List<EmployeeRequestDto> EmployeeRequestList { set; get; }
	}
}