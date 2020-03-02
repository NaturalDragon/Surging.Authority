using MicroService.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MicroService.Data.Common
{
   public class EntityRequest:LoginUser
    {
        public EntityRequest()
        {
            Ids = new List<string>();
        }
        public IList<string> Ids { set; get; }
    }
}
