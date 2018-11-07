using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FPS.Models;
using FPS.IServices;

namespace FPS.Services
{
    public class JurisdictionService : IJurisdiction
    {
        /// <summary>
        /// 添加权限
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddAuthority(Authority model)
        {
            var db = SugerBase.GetInstance();
            int i = db.Insertable(model).ExecuteCommand();
            return i;
        }
        /// <summary>
        /// 获取权限列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public List<Authority> GetAuthority(int id)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 获取权限下拉列表
        /// </summary>
        /// <param name="gid"></param>
        /// <returns></returns>

        public List<Authority> GetAuthorityList(int gid = 0)
        {
            var db = SugerBase.GetInstance();
            var authority0 = db.Queryable<Authority>().Where(c => c.FatherId == 0).ToList();
            return authority0;
        }
    }
}
