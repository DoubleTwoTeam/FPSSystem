using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FPS.Models.DTO;
using FPS.Models;

namespace FPS.Services
{
    /// <summary>
    /// 分页
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Parameters<T> where T : class, new()
    {
        public static PageList<T> GetpageList(PageParams pageParams)    
        {
            var db = SugerBase.GetInstance();
            List<T> list = db.Ado.SqlQuery<T>(
                 pageParams.Fields +
                 pageParams.TableName +
                 pageParams.Filter).Skip(pageParams.CurPage).Take(pageParams.PageSize).ToList();

            int count = db.Ado.SqlQuery<T>(
               pageParams.Fields +
               pageParams.TableName +
               pageParams.Filter).Count();

            PageList<T> pagelist = new PageList<T>() { ListData = list, TotalCount = count };

            return pagelist;
        }
    }
}
