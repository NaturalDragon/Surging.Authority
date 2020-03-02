
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
	/// RelationOrganizationEmployee -仓储
	/// </summary>
     public class RelationOrganizationEmployeeRespository : RespositoryBase<RelationOrganizationEmployee>, IRelationOrganizationEmployeeRespository
    {
        public RelationOrganizationEmployeeRespository()
        {
        }
    }
}