using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMS.Entity;
using PMS.UIModel;
using PMS.Util;
using PMS.UIModel.Forground;

namespace PMS.Logic
{
    public class ProjectService
    {
        #region 项目------列表
        public DataTableJsonResult<ProjectUIModel> GetProjectList()
        {
            using (var db = new EFContext())
            {

                var prolists = from list in db.Projects
                               join list2 in db.Lecturers on list.LecturerId equals list2.LecturerId
                               select new ProjectUIModel()
                               {
                                   ProjectIdUIModel = list.ProjectId,
                                   ProjectNameUIModel = list.ProjectName,
                                   ProjectFrameworkUIModel = list.ProjectFramework,
                                   ProdifficultyUIModel = list.Prodifficulty,
                                   StudyTimeUIModel = list.StudyTime,
                                   LecturerNameUIModel = list2.LecturerName,
                                   PalyAddressUIModel = list.PalyAddress,
                                   CreateTimeUIModel = list.CreateTime,
                               };

                //List <ProjectUIModel> prolist = db.Projects.Join(db.Lecturers,)
                //    .Select(x => new ProjectUIModel()
                //    {
                //        ProjectIdUIModel = x.ProjectId,
                //        ProjectNameUIModel = x.ProjectName,
                //        ProjectFrameworkUIModel = x.ProjectFramework,
                //        ProdifficultyUIModel = x.Prodifficulty,
                //        StudyTimeUIModel = x.StudyTime,
                //        LecturerName = x.GuideTeacher,
                //        PalyAddressUIModel = x.PalyAddress,
                //        CreateTimeUIModel = x.CreateTime,

                //    })
                //    .ToList();

                DataTableJsonResult<ProjectUIModel> tablepro = new DataTableJsonResult<ProjectUIModel>();

                tablepro.code = 0;
                tablepro.count = prolists.Count();
                tablepro.data = prolists.ToList();

                return tablepro;

            }
        }
        #endregion

        #region 项目-----导师列表
        public List<LectureUIModel> GetLecturerList()
        {
            using (var db = new EFContext())
            {
                List<LectureUIModel> leclist = db.Lecturers
                    .Select(x => new LectureUIModel()
                    {
                        LecturerIdUIModel = x.LecturerId,
                        LecturerNameUIModel = x.LecturerName
                    })
                    .ToList();


                return leclist;
            }
        }
        #endregion

        #region 项目----添加
        public int AddProject(Project project)
        {
            using (var db = new EFContext())
            {
                int result = 0;

                project.CreateTime = DateTime.Now;
                project.UpdateTime = DateTime.Now;


                db.Projects.Add(project);

                result = db.SaveChanges();

                return result;
            }
        }
        #endregion


        #region 项目----根据项目Id查询项目信息
        public ProjectUIModel GetProjectById(int projectid)
        {
            using (var db = new EFContext())
            {
                //首先要查询项目是否存在

                ProjectUIModel peject = db.Projects
                    .Select(x => new ProjectUIModel()
                    {
                        ProjectIdUIModel=x.ProjectId,
                        ProjectNameUIModel = x.ProjectName,
                        ProjectFrameworkUIModel = x.ProjectFramework,
                        ProdifficultyUIModel = x.Prodifficulty,
                        LecturerIdUIModel = x.LecturerId,
                        PalyAddressUIModel = x.PalyAddress,
                        ProjectIntroductionUIModel = x.ProjectIntroduction,
                        StudyTimeUIModel=x.StudyTime,

                    })
                    .FirstOrDefault(m => m.ProjectIdUIModel == projectid);


                if(peject==null)
                {
                    throw new Exception("要编辑项目不存在，请您刷新列表后在操作");
                }
                return peject;
            }
        }
        #endregion


