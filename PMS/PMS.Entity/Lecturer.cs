using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PMS.Entity
{
    public class Lecturer
    {
        /// <summary>
        /// 讲师Id
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        
        public int LecturerId { get; set; }
        

        /// <summary>
        /// 讲师姓名
        /// </summary>
        [Required]
        [StringLength(50)]
        public string LecturerName { get; set; }

        /// <summary>
        /// 讲师手机号
        /// </summary>
        [Required]
        [StringLength(50)]
        public string LecturerPhoneNumber { get; set; }

        /// <summary>
        /// 讲师性别
        /// </summary>
        public int LecturerGender { get; set; }

        /// <summary>
        /// 讲师简介
        /// </summary>
        [StringLength(500)]
        [Required]
        public string Introduction { get; set; }

        
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

        

        // [ForeignKey("Users")]
        public int UserId { get; set; }

        public virtual User Users { get; set; }
        

        /// <summary>
        /// 一个老师可以有多个项目
        /// </summary>
        public virtual ICollection<Project> Projects { get; set; }

    }
}
