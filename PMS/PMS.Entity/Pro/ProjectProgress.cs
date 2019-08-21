using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace PMS.Entity
{
    public class ProjectProgress
    {

        /// <summary>
        /// 项目进度Id
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProjectProgressId { get; set; }

        /// <summary>
        /// 上报时间
        /// </summary>
        [Required]
        public DateTime SubmitTime { get; set; }


        /// <summary>
        /// 进度详情
        /// </summary>
        [Required]
        [StringLength(200)]
        public string ProgressDetails { get; set; }

        /// <summary>
        /// 是否检查
        /// </summary>
        [Required]
        public int IsCheck { get; set; }


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


        public int ProjectSignId { get; set; }

        public virtual ProjectSign ProjectSign { get; set; }

    }
}
