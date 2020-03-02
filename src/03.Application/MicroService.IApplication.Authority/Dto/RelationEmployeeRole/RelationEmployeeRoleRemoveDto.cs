using MicroService.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MicroService.IApplication.Authority.Dto
{
   public class RelationEmployeeRoleRemoveDto:LoginUser
    {
        public RelationEmployeeRoleRemoveDto()
        {
            RoleIds = new List<string>();
        }
        public string EmployeeId { set; get; }

        public IList<string> RoleIds { set; get; }
    }
}
