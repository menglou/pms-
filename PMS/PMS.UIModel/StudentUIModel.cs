using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMS.Util;

namespace PMS.UIModel
{
   public class StudentUIModel
    {
       
        public int StudentIdUIModel { get; set; }


        public string StudentNameUIModel { get; set; }


       
        public string StudentPhoneNumberUIModel { get; set; }


        
        public int StudentGenderUIModel { get; set; }

       
        public string StudentGenderUIModelstr
        {
            get
            {
                if(StudentGenderUIModel==0)
                {
                    return "男";
                }
                else
                {
                    return "女";
                }
            }
        }

        public string CityUIModel { get; set; }


        public int EducationUIModel { get; set; }


        public string EducationUIModelstr
        {
            get
            {
                if(EducationUIModel==10)
                {
                    return "高中";
                }
                else if(EducationUIModel == 20)
                {
                    return "大专";
                }
                else if (EducationUIModel == 30)
                {
                    return "本科";
                }
                else if (EducationUIModel == 40)
                {
                    return "硕士";
                }
                else 
                {
                    return "博士";
                }
            }
        }



        public string PersonalrofileUIModel { get; set; }

      
        public DateTime CreateTimeUIModel { get; set; }


        public string CreateTimeUIModelstr
        {
            get { return CreateTimeUIModel.ToString(); }
        }
    }
}
