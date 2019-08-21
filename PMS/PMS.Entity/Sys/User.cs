using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PMS.Entity
{
    [Table("SysUser")]
  public  class User
    {
        /// <summary>
        /// 用户id
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        
        public int UserId { get; set; }


        /// <summary>
        /// 关联Guid
        /// </summary>
        public Guid RelationGuid { get; set; }

        /// <summary>
        /// 用户昵称
        /// </summary>
        [Required]
        [StringLength(50)]
        public string UserNickName { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        [Required]
        [StringLength(50)]
        public string UserPhoneNumber { get; set; }


        /// <summary>
        /// 密码
        /// </summary>
        [Required]
        [StringLength(50)]
        public string UserPwd { get; set; }


        /// <summary>
        /// 性别
        /// </summary>
        [Required]
        public int UserGender { get; set; }

        /// <summary>
        /// 是否禁用
        /// </summary>
        [Required]
        public int IsDisable { get; set; }//0代表禁用，1代表没有

        [Required]
        /// <summary>
        /// 是否删除
        /// </summary>
        public int IsDelete { get; set; }
        
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


        public int? RoleId { get; set; }

        public virtual Role Roles { get; set; }

        
       
    }
}
