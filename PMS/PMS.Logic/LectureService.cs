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
   public class LectureService
    {

        #region 导师----列表
        public DataTableJsonResult<LectureUIModel> GetLectureList()
        {
            using (var db = new EFContext())
            {
                List<LectureUIModel> lecturelist = db.Lecturers.Select(x => new LectureUIModel()
                {
                     LecturerIdUIModel = x.LecturerId,
                    LecturerNameUIModel = x.LecturerName,
                    LecturerPhoneNumberUIModel = x.LecturerPhoneNumber,
                    LecturerGenderUIModel = x.LecturerGender,
                    IntroductionUIModel = x.Introduction,
                    CreateTimeUIModel = x.CreateTime,
                })
                .ToList();

                DataTableJsonResult<LectureUIModel> tableresult = new DataTableJsonResult<LectureUIModel>();

                tableresult.code = 0;
                tableresult.count = lecturelist.Count;
                tableresult.data = lecturelist;


                return tableresult;

            }
        }
        #endregion

        #region 导师----添加
        public int AddLecturer(Lecturer lecturer)
        {
            using (var db = new EFContext())
            {
                  
                  int result = 0;

                User user = new User()
                {
                    UserNickName = lecturer.LecturerName,
                    UserPhoneNumber = lecturer.LecturerPhoneNumber,
                    UserGender = lecturer.LecturerGender,
                    UserPwd = Encrypt.MD5Encrypt64("123456"),
                    RelationGuid = Guid.NewGuid(),
                    IsDelete = 0,
                    IsDisable = 0,
                    CreateTime = DateTime.Now,
                    UpdateTime = DateTime.Now,
                    //角色默认为空不填
                };


                lecturer.CreateTime = DateTime.Now;
                lecturer.UpdateTime = DateTime.Now;
                lecturer.UserId = user.UserId;

                // lecturer.Users = user;


                //db.SysUsers.Add(user);

                using (var tran = db.Database.BeginTransaction())
                {
                    db.Lecturers.Add(lecturer);

                    db.SysUsers.Add(user);

                    result = db.SaveChanges();

                    tran.Commit();

                    if(result!=2)
                    {
                        tran.Rollback();
                    }
                }
                   
                //result = db.;
                 

                return result;
            }
        }
        #endregion

        #region 导师----根据Id查询导师信息
        public LectureUIModel GetLectureById(int lecturerid)
        {
            using (var db = new EFContext())
            {
                //要检查导师是否存在
                LectureUIModel lecture = db.Lecturers
                    .Select(x => new LectureUIModel()
                    {
                        LecturerIdUIModel=x.LecturerId,
                        LecturerGenderUIModel=x.LecturerGender,
                        LecturerNameUIModel=x.LecturerName,
                        LecturerPhoneNumberUIModel=x.LecturerPhoneNumber,
                        IntroductionUIModel=x.Introduction

                    })
                    .FirstOrDefault(m => m.LecturerIdUIModel == lecturerid);


                if(lecture==null)
                {
                    throw new Exception("要编辑的导师不存在，请您刷新列表后在操作！");
                }
                return lecture;
            }
        }
        #endregion


        #region 导师----更新
        public int UpdateLecturer(Lecturer lecture)
        {
            using (var db = new EFContext())
            {
                int result = 0;
                //更新时候检查信息是否存在

                Lecturer lectures = db.Lecturers.Find(lecture.LecturerId);

                if(lectures==null)
                {
                    throw new Exception("要编辑导师的信息不存在，请您刷新列表后在操作！");
                }
                else
                {
                    //更新导师
                    lectures.LecturerName = lecture.LecturerName;
                    lectures.LecturerGender = lecture.LecturerGender;
                    lectures.LecturerPhoneNumber = lecture.LecturerPhoneNumber;
                    lectures.Introduction = lecture.Introduction;
                     


                    //G更新用户的
                    User user= db.SysUsers.Find(lectures.UserId);

                    if(user!=null)
                    {
                        user.UserNickName= lecture.LecturerName;
                        user.UserGender = lecture.LecturerGender;
                        user.UserPhoneNumber = lecture.LecturerPhoneNumber;
                    }
                    else
                    {
                        throw new Exception("数据出现异常");
                    }

                    lectures.UpdateTime = DateTime.Now;

                    using (var tran = db.Database.BeginTransaction())
                    {
                        db.Lecturers.Add(lectures);
                        db.Entry(lectures).State = System.Data.Entity.EntityState.Modified;
                        db.SysUsers.Add(user);
                        db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                        
                        result = db.SaveChanges();

                        tran.Commit();

                        if (result!=2)
                        {
                            tran.Rollback();
                        }
                    }
                }
                return result;
            }

        }
        #endregion



        #region 导师------删除
        public int Delete(int lecturerid)
        {
            using (var db = new EFContext())
            {
                int result = 0;

                //删除之前检查信息是否存在

                Lecturer lecture = db.Lecturers.Find(lecturerid);

                if(lecture==null)
                {
                    throw new Exception("要删除的导师不存在，请您刷新列表后在操作！");
                }
                else
                {
                    //查询出用户信息

                    User user = db.SysUsers.Find(lecture.UserId);

                    user.IsDelete = 1;//1是  


                    using (var tran = db.Database.BeginTransaction())
                    {
                        db.Lecturers.Remove(lecture);

                        db.SysUsers.Add(user);
                        db.Entry(user).State = System.Data.Entity.EntityState.Modified;

                        result = db.SaveChanges();


                        tran.Commit();

                        if (result!=2)
                        {
                            tran.Rollback();
                        }
                    }
                    return result;
                }
            }
        }
        #endregion

    }
}
