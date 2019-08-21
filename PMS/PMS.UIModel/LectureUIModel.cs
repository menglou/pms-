using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.UIModel
{
   public class LectureUIModel
    {
        /// <summary>
        /// 讲师Id
        /// </summary>
     
        public int LecturerIdUIModel { get; set; }

        /// <summary>
        /// 讲师姓名
        /// </summary>
    
        public string LecturerNameUIModel { get; set; }

        /// <summary>
        /// 讲师手机号
        /// </summary>
      
        public string LecturerPhoneNumberUIModel { get; set; }

        /// <summary>
        /// 讲师性别
        /// </summary>
        public int LecturerGenderUIModel { get; set; }


        public string LecturerGenderUIModelstr
        {
            get
            {
                if (LecturerGenderUIModel == 0)
                {
                    return "男";
                }
                else
                {
                    return "女";
                }
              
            }
        }

        /// <summary>
        /// 讲师简介
        /// </summary>

        public string IntroductionUIModel { get; set; }



        public DateTime CreateTimeUIModel { get; set; }


        public string CreateTimeUIModelstr
        {
            get { return CreateTimeUIModel.ToString(); }
        }
    }
}
