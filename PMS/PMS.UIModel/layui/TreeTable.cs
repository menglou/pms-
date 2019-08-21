using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.UIModel
{
    /// <summary>
    /// 树形列表
    /// </summary>
    /// <typeparam name="T"></typeparam>
   public class TreeTable<T>
    {

        public int code { get; set; }

        public string msg { get; set; }

        public int count { get; set; }

        public List<T> data { get; set; }
    }
}
