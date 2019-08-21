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
            #region �˵���ʼ��
            if (!context.SysMenus.Any())
            {
                Menu menu = new Menu()
                {
                    MenuName = "ϵͳ����",
                    MenuGuid = Guid.NewGuid(),
                    MenuDes = "����˵�����ɫ,�û���Ȩ��",
                    MenuPath = "ϵͳ����",
                    SortCode = "009/",
                    CreateTime = DateTime.Now,
                    UpdateTime = DateTime.Now,
                };
                Menu lecture = new Menu()
                {
                    MenuName = "��ʦ����",
                    MenuGuid = Guid.NewGuid(),
                    MenuDes = "����ʦ",
                    MenuPath = "��ʦ����",
                    SortCode = "008/",
                    MenuLink = "/Lecturer",
                    CreateTime = DateTime.Now,
                    UpdateTime = DateTime.Now,
                };
                Menu student = new Menu()
                {
                    MenuName = "ѧԱ����",
                    MenuGuid = Guid.NewGuid(),
                    MenuDes = "����ѧԱ",
                    MenuPath = "ѧԱ����",
                    SortCode = "007/",
                    MenuLink = "/Student",
                    CreateTime = DateTime.Now,
                    UpdateTime = DateTime.Now,
                };
                Menu project = new Menu()
                {
                    MenuName = "��Ŀ����",
                    MenuGuid = Guid.NewGuid(),
                    MenuDes = "������Ŀ",
                    MenuPath = "��Ŀ����",
                    SortCode = "006/",
                    MenuLink = "/Project",
                    CreateTime = DateTime.Now,
                    UpdateTime = DateTime.Now,
                };
                Menu projectsign = new Menu()
                {
                    MenuName = "��Ŀ���ȹ���",
                    MenuGuid = Guid.NewGuid(),
                    MenuDes = "������Ŀ����",
                    MenuPath = "��Ŀ���ȹ���",
                    SortCode = "005/",
                    MenuLink="/ProjectSign",
                    
                    CreateTime = DateTime.Now,
                    UpdateTime = DateTime.Now,
                };

                IList<Menu> MenuList = new List<Menu>()
                {
                    new Menu()
                    {
                        MenuName="�˵�����",
                        MenuGuid=Guid.NewGuid(),
                        MenuDes="����˵���Ϣ",
                        ParentGuid=menu.MenuGuid,
                        MenuPath="ϵͳ����/�˵�����",
                        MenuLink="/Menu",
                        SortCode="009/001",
                        CreateTime=DateTime.Now,
                        UpdateTime=DateTime.Now,
                    },
                     new Menu()
                    {
                        MenuName="Ȩ�޹���",
                        MenuGuid=Guid.NewGuid(),
                        MenuDes="����Ȩ����Ϣ",
                        ParentGuid=menu.MenuGuid,
                        MenuPath="ϵͳ����/Ȩ�޹���",
                        MenuLink="/Author",
                        SortCode="009/002",
                        CreateTime=DateTime.Now,
                        UpdateTime=DateTime.Now,
                    },
                      new Menu()
                    {
                        MenuName="��ɫ����",
                        MenuGuid=Guid.NewGuid(),
                        MenuDes="�����ɫ��Ϣ",
                        ParentGuid=menu.MenuGuid,
                        MenuPath="ϵͳ����/��ɫ����",
                        MenuLink="/Role",
                        SortCode="009/003",
                        CreateTime=DateTime.Now,
                        UpdateTime=DateTime.Now,
                    },
                       new Menu()
                    {
                        MenuName="�û�����",
                        MenuGuid=Guid.NewGuid(),
                        MenuDes="�����û���Ϣ",
                        ParentGuid=menu.MenuGuid,
                        MenuPath="ϵͳ����/�û�����",
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

            #region Ȩ�޳�ʼ��

            List<Author> authorlist = new List<Author>();

            #region ϵͳ����Ȩ��
            Author author = new Author()
            {
                AuthorName = "SysManager",
                AuthorType = "����Ȩ��",
                AuthorPath = "ϵͳ����",
                AuthorSortCode = "001/",
                AuthorDes = "ϵͳ����",
                AuthorGuid = Guid.NewGuid(),
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now,

            };

            authorlist.Add(author);

            #endregion

            #region �˵�����Ȩ��
            Author menuauthor = new Author()
            {
                AuthorName = "MenuManager",
                AuthorType = "ҳ��Ȩ��",
                AuthorPath = "ϵͳ����/�˵�����",
                AuthorSortCode = "001/001/",
                AuthorDes = "�˵�����",
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
                       // AuthorType = "����Ȩ��",
                       // AuthorPath = "ϵͳ����/�˵�����/�˵��б�",
                       // AuthorSortCode = "001/001/001",
                       // AuthorDes = "�˵��б�",
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
                        AuthorType = "����Ȩ��",
                        AuthorPath = "ϵͳ����/�˵�����/��Ӳ˵�",
                        AuthorSortCode = "001/001/001",
                        AuthorDes = "��Ӳ˵�",
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
                        AuthorType = "����Ȩ��",
                        AuthorPath = "ϵͳ����/�˵�����/�˵���ӱ���",
                        AuthorSortCode = "001/001/002",
                        AuthorDes = "��Ӳ˵�",
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
                       // AuthorType = "����Ȩ��",
                       // AuthorPath = "ϵͳ����/�˵�����/�༭�˵���ʼ��",
                       // AuthorSortCode = "001/001/004",
                       // AuthorDes = "�༭�˵���ʼ��",
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
                        AuthorType = "����Ȩ��",
                        AuthorPath = "ϵͳ����/�˵�����/�༭�˵�",
                        AuthorSortCode = "001/001/003",
                        AuthorDes = "�༭�˵�",
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
                        AuthorType = "����Ȩ��",
                        AuthorPath = "ϵͳ����/�˵�����/�˵����±���",
                        AuthorSortCode = "001/001/004",
                        AuthorDes = "�˵����±���",
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
                        AuthorType = "����Ȩ��",
                        AuthorPath = "ϵͳ����/�˵�����/ɾ��",
                        AuthorSortCode = "001/001/005",
                        AuthorDes = "ɾ���˵�",
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

            #region Ȩ�޹���Ȩ��

            Author authorAuthor = new Author()
            {
                AuthorName = "AuthorManager",
                AuthorType = "ҳ��Ȩ��",
                AuthorPath = "ϵͳ����/Ȩ�޹���",
                AuthorSortCode = "001/002/",
                AuthorDes = "Ȩ�޹���",
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
                        //    AuthorType = "����Ȩ��",
                        //    AuthorPath = "ϵͳ����/Ȩ�޹���/Ȩ���б�",
                        //    AuthorSortCode = "001/002/001",
                        //    AuthorDes = "Ȩ���б�",
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
                            AuthorType = "����Ȩ��",
                            AuthorPath = "ϵͳ����/Ȩ�޹���/���Ȩ��",
                            AuthorSortCode = "001/002/001",
                            AuthorDes = "���Ȩ��",
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
                            AuthorType = "����Ȩ��",
                            AuthorPath = "ϵͳ����/Ȩ�޹���/���Ȩ�ޱ���",
                            AuthorSortCode = "001/002/002",
                            AuthorDes = "���Ȩ�ޱ���",
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
                            AuthorType = "����Ȩ��",
                            AuthorPath = "ϵͳ����/Ȩ�޹���/ɾ��",
                            AuthorSortCode = "001/002/003",
                            AuthorDes = "ɾ��Ȩ��",
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
                            AuthorType = "����Ȩ��",
                            AuthorPath = "ϵͳ����/Ȩ�޹���/�༭Ȩ��",
                            AuthorSortCode = "001/002/004",
                            AuthorDes = "�༭Ȩ��",
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
                        //    AuthorType = "����Ȩ��",
                        //    AuthorPath = "ϵͳ����/Ȩ�޹���/�༭Ȩ�޳�ʼ��",
                        //    AuthorSortCode = "001/002/006",
                        //    AuthorDes = "�༭Ȩ�޳�ʼ��",
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
                            AuthorType = "����Ȩ��",
                            AuthorPath = "ϵͳ����/Ȩ�޹���/�༭Ȩ�ޱ���",
                            AuthorSortCode = "001/002/005",
                            AuthorDes = "�༭Ȩ�ޱ���",
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

            #region ��ɫ����Ȩ��
            Author roleauthor = new Author()
            {
                AuthorName = "RoleManager",
                AuthorType = "ҳ��Ȩ��",
                AuthorPath = "ϵͳ����/��ɫ����",
                AuthorSortCode = "001/003/",
                AuthorDes = "��ɫ����",
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
                     AuthorType = "����Ȩ��",
                     AuthorPath = "ϵͳ����/��ɫ����/��ӽ�ɫ",
                     AuthorSortCode = "001/003/001",
                     AuthorDes = "��ӽ�ɫ",
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
                     AuthorType = "����Ȩ��",
                     AuthorPath = "ϵͳ����/��ɫ����/��ӽ�ɫ����",
                     AuthorSortCode = "001/003/002",
                     AuthorDes = "��ӽ�ɫ����",
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
                     AuthorType = "����Ȩ��",
                     AuthorPath = "ϵͳ����/��ɫ����/ɾ����ɫ",
                     AuthorSortCode = "001/003/003",
                     AuthorDes = "ɾ����ɫ",
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
                     AuthorType = "����Ȩ��",
                     AuthorPath = "ϵͳ����/��ɫ����/�༭��ɫ",
                     AuthorSortCode = "001/003/004",
                     AuthorDes = "�༭��ɫ",
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
                     AuthorType = "����Ȩ��",
                     AuthorPath = "ϵͳ����/��ɫ����/��ȡ��ɫ��Ϣ",
                     AuthorSortCode = "001/003/005",
                     AuthorDes = "��ȡ��ɫ��Ϣ",
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
                  //   AuthorType = "����Ȩ��",
                  //   AuthorPath = "ϵͳ����/��ɫ����/��ʼ����ɫ��Ϣ",
                  //   AuthorSortCode = "001/003/006",
                  //   AuthorDes = "��ʼ����ɫ��Ϣ",
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
                     AuthorType = "����Ȩ��",
                     AuthorPath = "ϵͳ����/��ɫ����/�༭��ɫ����",
                     AuthorSortCode = "001/003/006",
                     AuthorDes = "�༭��ɫ����",
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
                  //   AuthorType = "����Ȩ��",
                  //   AuthorPath = "ϵͳ����/��ɫ����/��ɫ�б�",
                  //   AuthorSortCode = "001/003/008",
                  //   AuthorDes = "��ɫ�б�",
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
                     AuthorType = "����Ȩ��",
                     AuthorPath = "ϵͳ����/��ɫ����/ѡ��Ȩ��",
                     AuthorSortCode = "001/003/007",
                     AuthorDes = "�༭��ɫ����",
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
                     AuthorType = "����Ȩ��",
                     AuthorPath = "ϵͳ����/��ɫ����/���½�ɫȨ�ޱ���",
                     AuthorSortCode = "001/003/008",
                     AuthorDes = "���½�ɫȨ�ޱ���",
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

            #region �û�����Ȩ��
            Author userauthor = new Author()
            {
                AuthorName = "UserManager",
                AuthorType = "ҳ��Ȩ��",
                AuthorPath = "ϵͳ����/�û�����",
                AuthorSortCode = "001/004/",
                AuthorDes = "�û�����",
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
                     AuthorType = "����Ȩ��",
                     AuthorPath = "ϵͳ����/��ɫ����/����û�",
                     AuthorSortCode = "001/004/001",
                     AuthorDes = "����û�",
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
                     AuthorType = "����Ȩ��",
                     AuthorPath = "ϵͳ����/��ɫ����/����û�����",
                     AuthorSortCode = "001/004/002",
                     AuthorDes = "����û�����",
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
                     AuthorType = "����Ȩ��",
                     AuthorPath = "ϵͳ����/��ɫ����/�༭�û�",
                     AuthorSortCode = "001/004/003",
                     AuthorDes = "�༭�û�",
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
                     AuthorType = "����Ȩ��",
                     AuthorPath = "ϵͳ����/��ɫ����/�༭�û�����",
                     AuthorSortCode = "001/004/004",
                     AuthorDes = "�༭�û�����",
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
                     AuthorType = "����Ȩ��",
                     AuthorPath = "ϵͳ����/��ɫ����/ɾ���û�",
                     AuthorSortCode = "001/004/005",
                     AuthorDes = "ɾ���û�",
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
                     AuthorType = "����Ȩ��",
                     AuthorPath = "ϵͳ����/��ɫ����/�����û�",
                     AuthorSortCode = "001/004/006",
                     AuthorDes = "�����û�",
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

            #region ��ʦȨ�޹���
            Author lecturerauthor = new Author()
            {
                AuthorName = "LecturerManager",
                AuthorType = "ҳ��Ȩ��",
                AuthorPath = "��ʦ����/",
                AuthorSortCode = "002/",
                AuthorDes = "��ʦ����",
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
                     AuthorType = "����Ȩ��",
                     AuthorPath = "��ʦ����/��ӵ�ʦ",
                     AuthorSortCode = "002/001",
                     AuthorDes = "��ӵ�ʦ",
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
                     AuthorType = "����Ȩ��",
                     AuthorPath = "��ʦ����/��ӵ�ʦ����",
                     AuthorSortCode = "002/002",
                     AuthorDes = "��ӵ�ʦ����",
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
                     AuthorType = "����Ȩ��",
                     AuthorPath = "��ʦ����/�༭��ʦ",
                     AuthorSortCode = "002/003",
                     AuthorDes = "�༭��ʦ",
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
                     AuthorType = "����Ȩ��",
                     AuthorPath = "��ʦ����/�༭��ʦ����",
                     AuthorSortCode = "002/004",
                     AuthorDes = "�༭��ʦ",
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
                     AuthorType = "����Ȩ��",
                     AuthorPath = "��ʦ����/ɾ����ʦ",
                     AuthorSortCode = "002/005",
                     AuthorDes = "ɾ����ʦ",
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
                     AuthorType = "����Ȩ��",
                     AuthorPath = "��ʦ����/�ϴ���ĿͼƬ",
                     AuthorSortCode = "002/006",
                     AuthorDes = "�ϴ���ĿͼƬ",
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

            #region ѧԱȨ�޹���
            Author studentauthor = new Author()
            {
                AuthorName = "StudentManager",
                AuthorType = "ҳ��Ȩ��",
                AuthorPath = "ѧԱ����/",
                AuthorSortCode = "003/",
                AuthorDes = "ѧԱ����",
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
                     AuthorType = "����Ȩ��",
                     AuthorPath = "ѧԱ����/���ѧԱ",
                     AuthorSortCode = "003/001",
                     AuthorDes = "���ѧԱ",
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
                     AuthorType = "����Ȩ��",
                     AuthorPath = "ѧԱ����/���ѧԱ����",
                     AuthorSortCode = "003/002",
                     AuthorDes = "���ѧԱ����",
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
                     AuthorType = "����Ȩ��",
                     AuthorPath = "ѧԱ����/�༭ѧԱ",
                     AuthorSortCode = "003/003",
                     AuthorDes = "�༭ѧԱ",
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
                     AuthorType = "����Ȩ��",
                     AuthorPath = "ѧԱ����/�༭ѧԱ����",
                     AuthorSortCode = "003/004",
                     AuthorDes = "�༭ѧԱ����",
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
                     AuthorType = "����Ȩ��",
                     AuthorPath = "ѧԱ����/ɾ��ѧԱ",
                     AuthorSortCode = "003/005",
                     AuthorDes = "ɾ��ѧԱ",
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
            
            #region ��ĿȨ�޹���
            Author projectauthor = new Author()
            {
                AuthorName = "ProjectManager",
                AuthorType = "ҳ��Ȩ��",
                AuthorPath = "��Ŀ����/",
                AuthorSortCode = "004/",
                AuthorDes = "��Ŀ����",
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
                     AuthorType = "����Ȩ��",
                     AuthorPath = "��Ŀ����/�����Ŀ",
                     AuthorSortCode = "004/001",
                     AuthorDes = "�����Ŀ",
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
                     AuthorType = "����Ȩ��",
                     AuthorPath = "��Ŀ����/�����Ŀ����",
                     AuthorSortCode = "004/002",
                     AuthorDes = "�����Ŀ����",
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
                     AuthorType = "����Ȩ��",
                     AuthorPath = "��Ŀ����/�༭��Ŀ",
                     AuthorSortCode = "004/003",
                     AuthorDes = "�༭��Ŀ",
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
                     AuthorType = "����Ȩ��",
                     AuthorPath = "��Ŀ����/�༭��Ŀ����",
                     AuthorSortCode = "004/004",
                     AuthorDes = "�༭��Ŀ����",
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
                     AuthorType = "����Ȩ��",
                     AuthorPath = "��Ŀ����/������Ŀ",
                     AuthorSortCode = "004/006",
                     AuthorDes = "������Ŀ",
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
                     AuthorType = "����Ȩ��",
                     AuthorPath = "��Ŀ����/�ϴ���Ŀ",
                     AuthorSortCode = "004/007",
                     AuthorDes = "�ϴ���Ŀ",
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
                     AuthorType = "����Ȩ��",
                     AuthorPath = "��Ŀ����/չʾ��Ŀ��Ϣ",
                     AuthorSortCode = "004/008",
                     AuthorDes = "չʾ��Ŀ��Ϣ",
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
                     AuthorType = "����Ȩ��",
                     AuthorPath = "��Ŀ����/ɾ����Ŀ",
                     AuthorSortCode = "004/009",
                     AuthorDes = "ɾ����Ŀ",
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

            #region ��Ŀ����Ȩ�޹���
            Author projectsignauthor = new Author()
            {
                AuthorName = "ProjectSignManager",
                AuthorType = "ҳ��Ȩ��",
                AuthorPath = "��Ŀ����/",
                AuthorSortCode = "005/",
                AuthorDes = "��Ŀ��������",
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
                     AuthorType = "����Ȩ��",
                     AuthorPath = "��Ŀ����/�鿴ѧ����Ϣ",
                     AuthorSortCode = "005/001",
                     AuthorDes = "�鿴ѧ����Ϣ",
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
                     AuthorType = "����Ȩ��",
                     AuthorPath = "��Ŀ����/���",
                     AuthorSortCode = "005/001",
                     AuthorDes = "�鿴ѧ����Ϣ",
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

            #region ��Ŀ����Ȩ�޹���

            #endregion

            #region ��̨��¼��ҳ
            Author behindauthor = new Author()
            {
                AuthorName = "HomeMain",
                AuthorType = "ҳ��Ȩ��",
                AuthorPath = "��̨������/",
                AuthorSortCode = "007/",
                AuthorDes = "��̨������",
                AuthorGuid = Guid.NewGuid(),
                ActionName = "Index",
                ControllerName = "Home",
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now,
            };
            authorlist.Add(behindauthor);
            #endregion

            #region ѧ��ǰ̨��¼��ҳ
            Author frontdauthor = new Author()
            {
                AuthorName = "ProjectlistMain",
                AuthorType = "ҳ��Ȩ��",
                AuthorPath = "ѧ����¼��������/",
                AuthorSortCode = "007/",
                AuthorDes = "ѧ����¼��������",
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
            #region ��ӽ�ɫ��Ϣ

            List<Role> roleList = new List<Role>();

            #region ϵͳ������ɫ
            Role developRole = new Role()
            {
                RoleGuid = Guid.NewGuid(),
                RoleName = "ϵͳ������Ա",
                RolePath = "ϵͳ������Ա",
                RoleDes = "��ϵͳ�Ŀ�����Ա��ӵ��ϵͳ����Ȩ��",
                RoleOrderCode = "001/",
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now,
            };
            //�����е�Ȩ�޶���ӵ�
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

            #region ϵͳ����Ա��ɫ
            //Role adminRole = new Role()
            //{
            //    RoleGuid = Guid.NewGuid(),
            //    RoleName = "ϵͳ������Ա",
            //    RolePath = "ϵͳ������Ա",
            //    RoleDes = "��ϵͳ�Ŀ�����Ա��ӵ��ϵͳ����Ȩ��",
            //    RoleOrderCode = "001/",
            //    CreateTime = DateTime.Now,
            //    UpdateTime = DateTime.Now,
            //};
            #endregion


            #endregion

            #region ����û���Ϣ
            List<User> userList = new List<User>();

            #region ������Ա�û�

            User developUser = new User()
            {
                UserNickName = "¦��",
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

            #region ϵͳ����Ա�û�

            User adminUser = new User()
            {
                UserNickName = "ϵͳ����Ա",
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
