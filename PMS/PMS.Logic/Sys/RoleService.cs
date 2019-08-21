using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMS.Entity;
using PMS.UIModel;
using System.Data.Entity.Validation;

namespace PMS.Logic
{
    public class RoleService
    {

        #region 角色----列表
        public TreeTable<RoleUIModel> GetRoleList()
        {
            using (var db = new EFContext())
            {
                List<RoleUIModel> rolelist = db.SysRoles
                     .Select(x => new RoleUIModel()
                     {
                         RoleIdUIModel = x.RoleId,
                         RoleGuidUIModel = x.RoleGuid,
                         RoleNameUIModel = x.RoleName,
                         RolePathUIModel = x.RolePath,
                         ParentGuidUIModel = x.ParentGuid,
                         IsParent = x.ParentGuid == null ? true : false,
                         IsHavChild = db.SysRoles.Any(m => m.ParentGuid == x.RoleGuid),
                     })
                    .ToList();
                TreeTable<RoleUIModel> TreeTable = new TreeTable<RoleUIModel>();

                TreeTable.code = 0;
                TreeTable.count = rolelist.Count;
                TreeTable.data = rolelist;
                TreeTable.msg = "ok";


                return TreeTable;
            }
        }
        #endregion

        #region 角色----添加
        public int AddRole(Role role)
        {
            using (var db = new EFContext())
            {
                int result = 0; // 0是添加失败，1是添加成功，2是没有此信息无法添加
                //首先判读添加的一级节点还是二级节点
                if (role.ParentGuid.HasValue)
                {
                    //1.//true:添加的是二级节点

                    // 2.//添加角色之前首先检查信息是否存在
                    Role roles = db.SysRoles.FirstOrDefault(x => x.RoleGuid == role.ParentGuid);

                    if (roles != null)
                    {
                        //如果存在的话可以添加子角色信息 
                        result = CommonAddRole(role, db);
                    }
                    else
                    {
                        throw new Exception("本条角色信息不存在，请您刷新后在操作！");
                    }
                }
                else
                {
                    //false:添加的是一级节点
                    result = CommonAddRole(role, db);
                }
                return result;
            }
        }

        public int CommonAddRole(Role role, EFContext ef)
        {
           
            role.RoleGuid = Guid.NewGuid();
            role.CreateTime = DateTime.Now;
            role.UpdateTime = DateTime.Now;

            ef.SysRoles.Add(role);
            int result = ef.SaveChanges();
            return result;
        }

        #endregion

        #region 角色----删除
        /// <summary>
        /// 删除角色信息
        /// </summary>
        /// <param name="roleid"></param>
        /// <returns></returns>
        public int Delete(int roleid)
        {
            using (var db = new EFContext())
            {
                int result = 0; //0表示删除失败 ，1表示删除成功，2抛出的异常

                //首先要检查是否有子节点

                //根据 角色id查找要删除的信息

                Role role = db.SysRoles.FirstOrDefault(x => x.RoleId == roleid);

                if (role == null)
                {
                    //如果是空的话  
                    throw new Exception("要删除的信息不存在，请您刷新列表后在操作！");
                }
                else
                {
                    //1.如果不为空的话

                    //2.再判断是否子节点

                    List<Role> rolelist = db.SysRoles.Where(x => x.ParentGuid == role.RoleGuid).ToList();

                    if (rolelist.Count != 0)
                    {
                        //如果有子节点的话不能删除
                        throw new Exception("要删除的信息存在子节点，请您刷新列表后在操作！");
                    }
                    else
                    {
                        //如果没有的话就可以删除了
                        db.SysRoles.Remove(role);
                        result = db.SaveChanges();
                    }
                }
                return result;
            }
        }
        #endregion

        #region 角色----更新
        public int Update(Role role)
        {
            using (var db = new EFContext())
            {
                int result = 0;
                //更新之前查询此信息是否存在
                Role roles = db.SysRoles.Find(role.RoleId);

                if(roles==null)
                {
                    throw new Exception("要编辑的角色不存在，请您刷新列表后在操作！");
                }
               else
                {
                    roles.RoleName = role.RoleName;
                    roles.RoleOrderCode = role.RoleOrderCode;
                    roles.RoleDes = role.RoleDes;
                    roles.UpdateTime = DateTime.Now;

                    result = db.SaveChanges();
                }
                return result;
            }
        }
        #endregion

        #region 角色----根据角色id查询角色信息

