using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPS.Models.DTO
{
    public class PageParams
    {
        /// <summary>
        /// 表名或者视图名
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 显示的字段
        /// </summary>
        public string Fields { get; set; }

        /// <summary>
        /// 过滤条件
        /// </summary>
        public string Filter { get; set; }

        /// <summary>
        /// 页的大小
        /// </summary>
        public int PageSize { get; set; } = 10;

        /// <summary>
        /// 当前页面
        /// </summary>
        public int Page { get; set; } = 0;

        /// <summary>
        /// 排序字段
        /// </summary>
        public string Sort { get; set; }
        public string Orderby { get; set; } = "id desc";

        /// <summary>
        /// 当前页数
        /// </summary>
        public int CurPage { get; set; }
    }
}
