using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SqlSugar;

namespace FPS.Services
{
    public static class SimpleClientBase
    {
        /// <summary>
        /// 返回一个SimpleClient对象
        /// </summary>
        /// <typeparam name="T">指定操作类</typeparam>
        /// <returns></returns>
        public static SimpleClient<T> GetSimpleClient<T>() where T:class, new()
        {
            SqlSugarClient context = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = SugerBase.DBConnectionString,
                DbType=DbType.Oracle
            });
            context.Ado.IsEnableLogEvent = true;

            SimpleClient<T> client = new SimpleClient<T>(context);
            return client;
        }
    }
}
