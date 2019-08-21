using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMS.UIModel;
using PMS.Entity;


namespace PMS.Logic
{
  public  class MenuService
    {


        public TreeTable<MenUIModel> TreeTableMenUIModel = new TreeTable<MenUIModel>();


        #region 菜单-----初始化
        /// <summary>
        /// 更具父Guid查找父及菜单
        /// </summary>
        /// <param name="parentGuid"></param>
        /// <returns></returns>
        public List<MenUIModel> GetParentMenu(Guid? parentGuid = null)
        {
            List<MenUIModel> MenUIModelList = new List<MenUIModel>();

            using (var efcontext = new EFContext())
            {
                MenUIModelList = efcontext.SysMenus.Where(x => x.ParentGuid == parentGuid)
                    .Select(n => new MenUIModel()
                    {
                        MenuNameUIModel = n.MenuName,
                        MenuIconUIModel = n.MenuIcon,
                        MenuGuidUIModel = n.MenuGuid,
                        MenuLinkUIModel = n.MenuLink,
                    })
                    .ToList();


                return MenUIModelList;
            }
        }

        /// <summary>
        /// 查询菜单列表
        /// </summary>
        /// <returns></returns>
        public List<MenUIModel> GetMenList()
        {
            List<MenUIModel> MenUIModelList = GetParentMenu();

            if (MenUIModelList.Count != 0)
            {
                foreach (var menuparam in MenUIModelList)
                {
                    List<MenUIModel> MenUIModelListparam = GetParentMenu(menuparam.MenuGuidUIModel);

                    if (MenUIModelListparam.Count != 0)
                    {
                        menuparam.ChildMenuList.AddRange(MenUIModelListparam);
                    }
                }

            }
            return MenUIModelList;
        }
        #endregion

        #region 菜单----菜单列表
        /// <summary>
        /// 菜单列表
        /// </summary>
        /// <returns></returns>
        public TreeTable<MenUIModel> GetMenuExpand()
        {
            List<MenUIModel> MenUIModelList = new List<MenUIModel>();

            using (var efcontext = new EFContext())
            {
                MenUIModelList = efcontext.SysMenus
                    .Select(n => new MenUIModel()
                    {
                        MenuNameUIModel = n.MenuName,
                        MenuIconUIModel = n.MenuIcon,
                        MenuGuidUIModel = n.MenuGuid,
                        MenuLinkUIModel = n.MenuLink,
                        ParentGuidUIModel = n.ParentGuid,
                        MenuPathUIModel = n.MenuPath,
                        MenuIdUIModel = n.MenuId,
                        IsParent = n.ParentGuid == null ? false : true,
                        IsChild = efcontext.SysMenus.Any(x => x.ParentGuid == n.MenuGuid)
                    })
                    .ToList();

                TreeTableMenUIModel.data = MenUIModelList;

                foreach (var item in MenUIModelList)
                {

                }

                TreeTableMenUIModel.code = 0;
                TreeTableMenUIModel.count = MenUIModelList.Count;
                TreeTableMenUIModel.msg = "ok";

                return TreeTableMenUIModel;
            }
        }
        #endregion
        
        #region 菜单-----添加
        /// <summary>
        /// 添加子菜单或一级菜单
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public int AddChildOrParentMenu(Menu menu)
        {
            using (var db = new EFContext())
            {
                #region 判断是否添加的是二级菜单，如果是二级菜单首先要判断一级菜单存不存在

                Menu menus = null;
                int result = 0;
                //如果是二级菜单
                if (menu.ParentGuid.HasValue)
                {
                    menus = db.SysMenus.FirstOrDefault(m => m.MenuGuid == menu.ParentGuid);

                    if (menus == null)
                    {
                        throw new Exception("父级菜单不存在,不允许添加子菜单!");

                    }
                    else
                    {
                        result = AddMenuCommon(menu, db);
                    }
                }
                //如果不是二级菜单
                else
                {
                    result = AddMenuCommon(menu, db);
                }

                return result;

                #endregion

            }
        }
        /// <summary>
        /// 通用添加菜单的方法（一级菜单和二级菜单）
        /// </summary>
        /// <param name="menu"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public int AddMenuCommon(Menu menu, EFContext db)
        {
            int result = 0;
            ///创建时间
            menu.CreateTime = DateTime.Now;
            ///更新时间
            menu.UpdateTime = DateTime.Now;

            menu.MenuGuid = Guid.NewGuid();

            db.SysMenus.Add(menu);

            result = db.SaveChanges();

            if (result != 1)
            {
                throw new Exception("添加失败");
            }

            return result;
        }
        #endregion
        
        #region 菜单----删除
        public int DeleteMenu(int menuid)
        {
            using (var db = new EFContext())
            {
                #region 删除之前要判断此信息是否存在

                int result = 0;
                Menu menu = db.SysMenus.FirstOrDefault(x => x.MenuId == menuid);
                
                if(menu==null)
                {
                    throw new Exception("要删除的菜单不存在,建议您刷新列表信息后再操作");
                }
                else
                {
                    //如果存在 再判断他是否有子菜单

                    List<Menu> menulist = db.SysMenus.Where(x => x.ParentGuid == menu.MenuGuid).ToList();

                    if(menulist.Count!=0)
                    {
                        throw new Exception("要删除的菜单下有子菜单不予许删除,建议您刷新列表信息后再操作");
                    }
                    else
                    {
                        db.SysMenus.Remove(menu);
                        result = db.SaveChanges();
                    }
                   
                }
                return result;
                #endregion
            }
        }
        #endregion

        #region 菜单---根据菜单id查询信息

        public Menu GetMenuInfoByMenuId(int menuid)
        {
            using (var db = new EFContext())
            {
                #region 首先展示信息之前要判断此条信息是否存在
                
                Menu menu = null;

                menu = db.SysMenus.FirstOrDefault(x => x.MenuId == menuid);

                if (menu == null)
                {
                    throw new Exception("此菜单信息不存在,请您刷新列表后再操作！");
                }
                return menu;
                #endregion
            }
        }
        #endregion


        #region 更新菜单
        /// <summary>
        /// 更新菜单
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public int UpdateMenu(Menu menu)
        {
            using (var db = new EFContext())
            {
                Menu menus = null;
                int result = 0;

                menus = db.SysMenus.AsNoTracking().FirstOrDefault(x => x.MenuId == menu.MenuId);

                if(menus == null)
                {
                    throw new Exception("此菜单信息不存在,请您刷新列表后再操作！");
                }
                else
                {
                    menus.MenuName = menu.MenuName;
                    menus.MenuIcon = menu.MenuIcon;
                    menus.MenuLink = menu.MenuLink;
                    menus.SortCode = menu.SortCode;
                    menus.MenuDes = menu.MenuDes;
                    menus.UpdateTime = DateTime.Now;
                    db.Entry(menus).State = System.Data.Entity.EntityState.Modified;
                    result = db.SaveChanges();
                }

                return result;
            }
        }
        #endregion

    }
}
