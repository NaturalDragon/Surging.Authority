using MicroService.Core;
using MicroService.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

 namespace MicroService.Entity.Authority
 {
	/// <summary>
	/// Organization --实体
	/// </summary>
	public class Organization:Entity<string>
	{
  		//<summary>
		// 名称
		//<summary>
		[StringLength(255)]
		[Required]
		public string Name { set; get; }
		//<summary>
		// 父级Id
		//<summary>
		[StringLength(36)]
		[Required]
		public string ParentId { set; get; }
		//<summary>
		// 次序值
		//<summary>
		[StringLength(10)]
		[Required]
		public string Order { set; get; }
		//<summary>
		// 部门Id
		//<summary>
		[Required]
		public long DepartmentId { set; get; }
		//<summary>
		// 是否创建企业群
		//<summary>
			public bool? CreateDeptGroup { set; get; }
		//<summary>
		// 自动增加用户
		//<summary>
			public bool? AutoAddUser { set; get; }
		//<summary>
		// 部门的主管列表
		//<summary>
		[StringLength(255)]
			public string DeptManagerUserIdList { set; get; }
		//<summary>
		// 是否隐藏部门
		//<summary>
			public bool? DeptHiding { set; get; }
		//<summary>
		// 部门查看权限列表
		//<summary>
		[StringLength(255)]
			public string DeptPerimits { set; get; }
		//<summary>
		// 职员查看权限列表
		//<summary>
		[StringLength(255)]
			public string UserPerimits { set; get; }
		//<summary>
		// 是否仅内部员工自己可见
		//<summary>
			public bool? OuterDept { set; get; }
		//<summary>
		// 额外配置部门查看权限列表
		//<summary>
		[StringLength(255)]
			public string OuterPermitDepts { set; get; }
		//<summary>
		// 额外配置外部员工查看权限列表
		//<summary>
		[StringLength(255)]
			public string OuterPerimitUsers { set; get; }
		//<summary>
		// 企业群群主
		//<summary>
		[StringLength(255)]
			public string OrgDeptOwner { set; get; }
 
	}
}