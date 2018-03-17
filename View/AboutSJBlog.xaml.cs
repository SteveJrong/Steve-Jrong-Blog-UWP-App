using System;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace SJBlogUWP.View
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class AboutSJBlog : Page
    {
        public AboutSJBlog()
        {
            this.InitializeComponent();
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
        /// “官方网站”链接事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void moreLink_Click(object sender, RoutedEventArgs e)
        {
            await Launcher.LaunchUriAsync(new Uri("https://www.stevejrong.top/"));
        }

        private async void moreLink2_Click(object sender, RoutedEventArgs e)
        {
            await Launcher.LaunchUriAsync(new Uri("https://github.com/SteveJrong"));
        }
    }
}
