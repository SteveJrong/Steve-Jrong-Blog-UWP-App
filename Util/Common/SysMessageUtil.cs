namespace SJBlogLoginDemo1.Util.Common
{
    /// <summary>
    /// 系统消息模板工具类
    /// </summary>
    public class SystemMessage
    {
        /// <summary>
        /// 登录成功提示
        /// </summary>
        private static string LoginSuccess = "恭喜！您的帐号验证成功。";

        /// <summary>
        /// 登录成功
        /// </summary>
        public static string LoginSuccessMsg
        {
            get
            {
                return LoginSuccess;
            }
        }

        /// <summary>
        /// 登录失败提示
        /// </summary>
        private static string LoginFailed = "非常抱歉，您的账号验证失败。\n请检查您的账户信息后重试。";

        /// <summary>
        /// 登录失败
        /// </summary>
        public static string LoginFailedMsg
        {
            get
            {
                return LoginFailed;
            }
        }

        /// <summary>
        /// 系统提示提示
        /// </summary>
        private static string dialogCommonTitle = "系统提示";

        /// <summary>
        /// 系统提示
        /// </summary>
        public static string DialogCommonTitleMsg
        {
            get
            {
                return dialogCommonTitle;
            }
        }

        /// <summary>
        /// 系统错误提示
        /// </summary>
        private static string dialogErrorTitle = "错误 (✖╭╮✖)";

        /// <summary>
        /// 错误提示
        /// </summary>
        public static string DialogErrorTitleMsg
        {
            get
            {
                return dialogErrorTitle;
            }
        }

        /// <summary>
        /// “确定”按钮文本
        /// </summary>
        private static string dialogButtonByOkValue = "确定";

        /// <summary>
        /// “确定”按钮
        /// </summary>
        public static string DialogButtonByOkValueMsg
        {
            get
            {
                return dialogButtonByOkValue;
            }
        }

        /// <summary>
        /// “取消”按钮文本
        /// </summary>
        private static string dialogButtonByCancelValue = "取消";

        public static string DialogButtonByCancelValueMsg
        {
            get
            {
                return dialogButtonByCancelValue;
            }
        }


        /// <summary>
        /// 注册成功提示
        /// </summary>
        private static string RegisterSuccess = "恭喜！您已成功注册。请牢记您的帐号和密码。";

        /// <summary>
        /// 注册成功
        /// </summary>
        public static string RegisterSuccessMsg
        {
            get
            {
                return RegisterSuccess;
            }
        }

        /// <summary>
        /// 注册失败提示
        /// </summary>
        private static string RegisterFailed = "非常抱歉，账号注册失败。\n请检查您的账户信息和网络后重试。";

        /// <summary>
        /// 登录失败
        /// </summary>
        public static string RegisterFailedMsg
        {
            get
            {
                return RegisterFailed;
            }
        }


    }
}
