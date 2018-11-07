using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPS.Models
{
    /// <summary>
    /// 审批表
    /// </summary>
    public class Approve
    {
        /// <summary>
        /// 审批表ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 事件ID
        /// </summary>
        public int OriginalId { get; set; }
        /// <summary>
        /// 业务类型ID
        /// </summary>
        public int BusinesstypeId { get; set; }
        /// <summary>
        /// 审批人ID
        /// </summary>
        public int ApprovePeopleId { get; set; }
        /// <summary>
        /// 角色ID
        /// </summary>
        public int RoleId { get; set; }
        /// <summary>
        /// 意见
        /// </summary>
        public string Ideas { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string State { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        public DateTime Time { get; set; }
        /// <summary>
        /// 后置ID4
        /// </summary>
        public int PlaceID { get; set; }
        /// <summary>
        /// 备用
        /// </summary>
        public string Space { get; set; }
    }
}
