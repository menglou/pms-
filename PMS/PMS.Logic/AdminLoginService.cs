using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMS.Entity;
using PMS.UIModel;
using PMS.Util;

namespace PMS.Logic
{
   public class AdminLoginService
    {
        public User AdminLogin(User user,out string statuscode)
        {
            using (var db = new EFContext())
            {
                User users = db.SysUsers.FirstOrDefault(x => x.UserPhoneNumber == user.UserPhoneNumber&&x.IsDelete==0);//判断用户是否存在

                if(users!=null)
                {
                    //再判断密码
                    if(users.UserPwd==Encrypt.MD5Encrypt64(user.UserPwd))
                    {
                        if(users.IsDisable==1)
                        {
                            statuscode = "账户被禁用";
                        }
                        else
                        {
                            statuscode = "登录成功";
                        }
                    }
                    else
                    {
                        statuscode = "用户名或密码错误";
                    }
                }
                else
                {
                    statuscode = "用户不存在！";
                    users = null;
                }
                return users;
            }
        }

        public User AdminLogin(User user)
        {
            using (var db = new EFContext())
            {
                User users = db.SysUsers.FirstOrDefault(x => x.UserId== user.UserId && x.IsDelete == 0);//判断用户是否存在

                if (users != null)
                {
                    //再判断密码
                    if (users.UserPwd == Encrypt.MD5Encrypt64(user.UserPwd))
                    {
                        if (users.IsDisable == 1)
                        {
                            //statuscode = "账户被禁用";
                        }
                        else
                        {
                            //statuscode = "登录成功";
                        }
                    }
                    else
                    {
                        //statuscode = "用户名或密码错误";
                    }
                }
                else
                {
                    //statuscode = "用户不存在！";
                    users = null;
                }
                return users;
            }
        }

        public Role GetRole(int? roleid)
        {
            using (var db = new EFContext())
            {
                Role role = new Role();
                if(roleid==null)
                {
                    role = null;
                }
                else
                {
                    role = db.SysRoles.Find(roleid);
                }
                return role;
            }
        }
      
    }

}