        public RoleUIModel GetRoleById(int roleid)
        {
            using (var db = new EFContext())
            {
                RoleUIModel role = db.SysRoles
                    .Where(x => x.RoleId == roleid)
                    .Select(m => new RoleUIModel()
                    {
                        RoleNameUIModel = m.RoleName,
                        RoleOrderCodeUIModel = m.RoleOrderCode,
                        RoleDesUIModel = m.RoleDes
                    })
                    .FirstOrDefault();

                if (role == null)
                {
                    throw new Exception("要编辑的角色不存在，请您刷新列表以后在操作");
                }

                return role;
            }
        }
        #endregion


        #region 角色----根据角色id查找所有得权限展示用的

        public TreeTable<AuthorUIModel> GetAuthorByRoleId(int rolid)
        {
            using (var db = new EFContext())
            {

                ICollection<Author> list = db.SysRoles.Find(rolid).Authors;
                //.Authors.Select(x => new AuthorUIModel()
                //{
                //    AuthorNameUIModel = x.AuthorName,
                //    AuthorTypeUIModel = x.AuthorType,
                //    ControllerNameUIModel = x.ControllerName,
                //    ActionNameUIModel = x.ActionName,
                //    AuthorPathUIModel = x.AuthorPath,
                //    AuthorGuidUIModel = x.AuthorGuid,
                //    AuthorIdUIModel = x.AuthorId,
                //    ParentGuidUIModel = x.ParentGuid,

                //})

                List<AuthorUIModel> authorlist = new AuthorService().GetAuthorList().data;

                foreach (var item in authorlist)
                {
                    item.LAY_CHECKED = list.Any(m => m.AuthorId == item.AuthorIdUIModel) ? true : false;
                } 

                ////查询所有的权限列表
                //List<AuthorUIModel> authorlist = db.SysAuthors
                //.Select(x => new AuthorUIModel()
                //{
                //    AuthorNameUIModel = x.AuthorName,
                //    AuthorTypeUIModel = x.AuthorType,
                //    ControllerNameUIModel = x.ControllerName,
                //    ActionNameUIModel = x.ActionName,
                //    AuthorPathUIModel = x.AuthorPath,
                //    AuthorGuidUIModel = x.AuthorGuid,
                //    AuthorIdUIModel = x.AuthorId,
                //    ParentGuidUIModel = x.ParentGuid,
                //    LAY_CHECKED = list.Any(m => m.AuthorId==x.AuthorId)?true:false,
                //})
                //.ToList();



                TreeTable<AuthorUIModel> tretableauthorlist = new TreeTable<AuthorUIModel>();

                tretableauthorlist.code = 0;
                tretableauthorlist.count = authorlist.Count;
                tretableauthorlist.data = authorlist;
                tretableauthorlist.msg = "ok";
                return tretableauthorlist;
            }
        }
        #endregion


        #region 角色----根据角色id查找所有得权限
        public ICollection<Author> GetAllAuthor(int? roleid)
        {
            using (var db = new EFContext())
            {
                ICollection<Author> list = new List<Author>();
                if (roleid==null)
                {
                    list = null;
                }
                else
                {
                    list = db.SysRoles.Find(roleid).Authors;
                }
                return list;
            }
        }
        #endregion

        #region 角色-----更新权限
        public int AddAuthorForRole(List<int> authorid, int roleid)
        {
            using (var db = new EFContext())
            {
              
                    //添加权限之前 判断这个脚色是否还存在
                 int result = 0;
                Role role = db.SysRoles.Find(roleid);

                if (role == null)
                {
                    result = -1;
                    throw new Exception("此角色信息不存在，请您刷新列表后在操作！");
                }
                else
                {
                    //如果角色存在,首先把 AuthorUIModel转换成Author

                    ICollection<Author> authorlist = new List<Author>();


                    foreach (var item in authorid)
                    {
                        Author author=  db.SysAuthors.Find(item);

                        authorlist.Add(author);
                    }
                    
                    role.Authors.Clear();

                    foreach (var author in authorlist)
                    {
                        role.Authors.Add(author);
                    }
                  
                    db.Entry(role).State = System.Data.Entity.EntityState.Modified;
                    try
                    {
                       result = db.SaveChanges();
                    }
                    catch (DbEntityValidationException ex)
                    {
                        throw new Exception(ex.Message);
                    }
                   
                }
                return result;
            }
        }
        #endregion
    }
}
