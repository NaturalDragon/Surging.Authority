using MicroService.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MicroService.IApplication.Authority.Dto
{
   public class RelationOrganizationEmployeeRemoveDto:LoginUser
    {

        public RelationOrganizationEmployeeRemoveDto()
        {
            OrganizationIds = new List<string>();
        }
        public string EmployeeId { set; get; }

         public IList<string> OrganizationIds { set; get; }
    }
}
