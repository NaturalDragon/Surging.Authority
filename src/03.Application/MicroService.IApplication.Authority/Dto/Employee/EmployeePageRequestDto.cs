

using MicroService.Core;
using MicroService.Data.Common;
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
	public class EmployeePageRequestDto:PageData
	{
        public EmployeePageRequestDto()
        {
            Ids = new List<string>();
        }
        public string Id { get; set; }
        public string Mobile { get; set; }
        public string Jobnumber { get; set; }
        public string OrganizationId { get; set; }
        public string Usre { get; set; }
        public string PassWord { get; set; }

        public long DeptId { set; get; }



        public List<string> UserIds { set; get; }
        public IList<string> OrganziationIds { get; set; }

        public IList<string> Ids { get; set; }

        /// <summary>
        /// ÊÇ·ñ¹Ø¼ü×Ö²éÑ¯
        /// </summary>
        public bool IsKeySearch { set; get; }
    }
}