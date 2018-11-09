using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FPS.Models;
using FPS.IServices;
using Newtonsoft.Json;

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

        /// <summary>
        /// 添加案件时添加审批表并返回
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public Approve GetApprove(Instance instance)
        {
            var db = SugerBase.GetInstance();
            ApproveCourse approveCourse= db.Queryable<ApproveCourse>().Where(m => (m.Condition.Contains(instance.InstanceState.ToString()) && m.BusinesstypeId == Convert.ToInt32(instance.InstanceTypes))).Single();
            Approve approve = new Approve() { RoleId = approveCourse.ApproveRoleId, BusinesstypeId = approveCourse.BusinesstypeId, PlaceID = approveCourse.PostpositionID, OriginalId=instance.ID, Ideas="", State="0" };
            return approve;
        }

        /// <summary>
        /// 案件显示页面
        /// </summary>
        /// <returns></returns>
        public List<InstanceDataModel> GetInstanceList()
        {
            var db = SugerBase.GetInstance();
            List<InstanceDataModel> inStance =db.SqlQueryable<InstanceDataModel>(
                "select Instance.ID,Alarm.AlarmReason,Alarm.DetailSplace,Users.RealName,Instance.InstanceTypes,Instance.ApproveState,Instance.InstanceState,Instance.Time as InstanceTime " +
                "from Instance,Alarm,Users" +
                " where Instance.AlterID=Alarm.ID and Instance.RegisterPeopleID=Users.ID "
                ).ToList();
            return inStance;
        }

        /// <summary>
        /// 根据Id查询对应的案件
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Instance GetInstanceById(int id)
        {
            using (var db = SugerBase.GetInstance())
            {

                Instance instance = db.Queryable<Instance>().Single(m => m.ID == id);

                return instance;
            }
        }

        /// <summary>
        /// 审核最后对案件状态进行更改
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public int UpdateinStance(Instance instance)
        {
            using (var db = SugerBase.GetInstance())
            {
                int result= db.Updateable<Instance>(instance).ExecuteCommand();
                return result;
            }
        }



    }
}
