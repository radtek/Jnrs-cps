using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learun.Util.Operat
{
    public class OperateOnlineInfo
    {
        public static string OperateOnlineAccount;
        public static string OperateOnlineAppId;
        public static string OperateOnlineLoginMark;

        public static string cacheKeyOperator = "";
        public static string cacheKeyToken = "";
        public static Dictionary<string, string> tokenMarkList;
        public static Operator operatorInfo;
        public static long CacheIdloginInfo;
    }
}
