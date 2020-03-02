using MicroService.Core;
using MicroService.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

 namespace MicroService.Entity.Authority
 {
	/// <summary>
	/// Employee --实体
	/// </summary>
	public class Employee:Entity<string>
	{
  		//<summary>
		// 如果职员是通过第三方系统导入的，这个字段记录第三方系统中的用户唯一标识
		//<summary>
		[StringLength(255)]
		[Required]
		public string UserId { set; get; }
		//<summary>
		// 姓名
		//<summary>
		[StringLength(100)]
		[Required]
		public string Name { set; get; }
		//<summary>
		// 密码
		//<summary>
		[StringLength(255)]
		[Required]
		public string Password { set; get; }
		//<summary>
		// 部门
		//<summary>
		[StringLength(200)]
		[Required]
		public string Department { set; get; }
		//<summary>
		// 部门内的排序值
		//<summary>
		[StringLength(200)]
		[Required]
		public string Order { set; get; }
		//<summary>
		// 职位
		//<summary>
		[StringLength(100)]
			public string Position { set; get; }
		//<summary>
		// 手机
		//<summary>
		[StringLength(64)]
		[Required]
		public string Mobile { set; get; }
		//<summary>
		// 电话
		//<summary>
		[StringLength(64)]
			public string Telephone { set; get; }
		//<summary>
		// 性别
		//<summary>
		[Required]
		public int Gender { set; get; }
		//<summary>
		// 邮箱
		//<summary>
		[StringLength(64)]
			public string Email { set; get; }
		//<summary>
		// 办公地点
		//<summary>
		[StringLength(255)]
			public string WorkPlace { set; get; }
		//<summary>
		// 英文名
		//<summary>
		[StringLength(64)]
			public string EnglishName { set; get; }
		//<summary>
		// 通讯录语言(默认zh_CN另外支持en_US)
		//<summary>
		[StringLength(64)]
			public string Lang { set; get; }
		//<summary>
		// 员工工号，对应显示到OA后台和客户端个人资料的工号栏目。长度为0~64个字符
		//<summary>
		[StringLength(64)]
			public string Jobnumber { set; get; }
		//<summary>
		// 盐
		//<summary>
		[StringLength(32)]
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
		[StringLength(255)]
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
		[StringLength(255)]
			public string Avatar { set; get; }
		//<summary>
		// 最后一次登录的职责id
		//<summary>
		[StringLength(36)]
			public string LastLoginRoleId { set; get; }
 
	}
}