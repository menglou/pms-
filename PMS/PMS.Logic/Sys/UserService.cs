using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMS.UIModel;
using PMS.Entity;
using PMS.Util;

namespace PMS.Logic
{
    public class UserService
    {
        #region 用户----列表
        public DataTableJsonResult<UserUIModel> GetUserList()
        {
            using (var db = new EFContext())
            {
                List<UserUIModel> userlist = db.SysUsers.Where(m => m.IsDelete == 0)
                    .Select(x => new UserUIModel()
                    {
                        UserIdUIModel = x.UserId,
                        UserNickNameUIModel = x.UserNickName,
                        UserPhoneNumberUIModel = x.UserPhoneNumber,
                        UserGenderUIModel = x.UserGender,
                        CreateTimeUIModel = x.CreateTime,
                        IsDisableUIModel = x.IsDisable == 0 ? "正常" : "禁用中",
                    })
                    .ToList();

                DataTableJsonResult<UserUIModel> resultdata = new DataTableJsonResult<UserUIModel>();



                resultdata.code = 0;
                resultdata.data = userlist;

                resultdata.count = userlist.Count;

                return resultdata;

            }
        }
        #endregion


        #region 用户----根据用户id查找用户信息
        public UserUIModel GetUserById(int userid)
        {
            using (var db = new EFContext())
            {
                UserUIModel user = new UserUIModel();
                user = db.SysUsers
                    .Select(x => new UserUIModel()
                    {
                        UserIdUIModel = x.UserId,
                        UserGenderUIModel = x.UserGender,
                        UserPwdUIModel = x.UserPwd,
                        UserNickNameUIModel = x.UserNickName,
                        UserPhoneNumberUIModel = x.UserPhoneNumber,
                        RoleIdUIModel = x.RoleId,
                    })
                    .FirstOrDefault(m => m.UserIdUIModel == userid);


                if (user == null)
                {
                    user = null;
                    throw new Exception("此用户信息不存在，请您刷新列表后再操作！");
                }
                return user;
            }
        }
        #endregion


        #region 用户---获取用户下拉列表

        public List<RoleUIModel> GetRoleList()
        {
            using (var db = new EFContext())
            {
                List<RoleUIModel> rolelist = db.SysRoles.Select(x => new RoleUIModel()
                {
                    RoleIdUIModel = x.RoleId,
                    RoleNameUIModel = x.RoleName,
                }).ToList();

                return rolelist;
            }
        }
        #endregion


        #region 用户-----禁用
        public int DiableUser(int userid)
        {
            using (var db = new EFContext())
            {
                //禁用之前检查是否此用户还存在
                int result = 0;
                User user = db.SysUsers.Find(userid);

                if (user == null)
                {
                    result = 2;
                    throw new Exception("此用户信息不存在，请您刷新列表再操作！");
                }
                else
                {
                    user.UpdateTime = DateTime.Now;
                    user.IsDisable = 1;//1表示禁用

                    db.Entry(user).State = System.Data.Entity.EntityState.Modified;

                    result = db.SaveChanges();
                }

                return result;
            }
        }
        #endregion


        #region 用户----添加用户
        public int AddUser(User user)
        {
            using (var db = new EFContext())
            {
                int result = 0;

                user.RelationGuid = Guid.NewGuid();
                user.IsDisable = 0;
                user.CreateTime = DateTime.Now;
                user.UpdateTime = DateTime.Now;

                db.SysUsers.Add(user);

                result = db.SaveChanges();

                return result;
            }
        }
        #endregion

        #region 用户---更新

        public int Update(User user)
        {
            using (var db = new EFContext())
            {
                int result = 0;
                //更新之前 检查要更新用户是否存在

                User users = db.SysUsers.Find(user.UserId);

                if (users == null)
                {
                    throw new Exception("要编辑的用户不存在，请您刷新列表后在操作");
                }
                else
                {
                    users.UserNickName = user.UserNickName;
                    users.UserGender = user.UserGender;
                    users.UserPhoneNumber = user.UserPhoneNumber;
                    if (users.UserPwd== user.UserPwd)
                    {
                        users.UserPwd =user.UserPwd;
                    }
                    else
                    {
                        users.UserPwd = Encrypt.MD5Encrypt64(user.UserPwd);
                    }
                    users.UserId = user.UserId;
                    users.RoleId = user.RoleId;
                    users.UpdateTime = DateTime.Now;

                    db.Entry(users).State = System.Data.Entity.EntityState.Modified;
                    result = db.SaveChanges();
                }
                return result;
            }
        }
        #endregion

        #region 用户-----删除
        /// <summary>
        /// 删除用户（不是真的删除）
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public int Delete(int userid)
        {
            using (var db = new EFContext())
            {
                //删除之前检查 用户是否存在
                int result = 0;
                User user = db.SysUsers.Find(userid);

                if (user == null)
                {
                    throw new Exception("该用户不存在，请您刷新列表后再操作");
                }
                else
                {
                    user.IsDelete = 1;
                    user.UpdateTime = DateTime.Now;
                    db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                    result = db.SaveChanges();
                }
                return result;
            }
        }
        #endregion


        #region 用户----根据用户id 查询 学员
        public Student GetStudetInfo(int userid)
        {
            using (var db = new EFContext())
            {
                var student = (from list in db.SysUsers
                                       join list2 in db.Students on list.UserId equals list2.UserId select new { StudentId=list2.StudentId,UserId=list.UserId }).FirstOrDefault(x=>x.UserId==userid);


                Student stu = new Student();

                stu.StudentId = student.StudentId;

                return stu;
            }
        }
        #endregion

    }
}
