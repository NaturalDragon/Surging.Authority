
using MicroService.Core;
using MicroService.Data;
using System;
using System.Collections.Generic;
using System.Text;
using MicroService.Entity.Authority;
 namespace MicroService.IRespository.Authority
 {
	/// <summary>
	/// Employee -仓储接口
	/// </summary>
	public interface IEmployeeRespository:IRespositoryBase<Employee>, IDependency
	{
  
        
	}
}