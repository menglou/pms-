using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.UIModel
{
  public  class MenUIModel
    {

        public MenUIModel()
        {
            ChildMenuList = new List<MenUIModel>();
        }


        /// <summary>
        /// 菜单Id
        /// </summary>
        public int MenuIdUIModel { get; set; }

        /// <summary>
        /// 菜单名称UI
        /// </summary>
        public string MenuNameUIModel { get; set; }

        /// <summary>
        /// 菜单图标UI
        /// </summary>
        public string MenuIconUIModel { get; set; }

        /// <summary>
        /// 菜单Guid UI
        /// </summary>
        public Guid MenuGuidUIModel { get; set; }

        /// <summary>
        /// 菜单排序 UI
        /// </summary>
        public string SortCodeUIModel { get; set; }

        /// <summary>
        /// 菜单链接 UI
        /// </summary>
        public string MenuLinkUIModel { get; set; }

        /// <summary>
        /// 全路径 UI
        /// </summary>
        public string MenuPathUIModel { get; set; }

        /// <summary>
        /// 父节点 UI
        /// </summary>
        public Guid? ParentGuidUIModel { get; set; }

        /// <summary>
        /// 子菜单列表
        /// </summary>
        public List<MenUIModel> ChildMenuList { get; set; }


        public bool IsParent { get; set; }


        public bool IsChild { get; set; }

    }
}
