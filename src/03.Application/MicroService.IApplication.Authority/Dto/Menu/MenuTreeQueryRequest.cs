using MicroService.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MicroService.IApplication.Authority.Dto
{
    public class MenuTreeQueryRequest: PageData
    {
        public string ModuleId { set; get; }

        public string MenuId { set; get; }

        public string MenuName { set; get; }
    }
}
