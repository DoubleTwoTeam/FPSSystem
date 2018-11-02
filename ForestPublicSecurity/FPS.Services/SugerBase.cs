using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SqlSugar;
using Oracle.ManagedDataAccess.Client;
using Oracle;

namespace FPS.Services
{
    public static class SugerBase
    {
        public static string DBConnectionString { get; set; }
        public static SqlSugarClient GetInstance()
        {
            SqlSugarClient db = new SqlSugarClient(new ConnectionConfig() { ConnectionString = DBConnectionString, DbType = DbType.Oracle, IsAutoCloseConnection = true });
            db.Ado.IsEnableLogEvent = true;
            db.Ado.LogEventStarting = (sql, pars) =>
            {
                Console.WriteLine(sql + "\r\n" + db.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value)));
                Console.WriteLine();
            };
            return db;
        }
    }
}
