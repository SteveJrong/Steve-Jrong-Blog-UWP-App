using Windows.UI.Xaml.Controls;

namespace SJBlogLoginDemo1.Util.Common
{
    /// <summary>
    /// Content Dialog对话框样式枚举
    /// </summary>
    public enum ContentDialogStyle
    {
        /// <summary>
        /// 普通对话框
        /// </summary>
        NORMAL = 0
    }

    /// <summary>
    /// Content Dialog模板工具类
    /// </summary>
    public class ContentDialogTemplateUtil
    {
        /// <summary>
        /// 呈现Content Dialog对话框的方法
        /// </summary>
        public static ContentDialog showContentDialog(string contentDialogTitle, string contentDialogContent, bool isEnabledOfPrimaryButton, bool isEnabledOfSecondButton, string primaryButtonText, string secondButtonText, ContentDialogStyle contentDialogStyle)
        {
            ContentDialog dialog = new ContentDialog();
            switch (contentDialogStyle)
            {
                case ContentDialogStyle.NORMAL:
                    dialog.Title = contentDialogTitle;
                    dialog.Content = contentDialogContent;
                    dialog.IsPrimaryButtonEnabled = isEnabledOfPrimaryButton;
                    dialog.IsSecondaryButtonEnabled = isEnabledOfSecondButton;
                    dialog.PrimaryButtonText = primaryButtonText;
                    if (!string.IsNullOrEmpty(secondButtonText))
                    {
                        dialog.SecondaryButtonText = secondButtonText;
                    }
                    break;
            }
            return dialog;
        }
    }
}
