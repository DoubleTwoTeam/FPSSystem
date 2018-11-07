using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FPS.Models
{
    /// <summary>
    /// 报警表
    /// </summary>
    public class Alarm
    {
        public int ID { get; set; }

        /// <summary>
        /// 报警事由
        /// </summary>
        public string AlarmReason { get; set; }

        /// <summary>
        /// 案情地点
        /// </summary>
        public string DetailSplace { get; set; }

        /// <summary>
        /// 附件
        /// </summary>
        public string Enclosure { get; set; }

        /// <summary>
        /// 报警时间
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        /// 报警人
        /// </summary>
        public string AlarmPeople { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 身份证
        /// </summary>
        public string IdCard { get; set; }

        /// <summary>
        /// 报警人标识
        /// </summary>
        public int Url { get; set; }

        /// <summary>
        /// 处理人
        /// </summary>
        public int SolvePeopleId { get; set; }

        /// <summary>
        /// 外派警员
        /// </summary>
        public int OutID { get; set; }

        /// <summary>
        /// 处理时间
        /// </summary>
        public DateTime OverTime { get; set; }


        /// <summary>
        /// 审批状态
        /// </summary>

        public int State { get; set; }

        /// <summary>
        /// 备用
        /// </summary>
        public string Stace { get; set; }
    }
}
