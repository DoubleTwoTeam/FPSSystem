using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FPS.Models;

namespace FPS.IServices
{
   public interface IJurisdiction
    {
        /// <summary>
        /// 显示权限
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<Authority> GetAuthority(int id);
        /// <summary>
        /// 添加权限
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>

        int AddAuthority(Authority model);
        /// <summary>
        /// 获取权限下拉框
        /// </summary>
        /// <param name="gid"></param>
        /// <returns></returns>

        List<Authority> GetAuthorityList(int gid = 0);
    }
}
