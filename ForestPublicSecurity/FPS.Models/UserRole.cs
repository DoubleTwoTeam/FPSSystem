using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPS.Models
{
    /// <summary>
    /// 用户角色关联表
    /// </summary>
  public class UserRole
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 名字
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int RoleId { get; set; }
    }
}
