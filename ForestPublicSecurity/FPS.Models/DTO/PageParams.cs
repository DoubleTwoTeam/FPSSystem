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
        /// 页的大小
        /// </summary>
        public int PageSize { get; set; } = 10;

        /// <summary>
        /// 当前页面
        /// </summary>
        public int Page { get; set; } = 0;

        /// <summary>
        /// 按照字段排序
        /// </summary>
        public string Orderby { get; set; } = "id desc";

        /// <summary>
        /// 搜索条件
        /// </summary>
        public string StrWhere { get; set; }

        /// <summary>
        /// 共有多少页
        /// </summary>
        public  int  PageCount { get; set; }
    }
}
