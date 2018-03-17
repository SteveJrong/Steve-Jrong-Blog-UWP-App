using Newtonsoft.Json;

namespace SJBlogUWP.Util
{
    /// <summary>
    /// Json工具类
    /// </summary>
    public class JsonUtil
    {
        /// <summary>
        /// 将对象转换为Json字符串的方法
        /// </summary>
        /// <returns></returns>
        public static string convertObjectToJsonFmt(object objects)
        {
            return JsonConvert.SerializeObject(objects);
        }
    }
}
