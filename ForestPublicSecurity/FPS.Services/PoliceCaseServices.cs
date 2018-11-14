using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FPS.Models;
using FPS.IServices;
using Newtonsoft.Json;
using FPS.Models.DTO;

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
            List<ApproveCourse> course = db.SqlQueryable<ApproveCourse>("select * from ApproveCourse where Condition like '%" + instance.InstanceState + "%' and BusinesstypeId=" + instance.InstanceTypes).ToList();
            ApproveCourse approveCourse = course[0];
            //ApproveCourse approveCourse= db.Queryable<ApproveCourse>().Where(m => (m.Condition.Contains(instance.InstanceState.ToString()) && m.BusinesstypeId == Convert.ToInt32(instance.InstanceTypes))).Single();
            Approve approve = new Approve() { RoleId = approveCourse.ApproveRoleId, BusinesstypeId = approveCourse.BusinesstypeId, PlaceID = approveCourse.PostpositionID, OriginalId=instance.ID, Ideas="", State="1" };
            return approve;
        }

        /// <summary>
        /// 案件显示页面
        /// </summary>
        /// <returns></returns>
        public PageList<InstanceDataModel> GetInstanceList()
        {
            var db = SugerBase.GetInstance();
            List<InstanceDataModel> inStance =db.SqlQueryable<InstanceDataModel>(
                "select Instance.ID,Alarm.AlarmReason,Alarm.DetailSplace,Users.RealName,Instance.InstanceTypes,Instance.ApproveState,Instance.InstanceState,Instance.Time as InstanceTime " +
                "from Instance,Alarm,Users" +
                " where Instance.AlterID=Alarm.ID and Instance.RegisterPeopleID=Users.ID "
                ).ToList();
            int count = db.SqlQueryable<InstanceDataModel>(
                "select Instance.ID,Alarm.AlarmReason,Alarm.DetailSplace,Users.RealName,Instance.InstanceTypes,Instance.ApproveState,Instance.InstanceState,Instance.Time as InstanceTime " +
                "from Instance,Alarm,Users" +
                " where Instance.AlterID=Alarm.ID and Instance.RegisterPeopleID=Users.ID "
                ).Count();
            PageList<InstanceDataModel> pageList = new PageList<InstanceDataModel>() { ListData = inStance, TotalCount = count };
            return pageList;
        }

        /// <summary>
        /// 根据Id查询对应的案件
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Instance GetInstanceById(int id)
        {
            var db = SugerBase.GetInstance();
            

            List<Instance> instances = db.SqlQueryable<Instance>("select * from Instance where ID="+id).ToList();
            Instance instance = instances[0];

            return instance;
            
        }

        /// <summary>
        /// 审核最后对案件状态进行更改
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public int UpdateinStance(Instance instance)
        {
            var db = SugerBase.GetInstance();
            
            int result= db.Updateable(instance).Where(m=>m.ID==instance.ID).ExecuteCommand();

             return result;
            
        }

        public List<Business> GetBusinessesList()
        {
            var db = SugerBase.GetInstance();
            List<Business> list= db.Queryable<Business>().ToList();
            return list;
        }


        public int GetinStanceByClass(Instance instance)
        {
            var db = SugerBase.GetInstance();
            List<Instance> instanceList = db.SqlQueryable<Instance>("select * from Instance where AlterID = " + instance.AlterID + " and RegisterPeopleID=" + instance.RegisterPeopleID+ " and InstanceTypes="+instance.InstanceTypes + " and ApproveState="+instance.ApproveState+ " and InstanceState="+instance.InstanceState).ToList();
            Instance instances = instanceList[0];
            int result = instances.ID;
            return result;
        }
    }
}
