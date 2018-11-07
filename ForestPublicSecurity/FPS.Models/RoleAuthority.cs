using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPS.Models
{
    /// <summary>
    /// 角色权限关联
    /// </summary>
   public class RoleAuthority
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 角色ID
        /// </summary>
        public int RoleId { get; set; }
        /// <summary>
        /// 权限ID
        /// </summary>
        public int AuthorityId { get; set; }
    }
}
