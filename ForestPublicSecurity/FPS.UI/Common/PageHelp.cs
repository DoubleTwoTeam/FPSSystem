using FPS.Models.DTO;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace FPS.UI.Common
{
    public class PageHelp : IPageHelper
    {
        IConfiguration configuration;
        public PageHelp(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        public  PageList<T> InfoList<T>(PageParams pageParams)
        {
            var connectionString = configuration.GetSection("ConnectionStrings").GetSection("EmployeeConnection").Value;
            var conn = new OracleConnection(connectionString);

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "MF_PAK_001.GetDataByPage";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("p_tableName", OracleDbType.Varchar2).Value = pageParams.TableName;
            cmd.Parameters.Add("p_fields", OracleDbType.Varchar2).Value = pageParams.Fields;
            cmd.Parameters.Add("p_filter", OracleDbType.Varchar2).Value = pageParams.Filter;
            cmd.Parameters.Add("p_sort", OracleDbType.Varchar2).Value = pageParams.Sort;
            cmd.Parameters.Add("p_curPage", OracleDbType.Int32).Value = pageParams.CurPage;
            cmd.Parameters.Add("p_pageSize", OracleDbType.Int32).Value = pageParams.PageSize;
            cmd.Parameters.Add("p_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("p_totalRecords", OracleDbType.Int32).Direction = ParameterDirection.Output;

            conn.Open();
            OracleDataReader dr = cmd.ExecuteReader();

            PageList<T> info = new PageList<T>();
            List<T> list = new List<T>();
            while (dr.Read())
            {
                T temp;
                string tempStr = "";
                for (int i = 0; i < dr.FieldCount; i++)
                {
                    if (i != 0)
                    {
                        tempStr += ",";
                    }
                    if (i == 0)
                    {
                        tempStr += "{";
                    }
                    //获取列名
                    var name = dr.GetName(i);
                    //获取类型
                    var resType = dr[i].GetType();
                    //获取值
                    Console.WriteLine(dr[i].ToString());

                    tempStr += ("'" + dr.GetName(i).Trim() + "':'" + dr[i].ToString().Trim() + "'");
                    if (i == dr.FieldCount - 1)
                    {
                        tempStr += "}";
                    }
                }
                temp = JsonConvert.DeserializeObject<T>(tempStr);
                list.Add(temp);
            }
            info.ListData = list;
            info.TotalCount = Convert.ToInt32(cmd.Parameters["p_totalRecords"].Value.ToString());

            //获取输出的值
            return info;
        }
    }
}
