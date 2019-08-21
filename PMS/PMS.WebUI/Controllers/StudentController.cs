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
    public class StudentController : Controller
    {
        private StudentService studentservice = new StudentService();

        // GET: Student
        public ActionResult Index()
        {
                return View();
        }

        /// <summary>
        /// 判断权限
        /// </summary>
        /// <param name="actionid">动作id (0代表新增，1新增保存，2代表修改，3代表修改保存，4代表删除)</param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult IsHaveAuthor(int actionid)
        {
            ICollection<Author> authorlist = (ICollection<Author>)Session["authorlist"];
            Author author = new Author();
            switch (actionid)
            {
                case 0:
                    author = authorlist.FirstOrDefault(x => x.ControllerName == "Student" && x.ActionName == "Create");
                    break;
                case 1:
                    author = authorlist.FirstOrDefault(x => x.ControllerName == "Student" && x.ActionName == "AddStudent");
                    break;
                case 2:
                    author = authorlist.FirstOrDefault(x => x.ControllerName == "Student" && x.ActionName == "Update");
                    break;
                case 3:
                    author = authorlist.FirstOrDefault(x => x.ControllerName == "Student" && x.ActionName == "UpdateStudent");
                    break;
                case 4:
                    author = authorlist.FirstOrDefault(x => x.ControllerName == "Student" && x.ActionName == "Delete");
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


        public ActionResult Update(int studentid)
        {
            ViewBag.studentid = studentid;
            return View();
        }

        [AllowAnonymous]
        public ActionResult UpdateInit(int studentid)
        {
            JsonResultData<StudentUIModel> resultdata = new JsonResultData<StudentUIModel>();

            try
            {
                StudentUIModel student = studentservice.GetStudentById(studentid);

                if(student==null)
                {
                    resultdata.Code = 0;
                    resultdata.Data = student;
                }
                else
                {
                    resultdata.Code = 1;
                    resultdata.Data = student;
                }

            }
            catch (Exception ex)
            {
                resultdata.Msg = ex.Message;
            }

            return Json(resultdata,JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateStudent(Student student)
        {
            ICollection<Author> authorlist = (ICollection<Author>)Session["authorlist"];

            Author author = authorlist.FirstOrDefault(x => x.ControllerName == "Student" && x.ActionName == "UpdateStudent");
            JsonResultData<string> resultdata = new JsonResultData<string>();

            if(author!=null)
            {
                try
                {
                    int result = studentservice.UpdateStudent(student);

                    if (result != 2)
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

            return Json(resultdata, JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult Delete(int studentid)
        {
            JsonResultData<string> resultdata = new JsonResultData<string>();

            try
            {
                int result = studentservice.Delete(studentid);

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

        public ActionResult AddStudent(Student student)
        {
            ICollection<Author> authorlist = (ICollection<Author>)Session["authorlist"];

            Author author = authorlist.FirstOrDefault(x => x.ControllerName == "Student" && x.ActionName == "AddStudent");

            JsonResultData<string> resultdata = new JsonResultData<string>();

            if(author!=null)
            {
                try
                {
                    int result = studentservice.AddStudent(student);

                    if (result != 2)
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
        [AllowAnonymous]
        public ActionResult GetStudentList()
        {
            DataTableJsonResult<StudentUIModel> tablestu = studentservice.GetStudentList();

            return Json(tablestu,JsonRequestBehavior.AllowGet);
        }
    }
}