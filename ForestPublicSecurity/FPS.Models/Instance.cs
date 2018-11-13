using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPS.Models
{
    /// <summary>
    /// 案件表
    /// </summary>
    public class Instance
    {
        /// <summary>
        /// 案件ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 报警ID
        /// </summary>
        public int AlterID { get; set; }
        /// <summary>
        /// 立案人ID
        /// </summary>
        public int RegisterPeopleID { get; set; }
        /// <summary>
        /// 立案类型
        /// </summary>
        public int InstanceTypes { get; set; }
        /// <summary>
        /// 审批状态
        /// </summary>
        public int ApproveState { get; set; }
        /// <summary>
        /// 案情状态
        /// </summary>
        public int InstanceState { get; set; }
        /// <summary>
        /// 立案时间
        /// </summary>
        public DateTime Time { get; set; }
        /// <summary>
        /// 备用字段
        /// </summary>
        public string Space { get; set; }



    }
}
