using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.UIModel.Forground
{
   public class ForProjectUIModel
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
        /// 老师介绍
        /// </summary>
        public string LecturerIntroduction { get; set; }

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

        /// <summary>
        /// 参与人数
        /// </summary>
        public int AttentProCount { get; set; }

        /// <summary>
        /// 累计完成人数
        /// </summary>
        public int AlreadyCount { get; set; }

        /// <summary>
        /// 优秀评分人数
        /// </summary>
        public int GoodCount { get; set; }

        /// <summary>
        /// 代码量
        /// </summary>
        public int CodeCount { get; set; }
    }
}
