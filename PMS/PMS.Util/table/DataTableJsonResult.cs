using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Util
{
   public class DataTableJsonResult<T>
    {
        public int code { get; set; }

        public string msg { get; set; }

        public int count { get; set; }

        public List<T> data { get; set; }
    }
}
