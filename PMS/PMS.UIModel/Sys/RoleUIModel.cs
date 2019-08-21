using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.UIModel
{
   public  class RoleUIModel
    {

        public int RoleIdUIModel { get; set; }


        public string RoleNameUIModel { get; set; }

        public string RolePathUIModel { get; set; }


        public Guid RoleGuidUIModel { get; set; }

        public string RoleOrderCodeUIModel { get; set; }

        public string RoleDesUIModel { get; set; }

        public Guid? ParentGuidUIModel { get; set; }


        public bool IsParent { get; set; }


        public bool IsHavChild { get; set; }

    }
}
