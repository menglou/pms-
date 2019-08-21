using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.UIModel
{
   public class UserUIModel
    {
        /// <summary>
        /// 用户id
        /// </summary>
       
        public int UserIdUIModel { get; set; }


        /// <summary>
        /// 关联Guid
        /// </summary>
        public Guid RelationGuidUIModel { get; set; }

        /// <summary>
        /// 用户昵称
        /// </summary>
       
        public string UserNickNameUIModel { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
      
        public string UserPhoneNumberUIModel { get; set; }


        /// <summary>
        /// 密码
        /// </summary>
        
        public string UserPwdUIModel { get; set; }


        /// <summary>
        /// 性别
        /// </summary>
       
        public int UserGenderUIModel { get; set; }


        public string UserGenderUIModelstr
        {
            get
            {
                if(UserGenderUIModel==0)
                {
                    return "男";
                }
                else
                {
                    return "女";
                }
            }
        }

        /// <summary>
        /// 是否禁用
        /// </summary>

        public string IsDisableUIModel { get; set; }//0代表禁用，1代表没有


        public int? RoleIdUIModel { get; set; }


        /// <summary>
        /// 创建时间
        /// </summary>

        public DateTime CreateTimeUIModel { get; set; }


        public string CreateTime
        {
            get { return CreateTimeUIModel.ToString("yyyy-MM-dd"); }
        }

        /// <summary>
        /// 更新时间
        /// </summary>

        public DateTime UpdateTimeUIModel { get; set; }



        public int StudentIdUIModel { get; set; }

        public int LecturerUIModel { get; set; }

    }
}
