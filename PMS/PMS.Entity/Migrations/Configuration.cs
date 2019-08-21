namespace PMS.Entity.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using System.Collections.Generic;
    using PMS.Util;

    internal sealed class Configuration : DbMigrationsConfiguration<PMS.Entity.EFContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = false;
            ContextKey = "StudentProject.Entity.EFContext";
        }

     
        protected override void Seed(PMS.Entity.EFContext context)
        {
            base.Seed(context);
            #region 菜单初始化
            if (!context.SysMenus.Any())
            {
                Menu menu = new Menu()
                {
                    MenuName = "系统管理",
                    MenuGuid = Guid.NewGuid(),
                    MenuDes = "管理菜单，角色,用户，权限",
                    MenuPath = "系统管理",
                    SortCode = "009/",
                    CreateTime = DateTime.Now,
                    UpdateTime = DateTime.Now,
                };
                Menu lecture = new Menu()
                {
                    MenuName = "导师管理",
                    MenuGuid = Guid.NewGuid(),
                    MenuDes = "管理导师",
                    MenuPath = "导师管理",
                    SortCode = "008/",
                    MenuLink = "/Lecturer",
                    CreateTime = DateTime.Now,
                    UpdateTime = DateTime.Now,
                };
                Menu student = new Menu()
                {
                    MenuName = "学员管理",
                    MenuGuid = Guid.NewGuid(),
                    MenuDes = "管理学员",
                    MenuPath = "学员管理",
                    SortCode = "007/",
                    MenuLink = "/Student",
                    CreateTime = DateTime.Now,
                    UpdateTime = DateTime.Now,
                };
                Menu project = new Menu()
                {
                    MenuName = "项目管理",
                    MenuGuid = Guid.NewGuid(),
                    MenuDes = "管理项目",
                    MenuPath = "项目管理",
                    SortCode = "006/",
                    MenuLink = "/Project",
                    CreateTime = DateTime.Now,
                    UpdateTime = DateTime.Now,
                };
                Menu projectsign = new Menu()
                {
                    MenuName = "项目进度管理",
                    MenuGuid = Guid.NewGuid(),
                    MenuDes = "管理项目进度",
                    MenuPath = "项目进度管理",
                    SortCode = "005/",
                    MenuLink="/ProjectSign",
                    
                    CreateTime = DateTime.Now,
                    UpdateTime = DateTime.Now,
                };

                IList<Menu> MenuList = new List<Menu>()
                {
                    new Menu()
                    {
                        MenuName="菜单管理",
                        MenuGuid=Guid.NewGuid(),
                        MenuDes="管理菜单信息",
                        ParentGuid=menu.MenuGuid,
                        MenuPath="系统管理/菜单管理",
                        MenuLink="/Menu",
                        SortCode="009/001",
                        CreateTime=DateTime.Now,
                        UpdateTime=DateTime.Now,
                    },
                     new Menu()
                    {
                        MenuName="权限管理",
                        MenuGuid=Guid.NewGuid(),
                        MenuDes="管理权限信息",
                        ParentGuid=menu.MenuGuid,
                        MenuPath="系统管理/权限管理",
                        MenuLink="/Author",
                        SortCode="009/002",
                        CreateTime=DateTime.Now,
                        UpdateTime=DateTime.Now,
                    },
                      new Menu()
                    {
                        MenuName="角色管理",
                        MenuGuid=Guid.NewGuid(),
                        MenuDes="管理角色信息",
                        ParentGuid=menu.MenuGuid,
                        MenuPath="系统管理/角色管理",
                        MenuLink="/Role",
                        SortCode="009/003",
                        CreateTime=DateTime.Now,
                        UpdateTime=DateTime.Now,
                    },
                       new Menu()
                    {
                        MenuName="用户管理",
                        MenuGuid=Guid.NewGuid(),
                        MenuDes="管理用户信息",
                        ParentGuid=menu.MenuGuid,
                        MenuPath="系统管理/用户管理",
                        MenuLink="/User",
                        SortCode="009/004",
                        CreateTime=DateTime.Now,
                        UpdateTime=DateTime.Now,
                    },
                };
                context.SysMenus.Add(menu);
                context.SysMenus.Add(lecture);
                context.SysMenus.Add(student);
                context.SysMenus.Add(project);
                context.SysMenus.Add(projectsign);
                context.SysMenus.AddRange(MenuList);

               
            }

            #endregion

            #region 权限初始化

            List<Author> authorlist = new List<Author>();

            #region 系统管理权限
            Author author = new Author()
            {
                AuthorName = "SysManager",
                AuthorType = "其他权限",
                AuthorPath = "系统管理",
                AuthorSortCode = "001/",
                AuthorDes = "系统管理",
                AuthorGuid = Guid.NewGuid(),
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now,

            };

            authorlist.Add(author);

            #endregion

            #region 菜单管理权限
            Author menuauthor = new Author()
            {
                AuthorName = "MenuManager",
                AuthorType = "页面权限",
                AuthorPath = "系统管理/菜单管理",
                AuthorSortCode = "001/001/",
                AuthorDes = "菜单管理",
                AuthorGuid = Guid.NewGuid(),
                ParentGuid = author.AuthorGuid,
                ActionName = "Index",
                ControllerName = "Menu",
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now,
            };

            List<Author> menuAuthorList = new List<Author>()
                    {
                       //new Author()
                       //{
                       // AuthorName = "MenuList",
                       // AuthorType = "操作权限",
                       // AuthorPath = "系统管理/菜单管理/菜单列表",
                       // AuthorSortCode = "001/001/001",
                       // AuthorDes = "菜单列表",
                       // AuthorGuid = Guid.NewGuid(),
                       // ParentGuid = menuauthor.AuthorGuid,
                       // ActionName = "GetMenuExpand",
                       // ControllerName = "Menu",
                       // CreateTime = DateTime.Now,
                       // UpdateTime = DateTime.Now,
                       //},
                       new Author()
                       {
                        AuthorName = "AddMenu",
                        AuthorType = "操作权限",
                        AuthorPath = "系统管理/菜单管理/添加菜单",
                        AuthorSortCode = "001/001/001",
                        AuthorDes = "添加菜单",
                        AuthorGuid = Guid.NewGuid(),
                        ParentGuid = menuauthor.AuthorGuid,
                        ActionName = "Create",
                        ControllerName = "Menu",
                        CreateTime = DateTime.Now,
                        UpdateTime = DateTime.Now,
                       },
                        new Author()
                       {
                        AuthorName = "AddMenu",
                        AuthorType = "操作权限",
                        AuthorPath = "系统管理/菜单管理/菜单添加保存",
                        AuthorSortCode = "001/001/002",
                        AuthorDes = "添加菜单",
                        AuthorGuid = Guid.NewGuid(),
                        ParentGuid = menuauthor.AuthorGuid,
                        ActionName = "AddChildOrParentMenu",
                        ControllerName = "Menu",
                        CreateTime = DateTime.Now,
                        UpdateTime = DateTime.Now,
                       },
                       //new Author()
                       //{
                       // AuthorName = "UpdateMenuInit",
                       // AuthorType = "操作权限",
                       // AuthorPath = "系统管理/菜单管理/编辑菜单初始化",
                       // AuthorSortCode = "001/001/004",
                       // AuthorDes = "编辑菜单初始化",
                       // AuthorGuid = Guid.NewGuid(),
                       // ParentGuid = menuauthor.AuthorGuid,
                       // ActionName = "UpdateInit",
                       // ControllerName = "Menu",
                       // CreateTime = DateTime.Now,
                       // UpdateTime = DateTime.Now,
                       //},
                        new Author()
                       {
                        AuthorName = "Update",
                        AuthorType = "操作权限",
                        AuthorPath = "系统管理/菜单管理/编辑菜单",
                        AuthorSortCode = "001/001/003",
                        AuthorDes = "编辑菜单",
                        AuthorGuid = Guid.NewGuid(),
                        ParentGuid = menuauthor.AuthorGuid,
                        ActionName = "Update",
                        ControllerName = "Menu",
                        CreateTime = DateTime.Now,
                        UpdateTime = DateTime.Now,
                       },
                        new Author()
                       {
                        AuthorName = "UpdateMenu",
                        AuthorType = "操作权限",
                        AuthorPath = "系统管理/菜单管理/菜单更新保存",
                        AuthorSortCode = "001/001/004",
                        AuthorDes = "菜单更新保存",
                        AuthorGuid = Guid.NewGuid(),
                        ParentGuid = menuauthor.AuthorGuid,
                        ActionName = "UpdateMenu",
                        ControllerName = "Menu",
                        CreateTime = DateTime.Now,
                        UpdateTime = DateTime.Now,
                       },
                         new Author()
                       {
                        AuthorName = "Delete",
                        AuthorType = "操作权限",
                        AuthorPath = "系统管理/菜单管理/删除",
                        AuthorSortCode = "001/001/005",
                        AuthorDes = "删除菜单",
                        AuthorGuid = Guid.NewGuid(),
                        ParentGuid = menuauthor.AuthorGuid,
                        ActionName = "Delete",
                        ControllerName = "Menu",
                        CreateTime = DateTime.Now,
                        UpdateTime = DateTime.Now,
                       },
                    };

            authorlist.Add(menuauthor);
            authorlist.AddRange(menuAuthorList);


            #endregion

            #region 权限管理权限

            Author authorAuthor = new Author()
            {
                AuthorName = "AuthorManager",
                AuthorType = "页面权限",
                AuthorPath = "系统管理/权限管理",
                AuthorSortCode = "001/002/",
                AuthorDes = "权限管理",
                AuthorGuid = Guid.NewGuid(),
                ParentGuid = author.AuthorGuid,
                ActionName = "Index",
                ControllerName = "Author",
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now,
            };

            List<Author> authorAuthorList = new List<Author>()
                    {
                        //new Author()
                        //{
                        //    AuthorName = "AuthorList",
                        //    AuthorType = "操作权限",
                        //    AuthorPath = "系统管理/权限管理/权限列表",
                        //    AuthorSortCode = "001/002/001",
                        //    AuthorDes = "权限列表",
                        //    AuthorGuid = Guid.NewGuid(),
                        //    ParentGuid = authorAuthor.AuthorGuid,
                        //    ActionName = "GetAuthorExpand",
                        //    ControllerName = "Author",
                        //    CreateTime = DateTime.Now,
                        //    UpdateTime = DateTime.Now,
                        //},
                        new Author()
                        {
                            AuthorName = "AddAuthor",
                            AuthorType = "操作权限",
                            AuthorPath = "系统管理/权限管理/添加权限",
                            AuthorSortCode = "001/002/001",
                            AuthorDes = "添加权限",
                            AuthorGuid = Guid.NewGuid(),
                            ParentGuid = authorAuthor.AuthorGuid,
                            ActionName = "Create",
                            ControllerName = "Author",
                            CreateTime = DateTime.Now,
                            UpdateTime = DateTime.Now,
                        },
                         new Author()
                        {
                            AuthorName = "AddAuthorSave",
                            AuthorType = "操作权限",
                            AuthorPath = "系统管理/权限管理/添加权限保存",
                            AuthorSortCode = "001/002/002",
                            AuthorDes = "添加权限保存",
                            AuthorGuid = Guid.NewGuid(),
                            ParentGuid = authorAuthor.AuthorGuid,
                            ActionName = "AddParentOrChildAuthor",
                            ControllerName = "Author",
                            CreateTime = DateTime.Now,
                            UpdateTime = DateTime.Now,
                        },
                          new Author()
                        {
                            AuthorName = "Delete",
                            AuthorType = "操作权限",
                            AuthorPath = "系统管理/权限管理/删除",
                            AuthorSortCode = "001/002/003",
                            AuthorDes = "删除权限",
                            AuthorGuid = Guid.NewGuid(),
                            ParentGuid = authorAuthor.AuthorGuid,
                            ActionName = "Delete",
                            ControllerName = "Author",
                            CreateTime = DateTime.Now,
                            UpdateTime = DateTime.Now,
                        },
                            new Author()
                        {
                            AuthorName = "Update",
                            AuthorType = "操作权限",
                            AuthorPath = "系统管理/权限管理/编辑权限",
                            AuthorSortCode = "001/002/004",
                            AuthorDes = "编辑权限",
                            AuthorGuid = Guid.NewGuid(),
                            ParentGuid = authorAuthor.AuthorGuid,
                            ActionName = "Update",
                            ControllerName = "Author",
                            CreateTime = DateTime.Now,
                            UpdateTime = DateTime.Now,
                        },
                        //      new Author()
                        //{
                        //    AuthorName = "UpdateInit",
                        //    AuthorType = "操作权限",
                        //    AuthorPath = "系统管理/权限管理/编辑权限初始化",
                        //    AuthorSortCode = "001/002/006",
                        //    AuthorDes = "编辑权限初始化",
                        //    AuthorGuid = Guid.NewGuid(),
                        //    ParentGuid = authorAuthor.AuthorGuid,
                        //    ActionName = "UpdateInit",
                        //    ControllerName = "Author",
                        //    CreateTime = DateTime.Now,
                        //    UpdateTime = DateTime.Now,
                        //},
                               new Author()
                        {
                            AuthorName = "UpdateSave",
                            AuthorType = "操作权限",
                            AuthorPath = "系统管理/权限管理/编辑权限保存",
                            AuthorSortCode = "001/002/005",
                            AuthorDes = "编辑权限保存",
                            AuthorGuid = Guid.NewGuid(),
                            ParentGuid = authorAuthor.AuthorGuid,
                            ActionName = "UpdateSave",
                            ControllerName = "Author",
                            CreateTime = DateTime.Now,
                            UpdateTime = DateTime.Now,
                        },
                    };
            authorlist.Add(authorAuthor);
            authorlist.AddRange(authorAuthorList);


            #endregion

            #region 角色管理权限
            Author roleauthor = new Author()
            {
                AuthorName = "RoleManager",
                AuthorType = "页面权限",
                AuthorPath = "系统管理/角色管理",
                AuthorSortCode = "001/003/",
                AuthorDes = "角色管理",
                AuthorGuid = Guid.NewGuid(),
                ParentGuid = author.AuthorGuid,
                ActionName = "Index",
                ControllerName = "Role",
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now,
            };

            List<Author> roleAuthorList = new List<Author>()
              {
                  new Author()
                  {
                     AuthorName = "AddRoleView",
                     AuthorType = "操作权限",
                     AuthorPath = "系统管理/角色管理/添加角色",
                     AuthorSortCode = "001/003/001",
                     AuthorDes = "添加角色",
                     AuthorGuid = Guid.NewGuid(),
                     ParentGuid = roleauthor.AuthorGuid,
                     ActionName = "Create",
                     ControllerName = "Role",
                     CreateTime = DateTime.Now,
                     UpdateTime = DateTime.Now,
                  },
                  new Author()
                  {
                     AuthorName = "AddRoleSave",
                     AuthorType = "操作权限",
                     AuthorPath = "系统管理/角色管理/添加角色保存",
                     AuthorSortCode = "001/003/002",
                     AuthorDes = "添加角色保存",
                     AuthorGuid = Guid.NewGuid(),
                     ParentGuid = roleauthor.AuthorGuid,
                     ActionName = "AddRole",
                     ControllerName = "Role",
                     CreateTime = DateTime.Now,
                     UpdateTime = DateTime.Now,
                  },
                  new Author()
                  {
                     AuthorName = "DeleteRole",
                     AuthorType = "操作权限",
                     AuthorPath = "系统管理/角色管理/删除角色",
                     AuthorSortCode = "001/003/003",
                     AuthorDes = "删除角色",
                     AuthorGuid = Guid.NewGuid(),
                     ParentGuid = roleauthor.AuthorGuid,
                     ActionName = "Delete",
                     ControllerName = "Role",
                     CreateTime = DateTime.Now,
                     UpdateTime = DateTime.Now,
                  },
                  new Author()
                  {
                     AuthorName = "UpdateRoleView",
                     AuthorType = "操作权限",
                     AuthorPath = "系统管理/角色管理/编辑角色",
                     AuthorSortCode = "001/003/004",
                     AuthorDes = "编辑角色",
                     AuthorGuid = Guid.NewGuid(),
                     ParentGuid = roleauthor.AuthorGuid,
                     ActionName = "Update",
                     ControllerName = "Role",
                     CreateTime = DateTime.Now,
                     UpdateTime = DateTime.Now,
                  },
                   new Author()
                  {
                     AuthorName = "GetAuthorByRoleIdAction",
                     AuthorType = "操作权限",
                     AuthorPath = "系统管理/角色管理/获取角色信息",
                     AuthorSortCode = "001/003/005",
                     AuthorDes = "获取角色信息",
                     AuthorGuid = Guid.NewGuid(),
                     ParentGuid = roleauthor.AuthorGuid,
                     ActionName = "GetAuthorByRoleId",
                     ControllerName = "Role",
                     CreateTime = DateTime.Now,
                     UpdateTime = DateTime.Now,
                  },
                  //  new Author()
                  //{
                  //   AuthorName = "UpdateInitRole",
                  //   AuthorType = "操作权限",
                  //   AuthorPath = "系统管理/角色管理/初始化角色信息",
                  //   AuthorSortCode = "001/003/006",
                  //   AuthorDes = "初始化角色信息",
                  //   AuthorGuid = Guid.NewGuid(),
                  //   ParentGuid = roleauthor.AuthorGuid,
                  //   ActionName = "UpdateInit",
                  //   ControllerName = "Role",
                  //   CreateTime = DateTime.Now,
                  //   UpdateTime = DateTime.Now,
                  //},
                     new Author()
                  {
                     AuthorName = "UpdateRoleSave",
                     AuthorType = "操作权限",
                     AuthorPath = "系统管理/角色管理/编辑角色保存",
                     AuthorSortCode = "001/003/006",
                     AuthorDes = "编辑角色保存",
                     AuthorGuid = Guid.NewGuid(),
                     ParentGuid = roleauthor.AuthorGuid,
                     ActionName = "UpdateRole",
                     ControllerName = "Role",
                     CreateTime = DateTime.Now,
                     UpdateTime = DateTime.Now,
                  },
                  //     new Author()
                  //{
                  //   AuthorName = "GetRoleExpandView",
                  //   AuthorType = "操作权限",
                  //   AuthorPath = "系统管理/角色管理/角色列表",
                  //   AuthorSortCode = "001/003/008",
                  //   AuthorDes = "角色列表",
                  //   AuthorGuid = Guid.NewGuid(),
                  //   ParentGuid = roleauthor.AuthorGuid,
                  //   ActionName = "GetRoleExpand",
                  //   ControllerName = "Role",
                  //   CreateTime = DateTime.Now,
                  //   UpdateTime = DateTime.Now,
                  //},
                          new Author()
                  {
                     AuthorName = "ChoseAuthorView",
                     AuthorType = "操作权限",
                     AuthorPath = "系统管理/角色管理/选择权限",
                     AuthorSortCode = "001/003/007",
                     AuthorDes = "编辑角色保存",
                     AuthorGuid = Guid.NewGuid(),
                     ParentGuid = roleauthor.AuthorGuid,
                     ActionName = "ChoseAuthor",
                     ControllerName = "Role",
                     CreateTime = DateTime.Now,
                     UpdateTime = DateTime.Now,
                  },
                             new Author()
                  {
                     AuthorName = "UpateRoleAuthor",
                     AuthorType = "操作权限",
                     AuthorPath = "系统管理/角色管理/更新角色权限保存",
                     AuthorSortCode = "001/003/008",
                     AuthorDes = "更新角色权限保存",
                     AuthorGuid = Guid.NewGuid(),
                     ParentGuid = roleauthor.AuthorGuid,
                     ActionName = "UpateRoleAuthor",
                     ControllerName = "Role",
                     CreateTime = DateTime.Now,
                     UpdateTime = DateTime.Now,
                  },
              };
            authorlist.Add(roleauthor);
            authorlist.AddRange(roleAuthorList);
            #endregion

            #region 用户管理权限
            Author userauthor = new Author()
            {
                AuthorName = "UserManager",
                AuthorType = "页面权限",
                AuthorPath = "系统管理/用户管理",
                AuthorSortCode = "001/004/",
                AuthorDes = "用户管理",
                AuthorGuid = Guid.NewGuid(),
                ParentGuid = author.AuthorGuid,
                ActionName = "Index",
                ControllerName = "User",
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now,
            };

            List<Author> userauthorlist = new List<Author>()
            {
                new Author()
                  {
                     AuthorName = "AddUserView",
                     AuthorType = "操作权限",
                     AuthorPath = "系统管理/角色管理/添加用户",
                     AuthorSortCode = "001/004/001",
                     AuthorDes = "添加用户",
                     AuthorGuid = Guid.NewGuid(),
                     ParentGuid = userauthor.AuthorGuid,
                     ActionName = "Create",
                     ControllerName = "User",
                     CreateTime = DateTime.Now,
                     UpdateTime = DateTime.Now,
                  },
                new Author()
                  {
                     AuthorName = "AddUserSave",
                     AuthorType = "操作权限",
                     AuthorPath = "系统管理/角色管理/添加用户保存",
                     AuthorSortCode = "001/004/002",
                     AuthorDes = "添加用户保存",
                     AuthorGuid = Guid.NewGuid(),
                     ParentGuid = userauthor.AuthorGuid,
                     ActionName = "AddUser",
                     ControllerName = "User",
                     CreateTime = DateTime.Now,
                     UpdateTime = DateTime.Now,
                  },
                new Author()
                  {
                     AuthorName = "UpdateUserView",
                     AuthorType = "操作权限",
                     AuthorPath = "系统管理/角色管理/编辑用户",
                     AuthorSortCode = "001/004/003",
                     AuthorDes = "编辑用户",
                     AuthorGuid = Guid.NewGuid(),
                     ParentGuid = userauthor.AuthorGuid,
                     ActionName = "Update",
                     ControllerName = "User",
                     CreateTime = DateTime.Now,
                     UpdateTime = DateTime.Now,
                  },
                new Author()
                  {
                     AuthorName = "UpdateUserSave",
                     AuthorType = "操作权限",
                     AuthorPath = "系统管理/角色管理/编辑用户保存",
                     AuthorSortCode = "001/004/004",
                     AuthorDes = "编辑用户保存",
                     AuthorGuid = Guid.NewGuid(),
                     ParentGuid = userauthor.AuthorGuid,
                     ActionName = "UpdateSave",
                     ControllerName = "User",
                     CreateTime = DateTime.Now,
                     UpdateTime = DateTime.Now,
                  },
                new Author()
                  {
                     AuthorName = "DeleteUser",
                     AuthorType = "操作权限",
                     AuthorPath = "系统管理/角色管理/删除用户",
                     AuthorSortCode = "001/004/005",
                     AuthorDes = "删除用户",
                     AuthorGuid = Guid.NewGuid(),
                     ParentGuid = userauthor.AuthorGuid,
                     ActionName = "Delete",
                     ControllerName = "User",
                     CreateTime = DateTime.Now,
                     UpdateTime = DateTime.Now,
                  },
                new Author()
                  {
                     AuthorName = "DisableUserAction",
                     AuthorType = "操作权限",
                     AuthorPath = "系统管理/角色管理/禁用用户",
                     AuthorSortCode = "001/004/006",
                     AuthorDes = "禁用用户",
                     AuthorGuid = Guid.NewGuid(),
                     ParentGuid = userauthor.AuthorGuid,
                     ActionName = "DisableUser",
                     ControllerName = "User",
                     CreateTime = DateTime.Now,
                     UpdateTime = DateTime.Now,
                  },
            };
            authorlist.Add(userauthor);
            authorlist.AddRange(userauthorlist);
            #endregion

            #region 导师权限管理
            Author lecturerauthor = new Author()
            {
                AuthorName = "LecturerManager",
                AuthorType = "页面权限",
                AuthorPath = "导师管理/",
                AuthorSortCode = "002/",
                AuthorDes = "导师管理",
                AuthorGuid = Guid.NewGuid(),
                ActionName = "Index",
                ControllerName = "Lecturer",
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now,
            };
            List<Author> lecturerauthorList = new List<Author>()
            {
                new Author()
                  {
                     AuthorName = "AddLecturerView",
                     AuthorType = "操作权限",
                     AuthorPath = "导师管理/添加导师",
                     AuthorSortCode = "002/001",
                     AuthorDes = "添加导师",
                     AuthorGuid = Guid.NewGuid(),
                     ParentGuid = lecturerauthor.AuthorGuid,
                     ActionName = "Create",
                     ControllerName = "Lecturer",
                     CreateTime = DateTime.Now,
                     UpdateTime = DateTime.Now,
                  },
                 new Author()
                  {
                     AuthorName = "AddLecturerSave",
                     AuthorType = "操作权限",
                     AuthorPath = "导师管理/添加导师保存",
                     AuthorSortCode = "002/002",
                     AuthorDes = "添加导师保存",
                     AuthorGuid = Guid.NewGuid(),
                     ParentGuid = lecturerauthor.AuthorGuid,
                     ActionName = "AddLecture",
                     ControllerName = "Lecturer",
                     CreateTime = DateTime.Now,
                     UpdateTime = DateTime.Now,
                  },
                 new Author()
                  {
                     AuthorName = "UpdateLecturerView",
                     AuthorType = "操作权限",
                     AuthorPath = "导师管理/编辑导师",
                     AuthorSortCode = "002/003",
                     AuthorDes = "编辑导师",
                     AuthorGuid = Guid.NewGuid(),
                     ParentGuid = lecturerauthor.AuthorGuid,
                     ActionName = "Update",
                     ControllerName = "Lecturer",
                     CreateTime = DateTime.Now,
                     UpdateTime = DateTime.Now,
                  },
                  new Author()
                  {
                     AuthorName = "UpdateLecturerSave",
                     AuthorType = "操作权限",
                     AuthorPath = "导师管理/编辑导师保存",
                     AuthorSortCode = "002/004",
                     AuthorDes = "编辑导师",
                     AuthorGuid = Guid.NewGuid(),
                     ParentGuid = lecturerauthor.AuthorGuid,
                     ActionName = "UpdateLecture",
                     ControllerName = "Lecturer",
                     CreateTime = DateTime.Now,
                     UpdateTime = DateTime.Now,
                  },
                  new Author()
                  {
                     AuthorName = "DeleteLecturerAction",
                     AuthorType = "操作权限",
                     AuthorPath = "导师管理/删除导师",
                     AuthorSortCode = "002/005",
                     AuthorDes = "删除导师",
                     AuthorGuid = Guid.NewGuid(),
                     ParentGuid = lecturerauthor.AuthorGuid,
                     ActionName = "DeleteLecturer",
                     ControllerName = "Lecturer",
                     CreateTime = DateTime.Now,
                     UpdateTime = DateTime.Now,
                  },
                  new Author()
                  {
                     AuthorName = "DUploadImageAction",
                     AuthorType = "操作权限",
                     AuthorPath = "导师管理/上传项目图片",
                     AuthorSortCode = "002/006",
                     AuthorDes = "上传项目图片",
                     AuthorGuid = Guid.NewGuid(),
                     ParentGuid = lecturerauthor.AuthorGuid,
                     ActionName = "UploadImage",
                     ControllerName = "Lecturer",
                     CreateTime = DateTime.Now,
                     UpdateTime = DateTime.Now,
                  },
            };
            authorlist.Add(lecturerauthor);
            authorlist.AddRange(lecturerauthorList);
            #endregion

            #region 学员权限管理
            Author studentauthor = new Author()
            {
                AuthorName = "StudentManager",
                AuthorType = "页面权限",
                AuthorPath = "学员管理/",
                AuthorSortCode = "003/",
                AuthorDes = "学员管理",
                AuthorGuid = Guid.NewGuid(),
                ActionName = "Index",
                ControllerName = "Student",
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now,
            };
            List<Author> studentauthorlist = new List<Author>()
            {
                 new Author()
                  {
                     AuthorName = "AddStudentView",
                     AuthorType = "操作权限",
                     AuthorPath = "学员管理/添加学员",
                     AuthorSortCode = "003/001",
                     AuthorDes = "添加学员",
                     AuthorGuid = Guid.NewGuid(),
                     ParentGuid = studentauthor.AuthorGuid,
                     ActionName = "Create",
                     ControllerName = "Student",
                     CreateTime = DateTime.Now,
                     UpdateTime = DateTime.Now,
                  },
                  new Author()
                  {
                     AuthorName = "AddStudentSave",
                     AuthorType = "操作权限",
                     AuthorPath = "学员管理/添加学员保存",
                     AuthorSortCode = "003/002",
                     AuthorDes = "添加学员保存",
                     AuthorGuid = Guid.NewGuid(),
                     ParentGuid = studentauthor.AuthorGuid,
                     ActionName = "AddStudent",
                     ControllerName = "Student",
                     CreateTime = DateTime.Now,
                     UpdateTime = DateTime.Now,
                  },
                   new Author()
                  {
                     AuthorName = "UpdateStudentView",
                     AuthorType = "操作权限",
                     AuthorPath = "学员管理/编辑学员",
                     AuthorSortCode = "003/003",
                     AuthorDes = "编辑学员",
                     AuthorGuid = Guid.NewGuid(),
                     ParentGuid = studentauthor.AuthorGuid,
                     ActionName = "Update",
                     ControllerName = "Student",
                     CreateTime = DateTime.Now,
                     UpdateTime = DateTime.Now,
                  },
                    new Author()
                  {
                     AuthorName = "UpdateStudentSave",
                     AuthorType = "操作权限",
                     AuthorPath = "学员管理/编辑学员保存",
                     AuthorSortCode = "003/004",
                     AuthorDes = "编辑学员保存",
                     AuthorGuid = Guid.NewGuid(),
                     ParentGuid = studentauthor.AuthorGuid,
                     ActionName = "UpdateStudent",
                     ControllerName = "Student",
                     CreateTime = DateTime.Now,
                     UpdateTime = DateTime.Now,
                  },
                       new Author()
                  {
                     AuthorName = "DeleteAction",
                     AuthorType = "操作权限",
                     AuthorPath = "学员管理/删除学员",
                     AuthorSortCode = "003/005",
                     AuthorDes = "删除学员",
                     AuthorGuid = Guid.NewGuid(),
                     ParentGuid = studentauthor.AuthorGuid,
                     ActionName = "Delete",
                     ControllerName = "Student",
                     CreateTime = DateTime.Now,
                     UpdateTime = DateTime.Now,
                  },
                  
            };
            authorlist.Add(studentauthor);
            authorlist.AddRange(studentauthorlist);
            #endregion
            
            #region 项目权限管理
            Author projectauthor = new Author()
            {
                AuthorName = "ProjectManager",
                AuthorType = "页面权限",
                AuthorPath = "项目管理/",
                AuthorSortCode = "004/",
                AuthorDes = "项目管理",
                AuthorGuid = Guid.NewGuid(),
                ActionName = "Index",
                ControllerName = "Project",
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now,
            };
            List<Author> projectauthorlist = new List<Author>()
            {
                 new Author()
                  {
                     AuthorName = "AddProjectView",
                     AuthorType = "操作权限",
                     AuthorPath = "项目管理/添加项目",
                     AuthorSortCode = "004/001",
                     AuthorDes = "添加项目",
                     AuthorGuid = Guid.NewGuid(),
                     ParentGuid = projectauthor.AuthorGuid,
                     ActionName = "Create",
                     ControllerName = "Project",
                     CreateTime = DateTime.Now,
                     UpdateTime = DateTime.Now,
                  },
                 new Author()
                  {
                     AuthorName = "AddProjectSave",
                     AuthorType = "操作权限",
                     AuthorPath = "项目管理/添加项目保存",
                     AuthorSortCode = "004/002",
                     AuthorDes = "添加项目保存",
                     AuthorGuid = Guid.NewGuid(),
                     ParentGuid = projectauthor.AuthorGuid,
                     ActionName = "AddProjectFor",
                     ControllerName = "Project",
                     CreateTime = DateTime.Now,
                     UpdateTime = DateTime.Now,
                  },
                  new Author()
                  {
                     AuthorName = "UpdateProjectView",
                     AuthorType = "操作权限",
                     AuthorPath = "项目管理/编辑项目",
                     AuthorSortCode = "004/003",
                     AuthorDes = "编辑项目",
                     AuthorGuid = Guid.NewGuid(),
                     ParentGuid = projectauthor.AuthorGuid,
                     ActionName = "Update",
                     ControllerName = "Project",
                     CreateTime = DateTime.Now,
                     UpdateTime = DateTime.Now,
                  },
                   new Author()
                  {
                     AuthorName = "UpdateProjectSave",
                     AuthorType = "操作权限",
                     AuthorPath = "项目管理/编辑项目保存",
                     AuthorSortCode = "004/004",
                     AuthorDes = "编辑项目保存",
                     AuthorGuid = Guid.NewGuid(),
                     ParentGuid = projectauthor.AuthorGuid,
                     ActionName = "UpdateProject",
                     ControllerName = "Project",
                     CreateTime = DateTime.Now,
                     UpdateTime = DateTime.Now,
                  },
                    new Author()
                  {
                     AuthorName = "ApplayProAction",
                     AuthorType = "操作权限",
                     AuthorPath = "项目管理/申请项目",
                     AuthorSortCode = "004/006",
                     AuthorDes = "申请项目",
                     AuthorGuid = Guid.NewGuid(),
                     ParentGuid = projectauthor.AuthorGuid,
                     ActionName = "ApplayPro",
                     ControllerName = "Project",
                     CreateTime = DateTime.Now,
                     UpdateTime = DateTime.Now,
                  },
                      new Author()
                  {
                     AuthorName = "UploadImageAction",
                     AuthorType = "操作权限",
                     AuthorPath = "项目管理/上传项目",
                     AuthorSortCode = "004/007",
                     AuthorDes = "上传项目",
                     AuthorGuid = Guid.NewGuid(),
                     ParentGuid = projectauthor.AuthorGuid,
                     ActionName = "UploadImage",
                     ControllerName = "Project",
                     CreateTime = DateTime.Now,
                     UpdateTime = DateTime.Now,
                  },
                    new Author()
                  {
                     AuthorName = "PreviewView",
                     AuthorType = "操作权限",
                     AuthorPath = "项目管理/展示项目信息",
                     AuthorSortCode = "004/008",
                     AuthorDes = "展示项目信息",
                     AuthorGuid = Guid.NewGuid(),
                     ParentGuid = projectauthor.AuthorGuid,
                     ActionName = "Preview",
                     ControllerName = "Project",
                     CreateTime = DateTime.Now,
                     UpdateTime = DateTime.Now,
                  },
                      new Author()
                  {
                     AuthorName = "DeleteAction",
                     AuthorType = "操作权限",
                     AuthorPath = "项目管理/删除项目",
                     AuthorSortCode = "004/009",
                     AuthorDes = "删除项目",
                     AuthorGuid = Guid.NewGuid(),
                     ParentGuid = projectauthor.AuthorGuid,
                     ActionName = "Delete",
                     ControllerName = "Project",
                     CreateTime = DateTime.Now,
                     UpdateTime = DateTime.Now,
                  },
            };
            authorlist.Add(projectauthor);
            authorlist.AddRange(projectauthorlist);
            #endregion

            #region 项目进度权限管理
            Author projectsignauthor = new Author()
            {
                AuthorName = "ProjectSignManager",
                AuthorType = "页面权限",
                AuthorPath = "项目管理/",
                AuthorSortCode = "005/",
                AuthorDes = "项目报名管理",
                AuthorGuid = Guid.NewGuid(),
                ActionName = "Index",
                ControllerName = "ProjectSign",
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now,
            };
            List<Author> projectsignauthorlist = new List<Author>()
            {
                new Author()
                  {
                     AuthorName = "PreviewStudentInfoAction",
                     AuthorType = "操作权限",
                     AuthorPath = "项目管理/查看学生信息",
                     AuthorSortCode = "005/001",
                     AuthorDes = "查看学生信息",
                     AuthorGuid = Guid.NewGuid(),
                     ParentGuid = projectsignauthor.AuthorGuid,
                     ActionName = "PreviewStudentInfo",
                     ControllerName = "ProjectSign",
                     CreateTime = DateTime.Now,
                     UpdateTime = DateTime.Now,
                  },
                new Author()
                  {
                     AuthorName = "AuditAction",
                     AuthorType = "操作权限",
                     AuthorPath = "项目管理/审核",
                     AuthorSortCode = "005/001",
                     AuthorDes = "查看学生信息",
                     AuthorGuid = Guid.NewGuid(),
                     ParentGuid = projectsignauthor.AuthorGuid,
                     ActionName = "Audit",
                     ControllerName = "ProjectSign",
                     CreateTime = DateTime.Now,
                     UpdateTime = DateTime.Now,
                  },
            };
            authorlist.Add(projectsignauthor);
            authorlist.AddRange(projectsignauthorlist);
            #endregion

            #region 项目报名权限管理

            #endregion

            #region 后台登录主页
            Author behindauthor = new Author()
            {
                AuthorName = "HomeMain",
                AuthorType = "页面权限",
                AuthorPath = "后台主界面/",
                AuthorSortCode = "007/",
                AuthorDes = "后台主界面",
                AuthorGuid = Guid.NewGuid(),
                ActionName = "Index",
                ControllerName = "Home",
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now,
            };
            authorlist.Add(behindauthor);
            #endregion

            #region 学生前台登录主页
            Author frontdauthor = new Author()
            {
                AuthorName = "ProjectlistMain",
                AuthorType = "页面权限",
                AuthorPath = "学生登录后主界面/",
                AuthorSortCode = "007/",
                AuthorDes = "学生登录后主界面",
                AuthorGuid = Guid.NewGuid(),
                ActionName = "Index",
                ControllerName = "ProjectList",
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now,
            };
            authorlist.Add(frontdauthor);
            #endregion

            #endregion

            if (!context.SysAuthors.Any())
            {
                context.SysAuthors.AddRange(authorlist);
              
            }
            #region 添加角色信息

            List<Role> roleList = new List<Role>();

            #region 系统开发角色
            Role developRole = new Role()
            {
                RoleGuid = Guid.NewGuid(),
                RoleName = "系统开发人员",
                RolePath = "系统开发人员",
                RoleDes = "本系统的开发人员，拥有系统所有权限",
                RoleOrderCode = "001/",
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now,
            };
            //把所有的权限都添加到
            foreach (var authorityEntity in authorlist)
            {
                developRole.Authors.Add(authorityEntity);
            }

            roleList.Add(developRole);

            if (!context.SysRoles.Any())
            {
                context.SysRoles.AddRange(roleList);
                context.SaveChanges();
            }

            #endregion

            #region 系统管理员角色
            //Role adminRole = new Role()
            //{
            //    RoleGuid = Guid.NewGuid(),
            //    RoleName = "系统开发人员",
            //    RolePath = "系统开发人员",
            //    RoleDes = "本系统的开发人员，拥有系统所有权限",
            //    RoleOrderCode = "001/",
            //    CreateTime = DateTime.Now,
            //    UpdateTime = DateTime.Now,
            //};
            #endregion


            #endregion

            #region 添加用户信息
            List<User> userList = new List<User>();

            #region 开发人员用户

            User developUser = new User()
            {
                UserNickName = "娄猛",
                RelationGuid = Guid.NewGuid(),
                UserPhoneNumber = "16622503663",
                UserPwd = Encrypt.MD5Encrypt64("123456"),
                UserGender = 0,
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now,
                RoleId = developRole.RoleId,
                IsDisable = 0,
            };

            userList.Add(developUser);

            #endregion

            #region 系统管理员用户

            User adminUser = new User()
            {
                UserNickName = "系统管理员",
                RelationGuid = Guid.NewGuid(),
                UserPhoneNumber = "16622503664",
                UserPwd = Encrypt.MD5Encrypt64("123456"),
                UserGender = 0,
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now,
                RoleId = developRole.RoleId,
                IsDisable = 0,
            };

            userList.Add(adminUser);



            #endregion


            if (!context.SysUsers.Any())
            {
                context.SysUsers.AddRange(userList);
                
            }
          
            #endregion

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
