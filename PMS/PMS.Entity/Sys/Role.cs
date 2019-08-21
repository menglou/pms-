using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PMS.Entity
{
    [Table("SysRole")]
   public class Role
    {
        
        /// <summary>
        /// 与权限表建立多对多的关系
        /// </summary>
        public Role()
        {
            Authors = new HashSet<Author>();
        }

        /// <summary>
        /// 角色id
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoleId { get; set; }


        /// <summary>
        /// 角色名称
        /// </summary>
        [StringLength(100)]
        [Required]
        public string RoleName { get; set; }


        /// <summary>
        /// 角色描述
        /// </summary>
        [StringLength(100)]
        [Required]
        public string RoleDes { get; set; }



        /// <summary>
        /// 全路径
        /// </summary>
        [StringLength(100)]
        public string RolePath { get; set; }


        /// <summary>
        /// 排序编码
        /// </summary>
        [StringLength(50)]
        public string RoleOrderCode { get; set; }

        /// <summary>
        /// 角色GUID
        /// </summary>
        public Guid RoleGuid { get; set; }
        
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


        public virtual ICollection<Author> Authors { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
