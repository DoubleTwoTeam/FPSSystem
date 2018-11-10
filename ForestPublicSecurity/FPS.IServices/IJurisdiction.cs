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
        List<Authority> GetAuthority();

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

        /// <summary>
        /// 显示角色
        /// </summary>
        /// <returns></returns>
        List<RoleAndAuthority> GetRole();

        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int AddRole(string name,string id);

        /// <summary>
        /// 获取角色下拉
        /// </summary>
        /// <returns></returns>
        List<Role> GetRoleList(int gid=0);

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <returns></returns>
        int AddUser(Users users, string roleid);

        /// <summary>
        /// 显示用户
        /// </summary>
        /// <returns></returns>
        List<UserAndRole> ShowUserAndRole();

        /// <summary>
        /// 修改用户状态
        /// </summary>
        /// <param name="tablename"></param>
        /// <param name="byid"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        int UpdateUserState(string tablename, string byid, string id);
    }
}
