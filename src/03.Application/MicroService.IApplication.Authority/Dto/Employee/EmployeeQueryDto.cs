using MicroService.Core;
using MicroService.Data.Enums;
using MicroService.Common.Models;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

 namespace MicroService.IApplication.Authority.Dto
 {
	/// <summary>
	/// Employee --dto
	/// </summary>
    [ProtoContract]
    [Serializable]
	public class EmployeeQueryDto:BaseDto
	{


        public EmployeeQueryDto()
        {
            OrganizationObjects = new List<KeyValueJsonObject>();
            RoleObjects = new List<KeyValueJsonObject>();
            OrganizationIds = new List<string>();
            RoleIds = new List<string>();
        }
        //<summary>
        // 如果职员是通过第三方系统导入的，这个字段记录第三方系统中的用户唯一标识
        //<summary>
        public string UserId { set; get; }

		//<summary>
		// 姓名
		//<summary>
		public string Name { set; get; }

		//<summary>
		// 密码
		//<summary>
		public string Password { set; get; }

		//<summary>
		// 部门
		//<summary>
		public string Department { set; get; }

		//<summary>
		// 部门内的排序值
		//<summary>
		public string Order { set; get; }

		//<summary>
		// 职位
		//<summary>
		public string Position { set; get; }

		//<summary>
		// 手机
		//<summary>
		public string Mobile { set; get; }

		//<summary>
		// 电话
		//<summary>
		public string Telephone { set; get; }

		//<summary>
		// 性别
		//<summary>
		public int Gender { set; get; }

		//<summary>
		// 邮箱
		//<summary>
		public string Email { set; get; }

		//<summary>
		// 办公地点
		//<summary>
		public string WorkPlace { set; get; }

		//<summary>
		// 英文名
		//<summary>
		public string EnglishName { set; get; }

		//<summary>
		// 通讯录语言(默认zh_CN另外支持en_US)
		//<summary>
		public string Lang { set; get; }

		//<summary>
		// 员工工号，对应显示到OA后台和客户端个人资料的工号栏目。长度为0~64个字符
		//<summary>
		public string Jobnumber { set; get; }

		//<summary>
		// 盐
		//<summary>
		public string Salt { set; get; }

		//<summary>
		// 是否隐藏
		//<summary>
		public bool? IsHide { set; get; }

		//<summary>
		// 是否高级
		//<summary>
		public bool? IsSenior { set; get; }

		//<summary>
		// 上级字段
		//<summary>
		public bool? IsLeader { set; get; }

		//<summary>
		// 扩展属性
		//<summary>
		public string Extra { set; get; }

		//<summary>
		// 错误次数
		//<summary>
		public int? ErrorCount { set; get; }

		//<summary>
		// 错误时间
		//<summary>
		public DateTime? ErrotTime { set; get; }

		//<summary>
		// 是否锁定
		//<summary>
		public bool? IsLock { set; get; }

		//<summary>
		// 头像
		//<summary>
		public string Avatar { set; get; }

		//<summary>
		// 最后一次登录的职责id
		//<summary>
		public string LastLoginRoleId { set; get; }


       

        public IList<KeyValueJsonObject> OrganizationObjects { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        public IList<KeyValueJsonObject> RoleObjects { set; get; }

        /// <summary>
        /// 组织Id
        /// </summary>
        public IList<string> OrganizationIds { get; set; }


        /// <summary>
        /// 角色Id
        /// </summary>
        public IList<string> RoleIds { set; get; }
        public string RoleName { get; set; }
    }
   
}