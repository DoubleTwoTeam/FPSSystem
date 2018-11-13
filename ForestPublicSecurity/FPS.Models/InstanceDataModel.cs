using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPS.Models
{
    /// <summary>
    /// 审批案件详情数据Model
    /// </summary>
    public class InstanceDataModel
    {
        public int ID { get; set; }
        /// <summary>
        /// 报警ID
        /// </summary>
        public int AlterID { get; set; }
        /// <summary>
        /// 报警事由
        /// </summary>
        public string AlarmReason { get; set; }

        /// <summary>
        /// 案情地点
        /// </summary>
        public string DetailSplace { get; set; }
        /// <summary>
        /// 立案人ID
        /// </summary>
        public int RegisterPeopleID { get; set; }
        /// <summary>
        /// 立案人姓名
        /// </summary>
        public string RealName { get; set; }
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
        public DateTime InstanceTime { get; set; }

        /// <summary>
        /// 案件备用字段
        /// </summary>
        public string Space { get; set; }
        
        
    }
}
