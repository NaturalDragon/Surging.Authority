using System;
using System.Collections.Generic;
using System.Text;

namespace MicroService.IApplication.Authority.Dto
{
    /// <summary>
    /// 权限选择--返回实体
    /// </summary>
    public class PowerSelectViewResponse
    {
        /// <summary>
        /// 选中资源id
        /// </summary>
        public string Id { set; get; }
        /// <summary>
        /// 是否禁用
        /// </summary>

        public bool Disabled { set; get; } = false;
    }
}
