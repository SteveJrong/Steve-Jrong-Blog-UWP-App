using Windows.ApplicationModel.Resources;

namespace SJBlogLoginDemo1.Util.Common
{
    /// <summary>
    /// 系统配置工具类
    /// </summary>
    public class SystemConfigUtil
    {
        /// <summary>
        /// 获取接口根路径的方法
        /// </summary>
        /// <returns></returns>
        public static string getRootUrlForMobileInterfaces()
        {
            return ResourceLoader.GetForCurrentView("MobileInterfaceResources").GetString("rootUrl");
        }
    }
}
