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
        /// 添加角色
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddRole(string name,string qxid)
        {
            var db = SugerBase.GetInstance();
            Role role = new Role();
            role.RoleName = name;
            int i = db.Insertable(role).ExecuteCommand();
            if (i > 0)
            {
                var newId = db.Queryable<Role>().Where(m=>m.RoleName==name).OrderBy("ID desc").Single().ID;
                string[] qxids = qxid.Split(',');
                int state = 0;
                foreach (var item in qxids)
                {
                    int id = Convert.ToInt32(item);
                    RoleAuthority roleAuthority = new RoleAuthority();
                        roleAuthority.AuthorityId = id;
                        roleAuthority.RoleId = Convert.ToInt32(newId);
                    state += db.Insertable(roleAuthority).ExecuteCommand();//记录成功条数
                }
                if(qxids.Length==state)
                {
                    return state;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        public int AddUser(Users users, string roleid)
        {
            var db = SugerBase.GetInstance();
            db.Insertable(users);
            users.Spare = "";
            users.State = 0;
            var addtime = DateTime.Now.ToString("yyyy-MM-dd hh24:mi:ss");
            var x =  db.Insertable<Users>(users);
            int i = db.Insertable(users).ExecuteCommand();
            if (i > 0)
            {
                var newId = db.Queryable<Users>().Where(m => m.LoginName == users.LoginName).First();
                string[] ids = roleid.Split(',');

                int state = 0;
                foreach (var item in ids)
                {
                    int itemid = Convert.ToInt32(item);
                    UserRole userRole = new UserRole();
                    userRole.RoleId = itemid;
                    state += db.Insertable(userRole).ExecuteCommand();

                }
                if (ids.Length == state)
                {
                    return state;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                return 0;
            }
        }
        
        /// <summary>
        /// 批量停用&用户
        /// </summary>
        /// <param name="tablename"></param>
        /// <param name="byid"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public int UpdateUserState(string tablename, string byid, string id) 
        {
            var db = SugerBase.GetInstance();
            string[] ids = id.Split(',');
            int state = 0;
            foreach (var item in ids)
            {
                int intId = Convert.ToInt32(item);
                //var updateUserState = db.SqlQueryable<int>("update "+ tablename + " set state=0 where "+ byid + "= "+ intId).First();
                
            }
            return 1;
        }

        /// <summary>
        /// 获取权限列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public List<Authority> GetAuthority()
        {
            var db = SugerBase.GetInstance();
            var authoritys = db.Queryable<Authority>().ToList();
            return authoritys;
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

        /// <summary>
        /// 角色显示
        /// </summary>
        /// <returns></returns>
        public List<RoleAndAuthority> GetRole()
        {
            var db = SugerBase.GetInstance();
            var rolelist = db.SqlQueryable<RoleAndAuthority>("select a.*,c.Name from role a,RoleAuthority b,Authority c where a.id=b.roleid and b.roleid=c.id");
            return rolelist.ToList();
        }

        /// <summary>
        /// 角色下拉列表
        /// </summary>
        /// <returns></returns>
        public List<Role> GetRoleList(int gid)
        {
            var db = SugerBase.GetInstance();
            var role = db.Queryable<Role>().ToList();
            return role;
        }

        /// <summary>
        /// 显示用户&&角色
        /// </summary>
        /// <returns></returns>
        public List<UserAndRole> ShowUserAndRole()
        {
            var db = SugerBase.GetInstance();
            var userlist = db.SqlQueryable<UserAndRole>("select a.*,c.rolename from users a,userrole b,role c where a.id=b.userid and b.roleid=c.id");
            return userlist.ToList();
        }

    }
}
