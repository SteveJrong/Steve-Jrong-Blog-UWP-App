using SJBlogLoginDemo1.Util.Common;
using System;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace SJBlogUWP.View
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class SysSettings : Page
    {
        ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;

        public SysSettings()
        {
            this.InitializeComponent();
            InitMainPageDatas();
        }

        public void InitMainPageDatas()
        {
            var themeValue = localSettings.Values["theme"] as string;
            if (!String.IsNullOrEmpty(themeValue) && themeValue.Equals("White"))
            {
                tsEnableNightMode.IsOn = false;
            }
            else
            {
                tsEnableNightMode.IsOn = true;
            }
        }

        /// <summary>
        /// “返回”按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void goBackButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        /// <summary>
        /// 开关夜间阅读模式按钮的方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsEnableNightMode_Toggled(object sender, RoutedEventArgs e)
        {
            if (tsEnableNightMode.IsOn == true)
            {
                //启用夜间模式
                //设置主题标识符为黑色
                localSettings.Values["theme"] = "Black";
            }
            else
            {
                //关闭夜间模式
                //设置主题标识符为白色
                localSettings.Values["theme"] = "White";
            }
        }

        /// <summary>
        /// 自动开关夜间阅读模式按钮的方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void tsAutoEnableNightMode_Toggled(object sender, RoutedEventArgs e)
        {
            if (tsAutoEnableNightMode.IsOn != true)
            {
                await ContentDialogTemplateUtil.showContentDialog("功能正在开发中", "非常抱歉，此功能目前暂未开放。", true, false, "好的", null, ContentDialogStyle.NORMAL).ShowAsync();
            }
            tsAutoEnableNightMode.IsOn = false;
        }
    }
}
