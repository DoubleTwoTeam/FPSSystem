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
<<<<<<< HEAD
        public List<T> ListData { get; set; }
        public int TotalCount { get; set; }
=======
        public List<T> listData { get; set; }

        public int totalCount { get; set; }
>>>>>>> b57e63695203ad7e6896a609262b9525b1435ec0
    }
}
