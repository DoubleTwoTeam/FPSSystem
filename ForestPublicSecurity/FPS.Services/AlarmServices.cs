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
        /// 显示功能
        /// </summary>
        /// <returns></returns>
        public PageList<Alarm> PagingCommon<Alarm>(PageParams pageparams)
        {
            var reimbursepagedList = PageCommon.PagingCommon<Alarm>(pageparams);
            return reimbursepagedList;
        }
        /// <summary>
        /// 反填显示
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable GetUptAlarm(int id)
        {
            var db = SugerBase.GetInstance();
            Alarm alarm= db.Queryable<Alarm>().Where(it => it.ID == id).Single();
            DataTable alarmDataTable =JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(alarm));
            return alarmDataTable;
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

        public int UptAlarm(Alarm alarm)
        {
            var db = SugerBase.GetInstance();
            var updateAlarm = new Alarm()
            {
                ID = alarm.ID,
                AlarmReason = alarm.AlarmReason,
                DetailSplace = alarm.DetailSplace,
                Enclosure = alarm.Enclosure,
                Time = alarm.Time,
                AlarmPeople = alarm.AlarmPeople,
                Phone = alarm.Phone,
                IdCard = alarm.IdCard,
                Url = alarm.Url,
                SolvePeopleId=alarm.SolvePeopleId,
                OutID=alarm.OutID,
                OverTime=alarm.OverTime,
                State=alarm.State,
                Space=alarm.Space
            };
            var ruselt = db.Updateable(updateAlarm).ExecuteCommand();
            return ruselt;
        }

        public List<Alarm> GetAlarms()
        {
            var db = SugerBase.GetInstance();
            var getAll = db.Queryable<Alarm>().ToList();
            return getAll;
        }
    }
}
