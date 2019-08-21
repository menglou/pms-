using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PMS.Entity;
using PMS.Logic;
using PMS.UIModel;
using PMS.Util;

namespace PMS.WebUI.Controllers
{
    [RequestAuthorize]
    public class LecturerController : Controller
    {
        private LectureService lectureservice = new LectureService();

        // GET: Lecturer
        public ActionResult Index()
        {

           
                return View();
            
        }

        /// <summary>
        /// 判断权限
        /// </summary>
        /// <param name="actionid">动作id (0代表新增，1新增保存，2代表修改，3代表修改保存，4代表删除,5代表上传照片)</param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult IsHaveAuthor(int actionid)
        {
            ICollection<Author> authorlist = (ICollection<Author>)Session["authorlist"];
            Author author = new Author();
            switch (actionid)
            {
                case 0:
                    author = authorlist.FirstOrDefault(x => x.ControllerName == "Lecturer" && x.ActionName == "Create");
                    break;
                case 1:
                    author = authorlist.FirstOrDefault(x => x.ControllerName == "Lecturer" && x.ActionName == "AddLecture");
                    break;
                case 2:
                    author = authorlist.FirstOrDefault(x => x.ControllerName == "Lecturer" && x.ActionName == "Update");
                    break;
                case 3:
                    author = authorlist.FirstOrDefault(x => x.ControllerName == "Lecturer" && x.ActionName == "UpdateLecture");
                    break;
                case 4:
                    author = authorlist.FirstOrDefault(x => x.ControllerName == "Lecturer" && x.ActionName == "DeleteLecturer");
                    break;
                case 5:
                    author = authorlist.FirstOrDefault(x => x.ControllerName == "Lecturer" && x.ActionName == "UploadImage");
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

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Update(int lecturerid)
        {
            ViewBag.lecturerid = lecturerid;
            return View();
        }



        [AllowAnonymous]
        /// <summary>
        /// 编辑初始化
        /// </summary>
        /// <returns></returns>
        public ActionResult UpdateInit( int lecturerid)
        {
            JsonResultData<LectureUIModel> resultdata = new JsonResultData<LectureUIModel>();

            try
            {
                LectureUIModel lecture = lectureservice.GetLectureById(lecturerid);


                if(lecture==null)
                {
                    resultdata.Code = 0;
                    resultdata.Data = lecture;
                }
                else
                {
                    resultdata.Code = 1;
                    resultdata.Data = lecture;
                }
            }
            catch (Exception ex)
            {
                resultdata.Msg = ex.Message;
            }

            return Json(resultdata,JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 更新导师信息
        /// </summary>
        /// <returns></returns>
        public ActionResult UpdateLecture( Lecturer lecture)
        {
            ICollection<Author> authorlist = (ICollection<Author>)Session["authorlist"];

            Author author = authorlist.FirstOrDefault(x => x.ControllerName == "Lecturer" && x.ActionName == "UpdateLecture");
            JsonResultData<string> resultdata = new JsonResultData<string>();

            if(author!=null)
            {
                try
                {
                    int result = lectureservice.UpdateLecturer(lecture);


                    if (result == 0)
                    {
                        resultdata.Code = 0;
                        resultdata.Data = "编辑失败";
                    }
                    else
                    {
                        resultdata.Code = 1;
                        resultdata.Data = "编辑成功";
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

        public ActionResult AddLecture(Lecturer lecture)
        {
            ICollection<Author> authorlist = (ICollection<Author>)Session["authorlist"];

            Author author = authorlist.FirstOrDefault(x => x.ControllerName == "Lecturer" && x.ActionName == "AddLecture");

            JsonResultData<string> resultdata = new JsonResultData<string>();

            if(author!=null)
            {
                try
                {
                    int result = lectureservice.AddLecturer(lecture);

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

        public ActionResult DeleteLecturer(int lectureid)
        {
            JsonResultData<string> resultdata = new JsonResultData<string>();

            try
            {
                int result = lectureservice.Delete(lectureid);

                if (result != 2)
                {

                    resultdata.Code = 0;
                    resultdata.Data = "删除失败";
                }
                else
                {
                    resultdata.Code = 1;
                    resultdata.Data = "删除成功";
                }
            }
            catch (Exception ex)
            {
                resultdata.Msg = ex.Message;
            }
            return Json(resultdata, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        /// <summary>
        /// 获取导师列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetLectureList()
        {
            DataTableJsonResult<LectureUIModel> tableresult = lectureservice.GetLectureList();



            return Json(tableresult,JsonRequestBehavior.AllowGet);
        }
        [AllowAnonymous]
        public ActionResult UploadImage(int lecturerid)
        {
            ViewBag.lecturerid = lecturerid;
            return View();
        }
        [AllowAnonymous]
        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="lecturerid"></param>
        /// <returns></returns>
        public ActionResult UploadImageInService(int lecturerid)
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
                filePathName = lecturerid + ".jpg";

                //获取服务器的文件夹路径
                var tmpName = Server.MapPath("~/Image/LecturerImage/");

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