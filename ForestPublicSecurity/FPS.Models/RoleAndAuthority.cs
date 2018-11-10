using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPS.Models
{
   public class RoleAndAuthority
    {
        //权限表字段

        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 权限名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 父级ID
        /// </summary>
        public int FatherId { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int State { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 排序ID
        /// </summary>
        public int OrderId { get; set; }

        //角色表字段

        /// <summary>
        /// ID
        /// </summary>
        public int RID { get; set; }

        /// <summary>
        /// 名字
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int RState { get; set; }
    }
}
