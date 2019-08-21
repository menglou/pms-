using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.UIModel
{
  public  class JsonResultData<T>
    {
        public int Code { get; set; }

        public string Msg { get; set; }

        public T Data { get; set; }


        public int IsStuent { get; set; }
    }
}
