using System;
using System.Collections.Generic;
using System.Text;

namespace MicroService.IApplication.Authority.Dto
{
    /// <summary>
    /// 菜单树
    /// </summary>
    public class MenuTreeViewResponse
    {
        public string key { set; get; }
        public string title { set; get; }

        public string value { set; get; }

        public string path { set; get; }

        public int sortIndex { set; get; }
        public List<MenuTreeViewResponse> children { set; get; }
    }
}
