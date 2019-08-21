using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using PMS.Entity;
using PMS.Logic;
using PMS.UIModel;
using PMS.Util;
using System.IO;
using PMS.UIModel.Forground;
using System.Web.Security;
using System.Web.Script.Serialization;

namespace PMS.WebUI.Controllers.Pro
{
    [Authorize]
    public class ProjectController : Controller
    {
        private ProjectService projectservice = new ProjectService();

        // GET: Project
        public ActionResult Index()
        {
            ICollection<Author> authorlist = (ICollection<Author>)Session["authorlist"];

            Author author = authorlist.FirstOrDefault(x => x.ControllerName == "Project" && x.ActionName == "Index");

            if(author!=null)
            {
                return View();
            }
            else
            {
                return Redirect("NoAuthority.html");
            }
          
        }

        /// <summary>
        /// 判断权限
        /// </summary>
        /// <param name="actionid">动作id (0代表新增，1新增保存，2代表修改，3代表修改保存，4代表删除，5代表上传图片)</param>
        /// <returns></returns>
        public ActionResult IsHaveAuthor(int actionid)
        {
            ICollection<Author> authorlist = (ICollection<Author>)Session["authorlist"];
            Author author = new Author();
            switch (actionid)
            {
                case 0:
                    author = authorlist.FirstOrDefault(x => x.ControllerName == "Project" && x.ActionName == "Create");
                    break;
                case 1:
                    author = authorlist.FirstOrDefault(x => x.ControllerName == "Project" && x.ActionName == "AddProjectFor");
                    break;
                case 2:
                    author = authorlist.FirstOrDefault(x => x.ControllerName == "Project" && x.ActionName == "Update");
                    break;
                case 3:
                    author = authorlist.FirstOrDefault(x => x.ControllerName == "Project" && x.ActionName == "UpdateProject");
                    break;
                case 4:
                    author = authorlist.FirstOrDefault(x => x.ControllerName == "Project" && x.ActionName == "Delete");
                    break;
                case 5:
                    author = authorlist.FirstOrDefault(x => x.ControllerName == "Project" && x.ActionName == "UploadImage");
                    break;

            }
            JsonResultData<string> result = new JsonResultData<string>();

            if (author == null)
            {
                result.Code = 0;
                result.Data = "没有权限";
            }
            else
            {
                result.Code = 1;
                result.Data = "有权限";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Preview(int projectid)
        {
            User user = (User)Session["currentuserinfo"];

           

            if(user==null)
            {
                return RedirectToAction("Index", "AdminLogin");
            }
            else
            {
                //查询学员信息
                Student stu = new UserService().GetStudetInfo(user.UserId);

                ForProjectUIModel pro = projectservice.GetProjectinfoById(projectid);

                ViewBag.users = user;

                if(stu!=null)
                {
                    ViewBag.stuid = stu.StudentId;
                }
            
                return View(pro);
            }
           
        }

        public ActionResult Create()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult AddForProject()
        {
            //JavaScriptSerializer js = new JavaScriptSerializer();

            ///string formInfor = CheckResponse.GetResponse("formdata");
            //Project project = js.Deserialize<Project>(HttpContext.Request.Params["from1"]);

            string ProjectName = Request.Params["ProjectName"].ToString();
            string ProjectFramework = Request.Params["ProjectFramework"].ToString();
            string Prodifficulty = Request.Params["Prodifficulty"].ToString();
            int StudyTime =Convert.ToInt32( Request.Params["StudyTime"]);
            int LecturerId = Convert.ToInt32( Request.Params["LecturerId"]);
            string PalyAddress = Request.Params["PalyAddress"].ToString();
            string ProjectIntroduction = Request.Params["ProjectIntroduction"].ToString();

            Project project = new Project()
            {
                ProjectName= ProjectName,
                ProjectFramework= ProjectFramework,
                Prodifficulty= Prodifficulty,
                StudyTime= StudyTime,
                LecturerId= LecturerId,
                PalyAddress= PalyAddress,
                ProjectIntroduction= ProjectIntroduction,
            };

            ICollection<Author> authorlist = (ICollection<Author>)Session["authorlist"];

            Author author = authorlist.FirstOrDefault(x => x.ControllerName == "Project" && x.ActionName == "AddProjectFor");

            JsonResultData<string> resultdata = new JsonResultData<string>();

            if(author!=null)
            {
                try
                {
                    int result = projectservice.AddProject(project);

                    if (result == 0)
                    {
                        resultdata.Code = 0;
                        resultdata.Data = "添加失败";
                    }
                    else
                    {
                        resultdata.Code = 1;
                        resultdata.Data = "添加成功";
                    }
                }
                catch (Exception ex)
                {
                    resultdata.Msg = ex.Message;
                }
            }
            else
            {
                resultdata.Code = 2;
                resultdata.Data = "没有权限";
            }
            return Json(resultdata,JsonRequestBehavior.AllowGet);
        }



        public ActionResult Update(int projectid)
        {
            ViewBag.projectid = projectid;
            return View();
        }

        /// <summary>
        /// 初始化项目信息
        /// </summary>
        /// <returns></returns>
        public ActionResult UpdateInit(int projectid)
        {
            JsonResultData<ProjectUIModel> resultdata = new JsonResultData<ProjectUIModel>();

            try
            {
                ProjectUIModel project = projectservice.GetProjectById(projectid);

                if(project==null)
                {
                    resultdata.Code = 0;
                    resultdata.Data = project;
                }
                else
                {
                    resultdata.Code = 1;
                    resultdata.Data = project;
                }
            }
            catch (Exception ex)
            {
                resultdata.Msg = ex.Message;
            }

            return Json(resultdata,JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 更新项目
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        public ActionResult UpdateProject(Project project)
        {
            ICollection<Author> authorlist = (ICollection<Author>)Session["authorlist"];

            Author author = authorlist.FirstOrDefault(x => x.ControllerName == "Project" && x.ActionName == "UpdateProject");

            JsonResultData<string> resultdata = new JsonResultData<string>();

            if(author!=null)
            {
                try
                {
                    int result = projectservice.UpdateProject(project);

                    if (result == 0)
                    {
                        resultdata.Code = 0;
                        resultdata.Data = "更新失败";
                    }
                    else
                    {
                        resultdata.Code = 1;
                        resultdata.Data = "更新成功";
                    }
                }
                catch (Exception ex)
                {
                    resultdata.Msg = ex.Message;
                }
            }
            else
            {
                resultdata.Code = 2;
                resultdata.Data = "没有权限";
            }
            return Json(resultdata,JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete(int projectid)
        {
            JsonResultData<string> resultdata = new JsonResultData<string>();

            try
            {
                int result = projectservice.Delete(projectid);
                
                if(result==0)
                {
                    resultdata.Code = 0;
                    resultdata.Data = "删除失败";
                }
                else
                {
                    resultdata.Code = 0;
                    resultdata.Data = "删除成功";
                }

            }
            catch (Exception ex)
            {
                resultdata.Msg = ex.Message;
            }

            return Json(resultdata,JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取项目列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetProjectrList()
        {
            DataTableJsonResult<ProjectUIModel> tablepro = projectservice.GetProjectList();

            return Json(tablepro,JsonRequestBehavior.AllowGet);
        }

        /// <summary>    
        /// 初始化导师列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetLecturerList()
        {
            List<LectureUIModel> leclist = projectservice.GetLecturerList();

            return Json(leclist,JsonRequestBehavior.AllowGet);
        }

        public ActionResult ApplayPro(int projectid,int studentid)
        {
            JsonResultData<string> resultdata = new JsonResultData<string>();

            ProjectSign pro = new ProjectSign()
            {
                SignTime = DateTime.Now,
                ProjectStatus = 0, //0 表示未审核，1表示审核未通过，2进行中 3已完成
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now,
                ProjectId = projectid,

                //学生
                StudentId= studentid,

                //评分

                //导师评价


                //git地址
            };

            try
            {
                int result = projectservice.ApplayPro(pro);

                if(result==0)
                {
                    resultdata.Code = 0;
                    resultdata.Data = "申请失败";
                }
                else if(result == 1)
                {
                    resultdata.Code = 1;
                    resultdata.Data = "申请成功";
                }
                else
                {
                    resultdata.Code = 2;
                    resultdata.Data = "您已申请该项目";
                }

            }
            catch (Exception ex)
            {
                resultdata.Msg = ex.Message;
            }

            return Json(resultdata,JsonRequestBehavior.AllowGet);
        }


        public ActionResult  UploadImage(int projectid)
        {
            ViewBag.projectid = projectid;
            return View();
        }

        public ActionResult UploadImageInService(int projectid)
        {
            try
            {
                var file = Request.Files[0];//获取选中的文件
                var filecombin = file.FileName.Split('.');
                if (file == null || String.IsNullOrEmpty(file.FileName) || file.ContentLength == 0 || filecombin.Length < 2)
                {
                    return Json(new
                    {
                        code = 0,
                        src = "",
                        name = "",
                        msg = "上传出错 请检查文件名 或 文件内容"
                    });
                }

                //定义本地路径位置
               //获取文件名称
                string filePathName = file.FileName;
                
                //更改文件名
                filePathName = projectid + ".jpg";

                //获取服务器的文件夹路径
                var tmpName = Server.MapPath("~/Image/ProImage/");

                //上传的完全路径（包括图片名称）
                filePathName = tmpName + filePathName;
                //先判断文件夹下是否有相同名字的图片
                if (!System.IO.File.Exists(tmpName + filePathName))
                {
                    file.SaveAs(filePathName);
                }
                else
                {
                    System.IO.File.Delete(tmpName + filePathName);
                    file.SaveAs(filePathName);
                }
                return Json(new
                {
                    code = 1,
                    src = filePathName,
                    name = "",
                    msg = "上传成功"
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    code = 0,
                    src = "",
                    name = "",
                    msg = "上传出错"
                });
            }
        }
    }
}