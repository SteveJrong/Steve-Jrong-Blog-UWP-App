using Windows.UI.Popups;

namespace SJBlogUWP.Util.Common
{
    /// <summary>
    /// Content Dialog对话框样式枚举
    /// </summary>
    public enum MessageDialogStyle
    {
        /// <summary>
        /// 仅有内容的对话框
        /// </summary>
        ONLY_CONTENT = 0,

        /// <summary>
        /// 仅有标题的对话框
        /// </summary>
        ONLY_TITLE = 1,

        /// <summary>
        /// 同时具有标题和内容的对话框
        /// </summary>
        HAVING_BOTH = 2
    }

    /// <summary>
    /// Message Dialog模板工具类
    /// </summary>
    public class MessageDialogTemplateUtil
    {
        /// <summary>
        /// 呈现Message Dialog对话框的方法
        /// </summary>
        /// <param name="messageDialogTitle">消息框标题</param>
        /// <param name="messageDialogContent">消息框内容</param>
        /// <param name="messageDialogStyle">消息框样式</param>
        /// <returns></returns>
        public static MessageDialog showMessageDialog(string messageDialogTitle, string messageDialogContent, MessageDialogStyle messageDialogStyle)
        {
            MessageDialog dialog = null;

            switch (messageDialogStyle)
            {
                case MessageDialogStyle.ONLY_CONTENT:
                    dialog = new MessageDialog(messageDialogContent);
                    break;

                case MessageDialogStyle.ONLY_TITLE:
                    dialog = new MessageDialog(messageDialogTitle);
                    break;

                case MessageDialogStyle.HAVING_BOTH:
                    dialog = new MessageDialog(messageDialogContent, messageDialogTitle);
                    break;
            }

            return dialog;
        }
    }
}
