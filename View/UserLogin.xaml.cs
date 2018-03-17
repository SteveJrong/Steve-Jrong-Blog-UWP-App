using Newtonsoft.Json;
using SJBlogLoginDemo1.Model;
using SJBlogLoginDemo1.Util;
using SJBlogLoginDemo1.Util.Common;
using SJBlogUWP.ResponseEntity;
using System;
using System.Collections.ObjectModel;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上提供

namespace SJBlogLoginDemo1
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class UserLogin : Page
    {
        public UserLogin()
        {
            this.InitializeComponent();
        }

        ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(this.inputAccount.Text) || this.inputAccount.Text.Length <= 0)
            {
                await new MessageDialog("请填写您的账户信息。", SystemMessage.DialogCommonTitleMsg).ShowAsync();
                return;
            }
            else if (String.IsNullOrEmpty(this.inputPassword.Password) || this.inputPassword.Password.Length <= 0)
            {
                await new MessageDialog("请填写您的账户密码。", SystemMessage.DialogCommonTitleMsg).ShowAsync();
                return;
            }

            User u = new User() { userNickName = this.inputAccount.Text, userPassword = this.inputPassword.Password };

            //对象序列化为JSON字符串
            string serializeResult = JsonConvert.SerializeObject(u);

            ObservableCollection<UserLoginRespEntity> result = await HTTPRequestHelper<UserLoginRespEntity>.requestAndResponseCollections(MobileInterfaceFactory.USER_LOGIN, serializeResult);
           
            if (result[0].result == true)
            {
                //await ContentDialogTemplateUtil.showContentDialog(SystemMessage.DialogCommonTitleMsg, SystemMessage.LoginSuccessMsg, true, false, SystemMessage.DialogButtonByOkValueMsg, string.Empty, ContentDialogStyle.NORMAL).ShowAsync();

                //登录成功后放入用户信息
                localSettings.Values["userIdentity"] = result[0].userId;

                Frame.Navigate(typeof(MainPage));
            }
            else
            {
                await ContentDialogTemplateUtil.showContentDialog(SystemMessage.DialogCommonTitleMsg, SystemMessage.LoginFailedMsg, true, false, SystemMessage.DialogButtonByOkValueMsg, string.Empty, ContentDialogStyle.NORMAL).ShowAsync();
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
    }
}
