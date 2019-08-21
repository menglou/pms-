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
  public  class ForProjectService
    {
        public List<ForProjectUIModel> GetProjectList()
        {
            using (var db = new EFContext())
            {
                List<ProjectSign> lists = db.ProjectSign.Where(x=>x.ProjectStatus==2).ToList();//获取所有的正在学习中的项目报名表


                var projectlist = from list in db.Projects
                                  join list2 in db.Lecturers on list.LecturerId equals list2.LecturerId
                                  
                                  select new ForProjectUIModel()
                                  {
                                      ProjectIdUIModel = list.ProjectId,
                                      ProjectNameUIModel = list.ProjectName,
                                      ProjectFrameworkUIModel = list.ProjectFramework,
                                      LecturerNameUIModel = list2.LecturerName,
                                      ProdifficultyUIModel = list.Prodifficulty,
                                      
                                  };


                List<ForProjectUIModel> forprolist = new List<ForProjectUIModel>();
                forprolist.AddRange(projectlist);//获取所有的项目列表


                foreach (var item in forprolist)
                {
                    List<ProjectSign> prosinlist= lists.Where(x => x.ProjectId == item.ProjectIdUIModel).ToList();

                    item.AttentProCount = prosinlist.Count;
                }

                return forprolist;
            }
        }
    }
}
