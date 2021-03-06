﻿using System;
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
    public class StudentServices : IStudent
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

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public UserAndRole Login(string name, string pwd)
        {
            var db = SugerBase.GetInstance();
            var userlist = db.SqlQueryable<UserAndRole>("select a.*,c.rolename,c.ID as RID,c.state as RState from users a,userrole b,role c where a.id=b.userid and b.roleid=c.id").Where(m => (m.LoginName == name && m.Password == pwd)).Single();
            return userlist;
        }

        /// <summary>
        /// 权限显示
        /// </summary>
        /// <returns></returns>
        public List<Authority> GetAuthority(int id)
        {

            var db = SugerBase.GetInstance();
            var userlist = db.SqlQueryable<Authority>("select distinct d.Name,Url,FatherID,d.ID,OrderID from Users a join UserRole b on a.ID = b.UserID" +
                             " join RoleAuthority c on b.RoleID = c.RoleID " +
                             " join Authority d on d.ID = c.AuthorityId where a.ID = "+id).ToList();
            return userlist;
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
