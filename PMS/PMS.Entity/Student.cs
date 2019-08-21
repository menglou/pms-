using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PMS.Entity
{
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StudentId { get; set; }


        [Required]
        [StringLength(100)]
        public string StudentName { get; set; }


        [Required]
        [StringLength(100)]
        public string StudentPhoneNumber { get; set; }


        [Required]
        public int StudentGender { get; set; }



        //Age
        [StringLength(100)]
        public string City { get; set; }


        [Required]
        public int Education { get; set; }


        [Required]
        [StringLength(100)]
        public string Personalrofile { get; set; }

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


        public int UserId { get; set; }

        public virtual User Users { get; set; }

        /// <summary>
        /// 可以报名多个项目，就有多个报名表
        /// </summary>
        public virtual ICollection<ProjectSign> ProjectSigns { get; set; }

    }
}
