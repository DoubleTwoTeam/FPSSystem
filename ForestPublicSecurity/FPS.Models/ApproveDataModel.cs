using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPS.Models
{
    public class ApproveDataModel
    {
        /// <summary>
        /// 审批表ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 意见
        /// </summary>
        public string Ideas { get; set; }

        /// <summary>
        /// 审批状态
        /// </summary>
        public int ApproveState { get; set; }

        /// <summary>
        /// 后置ID4
        /// </summary>
        public int PlaceID { get; set; }

        /// <summary>
        /// 案件ID
        /// </summary>
        public int InstanceID { get; set; }

        /// <summary>
        /// 案件类型ID
        /// </summary>
        public int BusinessID { get; set; }

        /// <summary>
        /// 业务名称
        /// </summary>
        public string BusinessName { get; set; }
        
        /// <summary>
        /// 角色ID
        /// </summary>
        public int RoleID { get; set; }
        /// <summary>
        /// 角色名字
        /// </summary>
        public string RoleName { get; set; }
        /// <summary>
        /// 立案状态
        /// </summary>
        public int InstanceTypes { get; set; }
        /// <summary>
        /// 立案时间
        /// </summary>
        public DateTime InstanceTime { get; set; }
        /// <summary>
        /// 案情状态
        /// </summary>
        public int InstanceState { get; set; }
    }
}
