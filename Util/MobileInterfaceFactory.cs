using SJBlogLoginDemo1.Util.Common;

namespace SJBlogLoginDemo1.Util
{
    /// <summary>
    /// 接口工厂类
    /// </summary>
    public class MobileInterfaceFactory
    {
        /// <summary>
        /// 接口根路径属性
        /// </summary>
        private static string rootUrl;

        public static string RootUrl
        {
            get
            {
                return SystemConfigUtil.getRootUrlForMobileInterfaces();
            }
        }

        private MobileInterfaceFactory()
        {
            rootUrl = SystemConfigUtil.getRootUrlForMobileInterfaces();
        }

        /// <summary>
        /// 用户登录接口
        /// </summary>
        public static readonly string USER_LOGIN = RootUrl + "mobileUserLogin.action";

        /// <summary>
        /// 用户注册接口
        /// </summary>
        public static readonly string USER_REGISTER = RootUrl + "mobileUserRegister.action";

        /// <summary>
        /// 载入所有博文信息（分页）接口
        /// </summary>
        public static readonly string GET_ALL_ARTICLES_INFO = RootUrl + "mobileLoadAllArticlesInfo.action";

        /// <summary>
        /// 根据博文编号获取单个博文信息接口
        /// </summary>
        public static readonly string GET_AN_ARTCILE_DETAIL_INFO = RootUrl + "mobileGetArticleDetailInfo.action";

        /// <summary>
        /// 根据博文编号获取单个博文标题信息接口
        /// </summary>
        public static readonly string GET_AN_ARTCILE_TITLE_INFO = RootUrl + "mobileGetArticleTitle.action";

        /// <summary>
        /// 根据博文编号获取博文的评论/回复信息接口
        /// </summary>
        public static readonly string GET_AN_ARTICLE_COMMENTS_OR_REPLIES_INFO = RootUrl + "mobileGetArticleCOrRInfo.action";

        /// <summary>
        /// 发布评论/回复信息接口
        /// </summary>
        public static readonly string PUBLISH_COMMENTS_OR_REPLIES_INFO = RootUrl + "mobilePublishCOrRInfo.action";

        /// <summary>
        /// 发布评论/回复信息接口
        /// </summary>
        public static readonly string DO_PRAISE_FOR_COMMENTS_OR_REPLIES_INFO = RootUrl + "mobileDoPraise.action";
    }
}
