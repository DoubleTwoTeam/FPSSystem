using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FPS.IServices;
using FPS.Models;
using FPS.Models.DTO;
using Newtonsoft.Json;

namespace FPS.Services
{
    public class AlarmServices : IAlarm
    {
        /// <summary>
        /// 外派警员列表
        /// </summary>
        /// <returns></returns>
        public List<Role> GetRoles()
        {
            var db = SugerBase.GetInstance();
            var roleList = db.SqlQueryable<Role>("Select a.RoleId as ID,b.RoleName from RoleAuthority a, ROLE b Where a.RoleId=b.ID and a.AuthorityId=88").ToList();
            return roleList;
        }

        /// <summary>
        /// 添加报警
        /// </summary>
        /// <param name="alarm"></param>
        /// <returns></returns>
        public int InsertAlarm(Alarm alarm)
        {
            var db = SugerBase.GetInstance();
            var insertObj = new Alarm()
            {
                AlarmReason = alarm.AlarmReason,
                DetailSplace = alarm.DetailSplace,
                Enclosure = alarm.Enclosure,
                Time = alarm.Time,
                AlarmPeople = alarm.AlarmPeople,
                Phone = alarm.Phone,
                IdCard = alarm.IdCard,
                Url = alarm.Url
            };

            int ruselt = db.Insertable(insertObj).ExecuteCommand();
            return ruselt;
        }


        /// <summary>
        /// 操作报警信息
        /// </summary>
        /// <param name="alarm"></param>
        /// <returns></returns>
        public int UptAlarm(int id, Alarm alarm)
        {
            var db = SimpleClientBase.GetSimpleClient<Alarm>();
            var result = db.Update(m => new Alarm {  OutID = alarm.ID,SolvePeopleId=alarm.SolvePeopleId,OverTime=alarm.OverTime,State=1 }, q => q.ID == id) ? 1 : 0;
            return result;
        }

        /// <summary>
        /// 警员归队
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int UptState(int id)
        {
            var db = SimpleClientBase.GetSimpleClient<Alarm>();
            var result = db.Update(m =>new Alarm { State=2 },q=>q.ID==id)?1:0;
            return result;
        }
    }
}
