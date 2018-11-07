using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPS.Models
{
    /// <summary>
    /// 轮播图
    /// </summary>
    public class Image
    {
        public int ID { get; set; }

        /// <summary>
        /// 名字
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 备用
        /// </summary>
        public string Space { get; set; }
    }
}
