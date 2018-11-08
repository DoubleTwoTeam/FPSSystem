using System;
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
        public static PageList<T> PagingCommon<T>(PageParams pageparams)
        {
            var db = SugerBase.GetInstance();

            SugarParameter[] parameter = new SugarParameter[5];
            parameter[0] = new SugarParameter("page",pageparams.PageIndex);
            parameter[1] = new SugarParameter("pageSize",pageparams.PageSize);
            parameter[2] = new SugarParameter("tableName", pageparams.TableName);
            parameter[3] = new SugarParameter("strWhere ", pageparams.StrWhere);
            parameter[4] = new SugarParameter("Orderby", pageparams.OrderCol);

            var pageList = JsonConvert.DeserializeObject<PageList<T>>(JsonConvert.SerializeObject(db.Ado.UseStoredProcedure().GetDataTable("pager", parameter)));

            return pageList;

        }
    }
}
