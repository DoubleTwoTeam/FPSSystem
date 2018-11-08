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

            return result;
        }

        public Approve GetApproveCourse(Instance instance)
        {
            var db = SugerBase.GetInstance();
            ApproveCourse approveCourse= db.Queryable<ApproveCourse>().Where(m => (m.Condition.Contains(instance.InstanceState.ToString()) && m.BusinesstypeId == Convert.ToInt32(instance.InstanceTypes))).Single();
            Approve approve = new Approve() { RoleId = approveCourse.ApproveRoleId, BusinesstypeId = approveCourse.BusinesstypeId, PlaceID = approveCourse.PostpositionID };
            return approve;
        }
        
    }
}
