using System;
using System.Collections.Generic;
using System.Text;

namespace MicroService.IApplication.Authority.Dto
{
    public class AntMenuTree
    {
        public string name { set; get; }

        public string icon { set; get; }
        public string path { set; get; }

        public string component { set; get; }

        public IList<AntMenuTree> children { set; get; }
    }
}
