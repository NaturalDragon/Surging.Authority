using System;
using System.Collections.Generic;
using System.Text;

namespace MicroService.IApplication.Authority.Dto
{
 public   class RoleTypeViewResponse
    {
        public RoleTypeViewResponse()
        {
            RoleList = new List<RoleQueryDto>();
        }
        public int Id { set; get; }


        public string Name { set; get; }


        public IList<RoleQueryDto> RoleList { set; get; }
    }
}
