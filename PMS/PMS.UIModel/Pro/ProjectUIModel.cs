using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.UIModel
{
    public class ProjectUIModel
    {

        /// <summary>
        /// 项目Id
        /// </summary>
        public int ProjectIdUIModel { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectNameUIModel { get; set; }

        /// <summary>
        /// 项目框架
        /// </summary>
      
        public string ProjectFrameworkUIModel { get; set; }

        /// <summary>
        /// 项目难度
        /// </summary>
     
        public string ProdifficultyUIModel { get; set; }

        /// <summary>
        /// 学习周期
        /// </summary>
      
        public int StudyTimeUIModel { get; set; }

        /// <summary>
        /// 指导老师
        /// </summary>
      
        public string LecturerNameUIModel { get; set; }



        /// <summary>
        /// 指导老师
        /// </summary>

        public int LecturerIdUIModel { get; set; }

        /// <summary>
        /// 演示地址
        /// </summary>

        public string PalyAddressUIModel { get; set; }

        /// <summary>
        /// 项目介绍
        /// </summary>
      
        public string ProjectIntroductionUIModel { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
       
        public DateTime CreateTimeUIModel { get; set; }



    }
}
