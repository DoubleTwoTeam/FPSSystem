using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FPS.Models;

namespace FPS.IServices
{
    public interface IPoliceCase
    {
        /// <summary>
        /// 添加案件
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        int InsertInstance(Instance instance);
    }
}
