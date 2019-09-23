using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TinyCSS_Webapi.Models
{
    public class ApiResultMutilObject<T> : ApiResultBase
    {
        public List<T> data { get; set; }
        public int totalCnt { get; set; }
    }

    public class ApiResultSingleObject<T> : ApiResultBase
    {
        public T data { get; set; }
    }

    public class ApiResult : ApiResultBase
    {
        public string data { get; set; }
    }

    public class ApiResultBase
    {
        public int code { get; set; } = EnumError.NET_FRE_UNKNOWN_ERROR;
        public string message { get; set; } = "NET_FRE_UNKNOWN_ERROR";

    }
}
