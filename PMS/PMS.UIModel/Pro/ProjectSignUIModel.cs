using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.UIModel
{
   public class ProjectSignUIModel
    {
        /// <summary>
        /// 项目报名Id
        /// </summary>
        public int ProjectSignIdUIModel { get; set; }

        /// <summary>
        /// 报名时间
        /// </summary>
     
        public DateTime SignTimeUIModel { get; set; }


        public string SignTimeUIModelstr
        {
            get
            {
                return SignTimeUIModel.ToString();
            }
        }

        /// <summary>
        /// 项目状态
        /// </summary>
        public int ProjectStatusUIModel { get; set; }  //0 表示未审核    1表示审核未通过  2表示进行中



        public string ProjectStatusUIModelstr
        {
            get
            {
                if(ProjectStatusUIModel==0)
                {
                    return "未审核";
                }
                else if(ProjectStatusUIModel == 1)
                {
                    return "审核未通过";
                }
                else
                {
                    return "进行中";
                }
            }
        }
        /// <summary>
        /// Git地址
        /// </summary>

        public string GitAddressUIModel { get; set; }

        /// <summary>
        /// 项目评分
        /// </summary>
      
        public int ProjectScoreUIModel { get; set; }

        /// <summary>
        /// 导师评价
        /// </summary>
        public string ReviewTeacherUIModel { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectNameUIModel { get; set; }

        /// <summary>
        /// 学生姓名
        /// </summary>
        public string StudentNameUIModel { get; set; }

        /// <summary>
        /// 学员Id
        /// </summary>
        public int StudentIdUIModel { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
      
        public DateTime CreateTimeUIModel { get; set; }
    }
}
