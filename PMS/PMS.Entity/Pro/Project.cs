using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PMS.Entity
{
    public class Project
    {
        /// <summary>
        /// 项目Id
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProjectId { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        [StringLength(200)]
        [Required]
        public string ProjectName { get; set; }

        /// <summary>
        /// 项目框架
        /// </summary>
        [StringLength(200)]
        [Required]
        public string ProjectFramework { get; set; }

        /// <summary>
        /// 项目难度
        /// </summary>
        [StringLength(100)]
        public string Prodifficulty { get; set; }

        /// <summary>
        /// 学习周期
        /// </summary>
        public int StudyTime { get; set; }
        
        /// <summary>
        /// 演示地址
        /// </summary>
        [StringLength(100)]
        public string PalyAddress { get; set; }
         
        /// <summary>
        /// 项目介绍
        /// </summary>
        [Required]
        [StringLength(5000)]
        public string ProjectIntroduction { get; set; }

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

        
        /// <summary>
        /// 一个项目一个指导老师
        /// </summary>

        public int LecturerId { get; set; }

        public virtual Lecturer Lecturer { get; set; }

    }
}
