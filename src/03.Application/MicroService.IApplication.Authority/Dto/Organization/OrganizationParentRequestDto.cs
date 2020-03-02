using MicroService.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MicroService.IApplication.Authority.Dto
{
    public class OrganizationParentRequestDto : PageData
    {
        /// <summary>
        /// 
        /// </summary>
        public string ParentId { get; set; }
        //1 所有，0指获取部门
        public int Type { get; set; }
        public string Name { get; set; }

        public IList<string> OrganizationIds { get; set; }


    }
}
