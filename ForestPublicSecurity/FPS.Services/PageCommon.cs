﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FPS.Models;
using FPS.Models.DTO;
using Oracle.ManagedDataAccess.Client;
using SqlSugar;
using Newtonsoft.Json;

namespace FPS.Services
{
    public class PageCommon
    {
        /// <summary>
        /// 多个结果集
        /// </summary>
        /// <param name="sysPaging">分页信息</param>
        /// <returns>查询结果</returns>
        //public static PageList<T> PagingCommon<T>(PageParams pageparams)
        //{
        //    var db = SugerBase.GetInstance();


        //    //var pageList = db.Ado.UseStoredProcedure().SqlQuery<T>("PAGER22", parameter);


        //    SugarParameter[] parameter = new SugarParameter[6];
        //    var page = new SugarParameter("page", pageparams.PageIndex);
        //    var pageSize = new SugarParameter("pageSize", pageparams.PageSize);
        //    var tableName = new SugarParameter("tableName", pageparams.TableName);
        //    var strWhere = new SugarParameter("strWhere ", pageparams.StrWhere);
        //    var Orderby = new SugarParameter("Orderby", pageparams.OrderCol);
        //    var numCount = new SugarParameter("numCount", null, true);
        //    //var v_cur = new SugarParameter("v_cur", null, true);

        //    //var pageList = db.Ado.UseStoredProcedure().SqlQuery<dynamic>("PAGER22", parameter);
        //    ////var dt = db.Ado.UseStoredProcedure().GetDataTable("PAGER22",
        //    //      new SugarParameter[]  {
        //    //          page,
        //    //          pageSize,
        //    //          tableName,
        //    //          strWhere,
        //    //          Orderby,
        //    //          numCount,
        //    //          v_cur
        //    //      }
        //    //    );

        //    object outPutValue;
        //    var outputResult = db.Ado.UseStoredProcedure<dynamic>(() =>
        //   {
        //       string spName = "PAGER22";
        //       var dbResult = db.Ado.SqlQueryDynamic(spName, new SugarParameter[] {   page,
        //              pageSize,
        //              tableName,
        //              strWhere,
        //              Orderby,
        //              numCount });
        //       outPutValue = numCount.Value;
        //       return dbResult;
        //   });



        //    //var pageList = JsonConvert.DeserializeObject<PageList<T>>(JsonConvert.SerializeObject(db.Ado.UseStoredProcedure().GetDataTable("pager", parameter)));

        //    PageList<T> pagelist = new PageList<T>();
        //    //  pagelist.listData = pageList;

        //    return pagelist;

        //}
    }
}
