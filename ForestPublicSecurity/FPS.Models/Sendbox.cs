using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPS.Models
{
    /// <summary>
    /// 收件箱
    /// </summary>
    public class Sendbox
    {
        public int ID { get; set; }

        /// <summary>
        /// 发件人ID
        /// </summary>
        public int SendPeople{ get; set; }

        /// <summary>
        /// 收件人ID
        /// </summary>
        public int ReceiveID{ get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 附件
        /// </summary>
        public string Enclosure{ get; set; }

        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        /// 查看时间
        /// </summary>
        public DateTime LookTime { get; set; }

        /// <summary>
        /// 抄送人IDs
        /// </summary>
        public int IDs { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int State { get; set; }

        /// <summary>
        /// 备用
        /// </summary>
        public string Space { get; set; }
    }
}
