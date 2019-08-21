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
  public  class StudentService
    {
        #region 学员-----获取学员列表
        public DataTableJsonResult<StudentUIModel> GetStudentList()
        {
            using (var db = new EFContext())
            {
                List<StudentUIModel> stulist = db.Students
                    .Select(x => new StudentUIModel()
                    {
                        StudentIdUIModel = x.StudentId,
                        StudentNameUIModel=x.StudentName,
                        StudentGenderUIModel=x.StudentGender,
                        StudentPhoneNumberUIModel=x.StudentPhoneNumber,
                        CityUIModel=x.City,
                        EducationUIModel=x.Education,
                        CreateTimeUIModel=x.CreateTime,
                    })
                    .ToList();

                DataTableJsonResult<StudentUIModel> tablestu = new DataTableJsonResult<StudentUIModel>();
                tablestu.code = 0;
                tablestu.count = stulist.Count;
                tablestu.data = stulist;
                return tablestu;
            }
        }
        #endregion


        #region 学员-----添加
        public int AddStudent(Student student)
        {
            using (var db = new EFContext())
            {

                int result = 0;

                student.CreateTime = DateTime.Now;
                student.UpdateTime = DateTime.Now;


                //创建用户表

                User user = new User()
                {
                    UserNickName = student.StudentName,
                    UserPhoneNumber = student.StudentPhoneNumber,
                    UserGender = student.StudentGender,
                    UserPwd = Encrypt.MD5Encrypt64("123456"),
                    IsDelete = 0,
                    IsDisable=0,
                    RelationGuid = Guid.NewGuid(),
                    CreateTime = DateTime.Now,
                    UpdateTime = DateTime.Now,
                };

                student.UserId = user.UserId;


                using (var tran = db.Database.BeginTransaction())
                {
                    db.SysUsers.Add(user);
                    db.Students.Add(student);


                    result = db.SaveChanges();

                    tran.Commit();

                    if(result!=2)
                    {
                        tran.Rollback();
                    }
                }
                return result;
            }
        }
        #endregion


        #region 学员----根据学员Id查询学员信息
        public StudentUIModel GetStudentById(int studentid)
        {
            using (var db = new EFContext())
            {
                StudentUIModel student = db.Students
                    .Select(x => new StudentUIModel()
                    {
                        StudentIdUIModel=x.StudentId,
                        StudentNameUIModel=x.StudentName,
                        StudentGenderUIModel=x.StudentGender,
                        StudentPhoneNumberUIModel=x.StudentPhoneNumber,
                        CityUIModel=x.City,
                        EducationUIModel=x.Education,
                        PersonalrofileUIModel=x.Personalrofile,
                    })
                    .FirstOrDefault(m => m.StudentIdUIModel == studentid);


                if(student==null)
                {
                    throw new Exception("要编辑的学员不存在，请您刷新列表后在操作！");
                }

                return student;
            }
        }
        #endregion


        #region 学员----更新

        public int UpdateStudent(Student student)
        {
            using (var db = new EFContext())
            {

                int result = 0;
                //首先检查信息是否存在
                Student students = db.Students.Find(student.StudentId);

                if(students==null)
                {
                    throw new Exception("编辑的学员信息不存在，请您刷新列表后再操作！");
                }
                else
                {
                    //更新学生的
                    students.StudentName = student.StudentName;
                    students.StudentGender = student.StudentGender;
                    students.StudentPhoneNumber = student.StudentPhoneNumber;
                    students.City = student.City;
                    students.Education = student.Education;
                    students.Personalrofile = student.Personalrofile;


                    //更新用户
                    User user = db.SysUsers.Find(students.UserId);

                    if(user==null)
                    {
                        throw new Exception("数据出现异常");
                    }
                    else
                    {
                        user.UserNickName = student.StudentName;
                        user.UserPhoneNumber = student.StudentPhoneNumber;
                        user.UserGender = student.StudentGender;
                    }

                    using (var tran = db.Database.BeginTransaction())
                    {
                        db.Students.Add(students);
                        db.Entry(students).State = System.Data.Entity.EntityState.Modified;

                        db.SysUsers.Add(user);
                        db.Entry(user).State = System.Data.Entity.EntityState.Modified;

                        result = db.SaveChanges();

                         tran.Commit();

                        if(result!=2)
                        {
                            tran.Rollback();
                        }

                    }
                }
                return result;
            }
        }
        #endregion

        #region 学员-----删除
        public int Delete(int studentid)
        {
            using (var db = new EFContext())
            {
                int result = 0;
                //检查学员是否存在
                Student student = db.Students.Find(studentid);

                if(student==null)
                {
                    throw new Exception("要删除的学员不存在，请您刷新列表后在操作！");
                }
                else
                {
                    //查询出用户信息
                    User user = db.SysUsers.Find(student.UserId);

                    if(user==null)
                    {
                        throw new Exception("数据异常");
                    }
                    else
                    {
                        user.IsDelete = 1;
                    }

                    using (var tran = db.Database.BeginTransaction())
                    {
                        db.Students.Remove(student);

                        db.SysUsers.Add(user);
                        db.Entry(user).State = System.Data.Entity.EntityState.Modified;

                        result = db.SaveChanges();

                        tran.Commit();

                        if(result!=2)
                        {
                            tran.Rollback();
                        }
                    }
                }
                return result;
            }
        }
        #endregion
    }
}
