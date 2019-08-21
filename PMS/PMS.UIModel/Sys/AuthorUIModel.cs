using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.UIModel
{
   public class AuthorUIModel
    {

        public int AuthorIdUIModel { get; set; }

        
        public string AuthorNameUIModel { get; set; }


        public string AuthorTypeUIModel { get; set; }


        public string ControllerNameUIModel { get; set; }


        public string ActionNameUIModel { get; set; }

        public string AuthorPathUIModel { get; set; }


        public Guid AuthorGuidUIModel { get; set; }


        public Guid? ParentGuidUIModel { get; set; }


        public string AuthorDesUIModel { get; set; }

        /// <summary>
        /// 是否有子菜单
        /// </summary>
        public  bool IsHavChild { get; set; }


        public bool LAY_CHECKED { get; set; }
    }
}
