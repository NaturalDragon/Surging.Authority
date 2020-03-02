
using MicroService.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;


 namespace MicroService.IApplication.Authority.Dto
 {
	/// <summary>
	/// Menu --dto
	/// </summary>
    [Serializable]
	public class MenuBatchRequestDto:LoginUser
	{
	      public List<MenuRequestDto> MenuRequestList { set; get; }
	}
}