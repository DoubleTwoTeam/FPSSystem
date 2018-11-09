using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPS.Models.DTO
{
    /// <summary>
    /// 返回的数据集
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PageList<T>
    {
        /// <summary>
        /// 返回的List集合
        /// </summary>
        public List<T> ListData { get; set; }

        /// <summary>
        /// 返回总条数
        /// </summary>
        public int TotalCount { get; set; }
    }
}
