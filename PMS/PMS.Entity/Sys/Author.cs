using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PMS.Entity
{
    [Table("SysAuthor")]
  public  class Author
    {
        /// <summary>
        /// 与角色建立多对多的关系
        /// </summary>
        public Author()
        {
            Roles = new HashSet<Role>();
        }

        /// <summary>
        /// 权限id
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AuthorId { get; set; }

        /// <summary>
        /// 权限名称
        /// </summary>
        [Required]
        [StringLength(100)]
        public string AuthorName { get; set; }


        /// <summary>
        /// 权限描述
        /// </summary>
        [Required]
        [StringLength(100)]
        public string AuthorDes { get; set; }

        /// <summary>
        /// 全路径
        /// </summary>
      
        [StringLength(100)]
        public string AuthorPath { get; set; }

        /// <summary>
        /// 控制器
        /// </summary>
        [StringLength(100)]
        public string ControllerName { get; set; }

        /// <summary>
        /// 动作方法
        /// </summary>
        [StringLength(100)]
        public string ActionName { get; set; }

        /// <summary>
        /// 权限类型
        /// </summary>
        [StringLength(100)]
        public string AuthorType { get; set; }

        /// <summary>
        /// 排序编码
        /// </summary>
        [StringLength(100)]
        public string AuthorSortCode { get; set; }

        /// <summary>
        /// 权限GUID
        /// </summary>
        public Guid AuthorGuid { get; set; }


        /// <summary>
        /// 父GUID
        /// </summary>
        public Guid? ParentGuid { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Required]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        [Required]
        public DateTime UpdateTime { get; set; }


        public virtual ICollection<Role> Roles { get; set; }

    }
}
