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

        public int PageSize { get; set; } = 10;


        public string Orderby { get; set; } = "id desc";

        /// <summary>
        /// 当前页数
        /// </summary>
        public int CurPage { get; set; }
    }
}
