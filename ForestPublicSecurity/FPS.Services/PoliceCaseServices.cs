using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FPS.Models;
using FPS.IServices;

namespace FPS.Services
{
    public class PoliceCaseServices:IPoliceCase
    {
        /// <summary>
        /// 添加案件
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public int InsertInstance(Instance instance)
        {
            var db = SugerBase.GetInstance();

            int result = db.Insertable(instance).ExecuteCommand();

            Approve approve=new Approve() { OriginalId=instance.ID,}
            return result;
        }
    }
}
