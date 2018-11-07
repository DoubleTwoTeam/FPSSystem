using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FPS.IServices;
using FPS.Models;
using SqlSugar;

namespace FPS.Services
{
    public class PoliceCase : IPoliceCase
    {

        /// <summary>
        /// 添加案件
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public int InsertInstance(Instance instance)
        {
            var db = SugerBase.GetInstance();

            int result= db.Insertable(instance).ExecuteCommand();

            return result;
        }
    }
}
