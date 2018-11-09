using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Text.RegularExpressions;
using Oracle.ManagedDataAccess.Types;
using System.Collections.Generic;
using System.Linq;
using System.Data.Common;
using System.Reflection;
using Newtonsoft.Json;

namespace JieCheng
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            InfoList<Student>();
            Console.ReadKey();
            //while (true)
            //{
            //    Console.WriteLine("输入计算的数字");
            //    var num = Convert.ToInt32(Console.ReadLine());
            //    Console.Write(num+"的阶乘是: ");
            //    Console.WriteLine();
            //    Console.WriteLine(GetJie(num));
            //}

        }

        static int GetJie(int num)
        {
            if (num == 1)
            {
                return 1;
            }
            else
            {
                return num * GetJie(num - 1);
            }
        }

        static void InfoList<T>()
        {
            string connString = "Data Source=169.254.159.216/ORCL;User Id=scott;Password=tiger";
            OracleConnection conn = new OracleConnection(connString);

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "MF_PAK_001.GetDataByPage";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("p_tableName", OracleDbType.Varchar2).Value = "student";
            cmd.Parameters.Add("p_fields", OracleDbType.Varchar2).Value = "ID,Name,Sex";
            cmd.Parameters.Add("p_filter", OracleDbType.Varchar2).Value = "";
            cmd.Parameters.Add("p_sort", OracleDbType.Varchar2).Value = "ID asc";
            cmd.Parameters.Add("p_curPage", OracleDbType.Int32).Value = 1;
            cmd.Parameters.Add("p_pageSize", OracleDbType.Int32).Value = 10;
            cmd.Parameters.Add("p_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("p_totalRecords", OracleDbType.Int32).Direction = ParameterDirection.Output;

            conn.Open();
            OracleDataReader dr = cmd.ExecuteReader();

            List<T> list = new List<T>();
            while (dr.Read())
            {
                T temp;
                string tempStr = "";
                for (int i = 0; i < dr.FieldCount; i++)
                {
                    if (i!=0)
                    {
                        tempStr += ",";
                    }
                    if (i==0)
                    {
                        tempStr += "{";
                    }
                    //获取列名
                    var name = dr.GetName(i);
                    //获取类型
                    var resType = dr[i].GetType();
                    //获取值
                    Console.WriteLine(dr[i].ToString());

                    tempStr += ("'"+dr.GetName(i).Trim() + "':'"+ dr[i].ToString().Trim()+"'");
                    if (i==dr.FieldCount-1)
                    {
                        tempStr += "}";
                    }
                }
                temp = JsonConvert.DeserializeObject<T>(tempStr);
                list.Add(temp);
            }
            //获取输出的值
            Console.WriteLine("总条数:" + cmd.Parameters["p_totalRecords"].Value);
            conn.Close();
        }

        #region 
        //         1         /// <summary>
        // 2         ///   将SqlDataReader转换为Model实体
        // 3         /// </summary>
        // 4         /// <typeparam name="T">实例类名</typeparam>
        // 5         /// <param name="dr">Reader对象</param>
        // 6         /// <returns>实体对象</returns>
        // 7         public static T ReaderToModel<T>(IDataReader dr)
        // 8         {
        // 9             
        //36         } 


        //OracleDataReader dr = cmd.ExecuteReader();
        //Console.WriteLine("获取的数据" + dr.Read());

        //Student student = new Student();
        //List<Student> list = new List<Student>();
        //var rest = (from DbDataRecord record in cmd.ExecuteReader() select new Student { Name = record["Name"].ToString(), ID = Convert.ToInt32(record["ID"]), Sex = Convert.ToInt32(record["Sex"]) }).ToList();
        #endregion
    }
}
