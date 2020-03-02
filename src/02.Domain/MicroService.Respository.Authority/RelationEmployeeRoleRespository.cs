
using MicroService.Core;
using System;
using System.Collections.Generic;
using System.Text;
using MicroService.Entity.Authority;
using MicroService.IRespository.Authority;
using MicroService.EntityFramwork;
using MicroService.Data;
 namespace MicroService.Respository.Authority
 {
	/// <summary>
	/// RelationEmployeeRole -仓储
	/// </summary>
     public class RelationEmployeeRoleRespository : RespositoryBase<RelationEmployeeRole>, IRelationEmployeeRoleRespository
    {
        public RelationEmployeeRoleRespository()
        {
        }
    }
}