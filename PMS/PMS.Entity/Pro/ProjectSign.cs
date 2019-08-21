using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PMS.Entity
{
    public class ProjectSign
    {
        /// <summary>
        /// 项目报名Id
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProjectSignId { get; set; }

        /// <summary>
        /// 报名时间
        /// </summary>
        [Required]
        public DateTime SignTime { get; set; }


        /// <summary>
        /// 项目状态
        /// </summary>
        public int ProjectStatus { get; set; }  //0 表示未审核    1表示审核未通过  2表示进行中 3表示已完成

        /// <summary>
        /// Git地址
        /// </summary>
        [StringLength(100)]
        public string GitAddress { get; set; }

        /// <summary>
        /// 项目评分
        /// </summary>
        public int ProjectScore { get; set; }

        /// <summary>
        /// 导师评价
        /// </summary>
        [StringLength(200)]
        public string ReviewTeacher { get; set; }


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



        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }



        public int StudentId { get; set; }
        public virtual Student Student { get; set; }
    }
}
