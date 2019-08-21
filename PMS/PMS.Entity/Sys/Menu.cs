using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PMS.Entity
{
    [Table("SysMenu")]
  public  class Menu
    {

        /// <summary>
        /// 菜单Id
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MenuId { get; set; }

        /// <summary>
        /// 菜单名称
        /// </summary>
        [Required]
        [StringLength(100)]
        public string MenuName { get; set; }

        /// <summary>
        /// 菜单Guid
        /// </summary>
        public Guid MenuGuid { get; set; }

        /// <summary>
        /// 菜单图标
        /// </summary>
        [StringLength(100)]
        public string MenuIcon { get; set; }

        /// <summary>
        /// 菜单描述
        /// </summary>
        [StringLength(200)]
        [Required]
        public string MenuDes { get; set; }

        /// <summary>
        /// 全路径
        /// </summary>
        [StringLength(100)]
        public string MenuPath { get; set; }

        /// <summary>
        /// 菜单链接
        /// </summary>
        [StringLength(100)]
        public string MenuLink { get; set; }

        /// <summary>
        /// 排序编码
        /// </summary>
        [StringLength(100)]
        public string SortCode { get; set; }

        /// <summary>
        /// 父Guid
        /// </summary>
        public Guid? ParentGuid { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
    }
}
