using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMS.UIModel;
using PMS.Entity;

namespace PMS.Logic
{
    public class AuthorService
    {

        #region 权限---列表

        /// <summary>
        /// 权限列表
        /// </summary>
        /// <returns></returns>
        public TreeTable<AuthorUIModel> GetAuthorList()
        {
            List<AuthorUIModel> authorist = new List<AuthorUIModel>();

            using (var db = new EFContext())
            {
                authorist = db.SysAuthors
                    .Select(x => new AuthorUIModel()
                    {
                        AuthorIdUIModel = x.AuthorId,
                        ActionNameUIModel = x.ActionName,
                        AuthorGuidUIModel = x.AuthorGuid,
                        AuthorPathUIModel = x.AuthorPath,
                        AuthorTypeUIModel = x.AuthorType,
                        AuthorNameUIModel = x.AuthorName,
                        ControllerNameUIModel = x.ControllerName,
                        ParentGuidUIModel = x.ParentGuid,
                        IsHavChild = db.SysAuthors.Any(m => m.ParentGuid == x.AuthorGuid),//是否有子菜单
                    })
                    .ToList();

                TreeTable<AuthorUIModel> TreeTable = new TreeTable<AuthorUIModel>();


                TreeTable.data = authorist;
                TreeTable.code = 0;
                TreeTable.msg = "ok";
                TreeTable.count = authorist.Count;

                return TreeTable;

            }
        }
        #endregion

        #region 权限----添加
        /// <summary>
        /// 添加子节点或父节点（最顶级节点）
        /// </summary>
        /// <param name="author"></param>
        /// <returns></returns>
        public int AddParentOrChildAuthor(Author author)
        {
            using (var db = new EFContext())
            {
                Author authors = null;
                int result = 0;
 
                #region 检查添加的是否是顶级节点
                //如果不是顶级节点
                if (author.ParentGuid.HasValue)
                {
                    //检查父节点是否存在了
                    authors = db.SysAuthors.FirstOrDefault(x => x.AuthorGuid == author.ParentGuid);

                    if (authors == null)
                    {
                        throw new Exception("父节点不存在,建议您刷新以后再操作！");
                    }
                    else//如果存在
                    {
                        ///创建时间
                        author.CreateTime = DateTime.Now;
                        ///更新时间
                        author.UpdateTime = DateTime.Now;
                        author.AuthorGuid = Guid.NewGuid();
                        if (author.AuthorType == "10")
                        {
                            author.AuthorType = "其他权限";
                        }
                        if (author.AuthorType == "20")
                        {
                            author.AuthorType = "页面权限";
                        }
                        if (author.AuthorType == "30")
                        {
                            author.AuthorType = "操作权限";
                        }

                        db.SysAuthors.Add(author);
                        result = db.SaveChanges();
                    }
                }
                else
                {
                    //添加的是顶级节点
                    ///创建时间
                    author.CreateTime = DateTime.Now;
                    ///更新时间
                    author.UpdateTime = DateTime.Now;
                    author.AuthorGuid = Guid.NewGuid();

                    if(author.AuthorType=="10")
                    {
                        author.AuthorType = "其他权限";
                    }
                    if (author.AuthorType == "20")
                    {
                        author.AuthorType = "页面权限";
                    }
                    if (author.AuthorType == "30")
                    {
                        author.AuthorType = "操作权限";
                    }
                    db.SysAuthors.Add(author);
                    result = db.SaveChanges();

                }
                #endregion
                return result;

            }
        }
        
        public int CheckSelfIsHave(Guid authorguid)
        {
            using (var db = new EFContext())
            {
                int result = 0;
                Author Author = db.SysAuthors.FirstOrDefault(x => x.AuthorGuid == authorguid);

                if (Author == null)
                {
                    throw new Exception("此条权限信息不存在,建议您刷新以后再操作！");
                }
                else
                {
                    result = 1;
                }
                return result;
            }
        }
        #endregion
        
        #region 权限------删除
         
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="authorid"></param>
        /// <returns></returns>
        public int Delete(int authorid)
        {
            using (var db = new EFContext())
            {

                int result = 0;
                Author author = null;
                //删除之前检查所要删除的信息是否还存在

                author = db.SysAuthors.FirstOrDefault(x => x.AuthorId == authorid);

                if(author==null)
                {
                    throw new Exception("删除的权限信息不存在，请您刷新列表后再操作！");
                }
                else
                {
                    db.SysAuthors.Remove(author);
                    result= db.SaveChanges();
                }

                return result;
            }
        }
        #endregion


        #region 权限---根据权限id查寻信息

        public AuthorUIModel GetAuthorById(int authorid)
        {
            using (var db = new EFContext())
            {
                AuthorUIModel author = (from s in db.SysAuthors.AsNoTracking()
                                        where s.AuthorId == authorid
                                        select new AuthorUIModel()
                                        {
                                            ActionNameUIModel = s.ActionName,
                                            AuthorGuidUIModel = s.AuthorGuid,
                                            AuthorIdUIModel = s.AuthorId,
                                            AuthorNameUIModel = s.AuthorName,
                                            AuthorPathUIModel = s.AuthorPath,
                                            ControllerNameUIModel = s.ControllerName,
                                            ParentGuidUIModel = s.ParentGuid,
                                            AuthorTypeUIModel=s.AuthorType,
                                            AuthorDesUIModel=s.AuthorDes,
                                        }).FirstOrDefault();



                if (author==null)
                {
                    throw new Exception("要编辑的权限信息不存在，请您刷新以后在操作！");
                }
                else
                {
                    if (author.AuthorTypeUIModel == "其他权限")
                    {
                        author.AuthorTypeUIModel = "10";
                    }
                    else if (author.AuthorTypeUIModel == "页面权限")
                    {
                        author.AuthorTypeUIModel = "20";
                    }
                    else if (author.AuthorTypeUIModel == "操作权限")
                    {
                        author.AuthorTypeUIModel = "30";
                    }
                }
                return author;
            }
        }
        #endregion


        #region 权限-----保存
        public int UpdateAuthorSave(Author author)
        {
            using (var db = new EFContext())
            {
                int result = 0;
                //先判断编辑的词条信息是否存在
                Author authors = db.SysAuthors.Find(author.AuthorId);

                if(authors==null)
                {
                    throw new Exception("要编辑的权限信息不存在，请您刷新列表后在操作！");
                }
                else
                {
                    authors.AuthorName = author.AuthorName;
                    authors.AuthorSortCode = author.AuthorSortCode;
                    authors.AuthorType = author.AuthorType;
                    authors.ControllerName = author.ControllerName;
                    authors.ActionName = author.ActionName;
                    authors.AuthorDes = author.AuthorDes;
                    authors.UpdateTime = DateTime.Now;

                    db.Entry(authors).State = System.Data.Entity.EntityState.Modified;
                    result= db.SaveChanges();
                }

                return result;
            }
        }
        #endregion
    }
}
