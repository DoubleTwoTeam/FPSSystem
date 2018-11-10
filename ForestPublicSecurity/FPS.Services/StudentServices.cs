using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FPS.Models;
using FPS.IServices;
using FPS.Models.DTO;
using SQLiteSugar;
using SqlSugar;

namespace FPS.Services
{
    public class StudentServices:IStudent
    {
        public string GetStudentName()
        {
            var db = SugerBase.GetInstance();
            var name = db.Queryable<Student>().Single(m => m.ID == 1).Name;
            return name;
        }

        /// <summary>
        /// 添加报警
        /// </summary>
        /// <param name="alarm"></param>
        /// <returns></returns>
        public int AddCallPolice(Alarm alarm)
        {
            var db = SugerBase.GetInstance();
            var result = db.Insertable<Alarm>(alarm);
            return result.ExecuteCommand();
        }

        //public List<Student> Fen()
        //{
        //    var db = SugerBase.GetInstance();
        //    PageParams parms = new PageParams();
        //    parms.PageIndex = 1;
        //    parms.PageSize = 1;
        //    parms.StrWhere = "";
        //    parms.TableName = "student";

        //    var result1 = PageCommon.PagingCommon<Student>(parms);

        //    var result0=db.Ado.SqlQuery<Student>("pager", parms);
        //    var result=db.Ado.SqlQuery<Student>("pager", parms).ToList();
        //    return result;

        //}
    }
}
