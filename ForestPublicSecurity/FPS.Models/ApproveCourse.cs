using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPS.Models
{
    /// <summary>
    /// 审批流程配置表
    /// </summary>
    public class ApproveCourse
    {
        /// <summary>
        /// 审批流程配置表ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 审批角色ID
        /// </summary>
        public int ApproveRoleId { get; set; }
        /// <summary>
        /// 业务类型ID
        /// </summary>
        public int BusinesstypeId { get; set; }
        /// <summary>
        /// 条件
        /// </summary>
        public string Condition { get; set; }
        /// <summary>
        /// 后置ID
        /// </summary>
        public int PostpositionID { get; set; }
        /// <summary>
        /// 备用ID
        /// </summary>
        public string Space { get; set; }
    }
}
