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
   public class ProjectSignService
    {
        #region 项目报名表-----列表
        public DataTableJsonResult<ProjectSignUIModel> GetProjecSigntrList()
        {
            using (var db = new EFContext())
            {
                var projectsignlist = from list in db.ProjectSign
                                      join list2 in db.Projects on list.ProjectId equals list2.ProjectId
                                      join list3 in db.Students on list.StudentId equals list3.StudentId
                                      select new ProjectSignUIModel()
                                      {
                                          SignTimeUIModel=list.SignTime,
                                          ProjectStatusUIModel=list.ProjectStatus,
                                          ProjectSignIdUIModel=list.ProjectSignId,
                                          GitAddressUIModel=list.GitAddress,
                                          ProjectScoreUIModel=list.ProjectScore,
                                          ProjectNameUIModel=list2.ProjectName,
                                          StudentIdUIModel=list3.StudentId,
                                          StudentNameUIModel =list3.StudentName
                                      };

                DataTableJsonResult<ProjectSignUIModel> tableprosign = new DataTableJsonResult<ProjectSignUIModel>();

                tableprosign.code = 0;
                tableprosign.count = projectsignlist.Count();
                tableprosign.data = projectsignlist.ToList();

                return tableprosign;
            }
        }
        #endregion
    }
}
