using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MicroService.Common.Core.Enums
{
    public enum RoleType
    {
        /// <summary>
        /// 管理员职责
        /// </summary>
        [Description("管理员职责")]
        AdministratorRole = 0,
        /// <summary>
        /// 职员职责
        /// </summary>
        [Description("职员职责")]
        EmployeeRole = 1
    }
}
