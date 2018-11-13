using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FPS.Models;
using FPS.IServices;
using SqlSugar;

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
        public int AddRole(string name, string qxid)
        {
            var db = SugerBase.GetInstance();
            Role role = new Role();
            role.RoleName = name;
            int i = db.Insertable(role).ExecuteCommand();
            if (i > 0)
            {
                var newId = db.Queryable<Role>().Where(m => m.RoleName == name).OrderBy("ID desc").First().ID;
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
                return qxids.Length == state ? state : -1;
            }
            return 0;
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
            var x = db.Insertable<Users>(users);
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
                    userRole.UserId = newId.ID;//用户ID
                    state += db.Insertable(userRole).ExecuteCommand();

                }
                return ids.Length == state ? state : 0;
            }
            return 0;
        }

        /// <summary>
        /// 批量停用权限
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteAuthority(string id)
        {
            SqlSugarClient context = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = "Data Source=169.254.159.216/orcl;User ID=scott;Password=tiger;",
                DbType = DbType.Oracle
            });
            context.Ado.IsEnableLogEvent = true;

            SimpleClient<Authority> simple = new SimpleClient<Authority>(context);

            var db = SugerBase.GetInstance();
            string[] ids = id.Split(',');
            int state = 0;
            foreach (var item in ids)
            {
                int intId = Convert.ToInt32(item);
                state += (simple.Update(m => new Authority() { State = 1 }, m => m.ID == intId)) ? 1 : 0;
            }
            return state;
        }

        /// <summary>
        /// 批量停用角色
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteRole(string id)
        {
            SqlSugarClient context = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = "Data Source=169.254.159.216/orcl;User ID=scott;Password=tiger;",
                DbType = DbType.Oracle
            });
            context.Ado.IsEnableLogEvent = true;

            SimpleClient<Role> simple = new SimpleClient<Role>(context);

            var db = SugerBase.GetInstance();
            string[] ids = id.Split(',');
            int state = 0;
            foreach (var item in ids)
            {
                int intId = Convert.ToInt32(item);
                state += (simple.Update(m => new Role() { State = 1 }, m => m.ID == intId)) ? 1 : 0;
            }
            return state;
        }
        
        /// <summary>
        /// 批量停用&用户
        /// </summary>
        /// <param name="tablename"></param>
        /// <param name="byid"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteUser(string tablename, string byid, string id)
        {
            SqlSugarClient context = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = "Data Source=169.254.159.216/orcl;User ID=scott;Password=tiger;",
                DbType = DbType.Oracle
            });
            context.Ado.IsEnableLogEvent = true;

            SimpleClient<Users> simple = new SimpleClient<Users>(context);

            var db = SugerBase.GetInstance();
            string[] ids = id.Split(',');
            int state = 0;
            foreach (var item in ids)
            {
                int intId = Convert.ToInt32(item);
                //var updateUserState = db.SqlQueryable<int>("update " + tablename + " set state=0 where " + byid + "= " + intId).First();
                state += (simple.Update(m => new Users() { State = 1 }, m => m.ID == intId)) ? 1 : 0;
            }
            return state;
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
            var rolelist = db.SqlQueryable<RoleAndAuthority>("select *  from Role");
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

        /// <summary>
        /// 修改权限保存
        /// </summary>
        /// <param name="authority"></param>
        /// <returns></returns>
        public int UpdateAuthority(Authority authority)
        {
            var db = SugerBase.GetInstance();
            //var client = SimpleClientBase.GetSimpleClient<Authority>();
            //var result = db.Updateable(authority).Where(m => m.ID == authority.ID).ExecuteCommand();
            //return result;
            int result = db.Updateable(authority).Where(m => m.ID == authority.ID).ExecuteCommand();
            return result;
        }

        /// <summary>
        /// 修改权限反填
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Authority UpdateAuthorityShow(int id)
        {
            var db = SugerBase.GetInstance();
            List<Authority> list = db.SqlQueryable<Authority>("select * from Authority where ID=" + id).ToList();
            Authority authority = list[0];
            return authority;
        }

        /// <summary>
        /// 修改角色保存
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public int UpdateRole(Authority authority, string qxid,int id)
        {
            var db = SugerBase.GetInstance();
            var client = SimpleClientBase.GetSimpleClient<Role>();
            var result = db.Updateable(authority).Where(m => m.ID == authority.ID).ExecuteCommand();
            //update关联表
            if (result > 0)
            {
                var linkClient = SimpleClientBase.GetSimpleClient<RoleAuthority>();
                var res = linkClient.Update(m => new RoleAuthority { RoleId = Convert.ToInt32(qxid) }, m => m.RoleId == authority.ID);
                return res ? 1 : 0;
            }
            return 0;
        }

        /// <summary>
        /// 修改角色反填
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public RoleAndAuthority UpdateRoleShow(int id)
        {
            var db = SugerBase.GetInstance();
            var role = db.SqlQueryable<RoleAndAuthority>("select a.*,c.name from role a,ROLEAUTHORITY b,Authority c where a.id=b.roleid and b.Authorityid=c.id and a.ID=" + id).First();
            return role;
        }

        /// <summary>
        /// 修改用户保存
        /// </summary>
        /// <param name="id"></param>
        /// <param name="users"></param>8x
        /// <param name="roleid"></param>
        /// <returns></returns>
        public int UpdateUser(Users users, string roleid, int id)
        {
            var db = SugerBase.GetInstance();
            var client = SimpleClientBase.GetSimpleClient<Users>();
            var result = db.Updateable(users).Where(m=>m.ID==users.ID).ExecuteCommand();
            //update关联表
            if (result > 0)
            {
                var linkClient = SimpleClientBase.GetSimpleClient<UserRole>();
                var res = linkClient.Update(m => new UserRole { RoleId = Convert.ToInt32(roleid) }, m => m.UserId == users.ID);
                return res?1:0;
            }
            return 0;
            //if (i > 0)
            //{
            //    string[] qxids = roleid.Split(',');
            //    int state = 0;
            //    foreach (var item in qxids)
            //    {
            //        int newId = Convert.ToInt32(item);
            //        RoleAuthority roleAuthority = new RoleAuthority();
            //        roleAuthority.AuthorityId = id;
            //        roleAuthority.RoleId = newId;
            //        state += db.Insertable(roleAuthority).ExecuteCommand();//记录成功条数
            //    }
            //    return qxids.Length == state ? 1 : 0;
            //}
            //return result ? 1 : 0;
        }

        /// <summary>
        /// 修改用户反填
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UserAndRole UpdateUserShow(int id)
        {
            var db = SugerBase.GetInstance();
            var user = db.SqlQueryable<UserAndRole>("select a.*,c.rolename from users a,userrole b,role c where a.id=b.userid and b.roleid=c.id and a.ID=" + id).Single();
            return user;
        }
    }
}