        #region 项目-----更新
        public int UpdateProject(Project project)
        {
            using (var db = new EFContext())
            {
                int result = 0;
                //更新之前检查项目是否存在
                Project projects = db.Projects.Find(project.ProjectId);

                if(projects==null)
                {
                    throw new Exception("要编辑项目不存在，请您刷新列表后在操作");
                }
                else
                {
                    projects.ProjectName = project.ProjectName;
                    projects.ProjectFramework = project.ProjectFramework;
                    projects.Prodifficulty = project.Prodifficulty;
                    projects.StudyTime = project.StudyTime;
                    projects.PalyAddress = project.PalyAddress;
                    projects.ProjectIntroduction = project.ProjectIntroduction;
                    projects.LecturerId = project.LecturerId;
                    projects.UpdateTime = DateTime.Now;


                    db.Projects.Add(projects);
                    db.Entry(projects).State = System.Data.Entity.EntityState.Modified;

                    result = db.SaveChanges();
                }
                return result;
            }
        }
        #endregion


        #region 项目-----删除
        public int Delete(int projectid)
        {
            using (var db = new EFContext())
            {
                int result = 0;

                //删除之前检查是否存在
                Project project = db.Projects.Find(projectid);

                if(project==null)
                {
                    throw new Exception("要删除的项目不存在，请您刷新列表后在操作");
                }
                else
                {
                    db.Projects.Remove(project);
                    result = db.SaveChanges();
                }

                return result;
            }
        }
        #endregion


        #region 项目-----详情
        public ForProjectUIModel GetProjectinfoById(int projectid)
        {
            using (var db = new EFContext())
            {
                List<ProjectSign> lists = db.ProjectSign.Where(x => x.ProjectStatus == 2).ToList();//获取所有的正在学习中的项目报名表
                List<ProjectSign> alderylists = db.ProjectSign.Where(x => x.ProjectStatus == 3).ToList();//获取所有的累计完成的项目报名表

                ForProjectUIModel project = (from list in db.Projects
                               join list2 in db.Lecturers on list.LecturerId equals list2.LecturerId

                               select new ForProjectUIModel()
                               {
                                   ProjectIdUIModel = list.ProjectId,//项目id
                                   ProjectNameUIModel = list.ProjectName,//项目名称
                                   ProjectFrameworkUIModel = list.ProjectFramework,//项目框架
                                   LecturerNameUIModel = list2.LecturerName,//老师名称
                                   ProdifficultyUIModel = list.Prodifficulty,//学习难度
                                   LecturerIdUIModel = list2.LecturerId,
                                   LecturerIntroduction = list2.Introduction,
                                   ProjectIntroductionUIModel = list.ProjectIntroduction,
                                   StudyTimeUIModel=list.StudyTime,

                               }).FirstOrDefault(x => x.ProjectIdUIModel == projectid);


                List<ProjectSign> prosinlist = lists.Where(x => x.ProjectId == projectid).ToList();//正在学习的

                List<ProjectSign> prosinlistaldery = alderylists.Where(x => x.ProjectId == projectid).ToList();//累计完成的

                //优秀评分
                List<ProjectSign> goodlists = db.ProjectSign.Where(x => x.ProjectId == projectid&&x.ProjectScore>=80).ToList();
               



                project.AlreadyCount = prosinlist.Count;//正在学习的
                project.AlreadyCount = prosinlistaldery.Count;//累计完成的
                project.GoodCount = goodlists.Count;//优秀评分的
                //ForProjectUIModel pro = new ForProjectUIModel();


                return project;
            }
        }
        #endregion

        #region 项目---报名项目
        public int ApplayPro(ProjectSign prosign)
        {
            using (var db = new EFContext())
            {
                int result = 0;

                //首先检查该学员有没有已经报过名该项目

                ProjectSign prosigns = db.ProjectSign.FirstOrDefault(x => x.ProjectId == prosign.ProjectId && x.StudentId == prosign.StudentId);

                if (prosigns != null&& prosigns.ProjectStatus!=3)
                {
                    result = 2;//代表已经报过名了
                    return result;
                }
                else 
                {
                    db.ProjectSign.Add(prosign);

                    result = db.SaveChanges();

                    return result;
                }
                
            }
        }
        #endregion




    }
}
