using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TinyCSS_Webapi.Models
{
    public class EnumError
    {
        public const int SUCCESS = 200;//成功
                                       //public const int AUTHORIZED_ERROR = 401;//未经授权

        // public const int NET_FRE_PASSWORD_ERROR = 10001;//用户名密码错误

        public const int NET_FRE_UNKNOWN_ERROR = 20001; //未知错误

        public const int NET_FRE_SERVICE_UNAVAILABLE = 20002; //服务暂不可用

        public const int NET_FRE_UNSUPPORTED_OPENAPI = 20003; //未知的方法

        public const int NET_FRE_INVALID_PARAMETER = 20004; //请求参数无效

        public const int NET_FRE_INVALID_FILE_EXTENSION = 30001; //图片格式无效

        public const int NET_FRE_OBJECT_EXISTS = 60001; //对象已存在
        public const int NET_FRE_OBJECT_NOT_EXISTS = 60002; //对象不存在
        public const int NET_FRE_UPLOADQINIU_ERROR = 70001; //对象不存在
    }
}
