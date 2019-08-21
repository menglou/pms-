using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMS.Entity.Migrations;

namespace PMS.Entity
{
   public  class EFContext:DbContext
    {
        public EFContext():base()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<EFContext, Configuration>());
        }

        //  protected override 

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Role>().HasMany<Author>(s => s.Authors).WithMany(x => x.Roles).Map(m =>
            {
                m.MapLeftKey("RoleId");
                m.MapRightKey("AuthorId");
                m.ToTable("RoleAuthor");

            });

            //  modelBuilder.Entity<User>().HasRequired(s => s.Lecturers).WithRequiredPrincipal(x => x.Users);
            //modelBuilder.Entity<Lecturer>().HasKey(m => m.LecturerId);
            //modelBuilder.Entity<User>().HasRequired(m => m.Lecturers).WithOptional(n => n.Users).Map(x=>x.;

            modelBuilder.Entity<Student>().HasMany(x => x.ProjectSigns).WithRequired(m => m.Student).WillCascadeOnDelete(false);
        }

        /// <summary>
        /// 菜单表
        /// </summary>
        public DbSet<Menu> SysMenus { get; set; }

        /// <summary>
        /// 权限表
        /// </summary>
        public DbSet<Author> SysAuthors { get; set; }

        /// <summary>
        /// 角色表
        /// </summary>
        public DbSet<Role> SysRoles { get; set; }

        /// <summary>
        /// 用户表
        /// </summary>
        public DbSet<User> SysUsers { get; set; }


        /// <summary>
        /// 导师表
        /// </summary>
        public DbSet<Lecturer> Lecturers { get; set; }

        /// <summary>
        /// 学员表
        /// </summary>
        public DbSet<Student> Students { get; set; }

        /// <summary>
        /// 项目表
        /// </summary>
        public DbSet<Project> Projects { get; set; }

        /// <summary>
        /// 项目报名表
        /// </summary>
        public DbSet<ProjectSign> ProjectSign { get; set; }

        /// <summary>
        /// 项目进度表
        /// </summary>
        public DbSet<ProjectProgress> ProjectProgress { get; set; }

    }
}
