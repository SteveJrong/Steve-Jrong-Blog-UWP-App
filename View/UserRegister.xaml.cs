using Newtonsoft.Json;
using SJBlogLoginDemo1.Model;
using SJBlogLoginDemo1.Util;
using SJBlogLoginDemo1.Util.Common;
using SJBlogUWP.Model;
using System;
using System.Text;
using System.Text.RegularExpressions;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace SJBlogLoginDemo1.View
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class UserRegister : Page
    {
        public UserRegister()
        {
            this.InitializeComponent();

            InitUserEegisterPageDatas();
        }

        /// <summary>
        /// 初始化注册页面数据的方法
        /// </summary>
        public void InitUserEegisterPageDatas()
        {
            //绑定年龄下拉框数据
            for (int i = 18; i <= 100; i++)
            {
                inputSelectAge.Items.Add(i.ToString());
            }

            inputSelectAge.SelectedIndex = 0;
        }

        /// <summary>
        /// “注册”按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            string nullOrEmptyFormValidateResultStrings = validateIsNullOrEmptyOfForm();

            //如果非空验证的验证结果不为空，则表示有表单元素没填写完整
            if (!string.IsNullOrEmpty(nullOrEmptyFormValidateResultStrings) && nullOrEmptyFormValidateResultStrings.Length > 0)
            {
                await ContentDialogTemplateUtil.showContentDialog("请全部填写注册信息", "非常抱歉，您有如下信息未填写完整，请重新填写后再试。" + nullOrEmptyFormValidateResultStrings, true, false, "好的", string.Empty, ContentDialogStyle.NORMAL).ShowAsync();
            }
            //否则去验证已填写的内容
            else
            {
                string correctFormValidateResultStrings = validateIsNullOrEmptyOfForm();

                //如果数据合法性验证的验证结果不为空，则表示有表单元素填入了不合法的值
                if (!string.IsNullOrEmpty(correctFormValidateResultStrings) && correctFormValidateResultStrings.Length > 0)
                {
                    await ContentDialogTemplateUtil.showContentDialog("填写注册信息错误", "非常抱歉，您有如下信息填写错误，请重新填写后再试。" + correctFormValidateResultStrings, true, false, "好的", string.Empty, ContentDialogStyle.NORMAL).ShowAsync();
                }
                //否则填写正确
                else
                {
                    User user = new User() { userNickName = inputNickName.Text, userGender = inputGenderForBoy.IsChecked == true ? "男" : "女", userRole = "普通用户", userAge = int.Parse(inputSelectAge.SelectedValue.ToString()), userPassword = inputPassword.Password, userSecurityQuestionAnswer = inputSecurityQuestionAnswer.Text, userState = "正常", userLevel = 1, userEmail = inputEmailAddress.Text, userQQNumber = int.Parse(inputQQNumber.Text), userLocation = "浙江省杭州市江干区" };
                    string paramDatas = JsonConvert.SerializeObject(user);
                    SingleResultEntity resultObj = await HTTPRequestHelper<SingleResultEntity>.requestAndResponseSingleResult(MobileInterfaceFactory.USER_REGISTER, paramDatas);
                    if (Boolean.Parse(resultObj.result.ToString()) == true)
                    {
                        await ContentDialogTemplateUtil.showContentDialog(SystemMessage.DialogCommonTitleMsg, SystemMessage.RegisterSuccessMsg, true, false, SystemMessage.DialogButtonByOkValueMsg, String.Empty, ContentDialogStyle.NORMAL).ShowAsync();
                        Frame.Navigate(typeof(UserLogin));
                    }
                    else
                    {
                        await ContentDialogTemplateUtil.showContentDialog(SystemMessage.DialogCommonTitleMsg, SystemMessage.RegisterFailedMsg, true, false, SystemMessage.DialogButtonByOkValueMsg, string.Empty, ContentDialogStyle.NORMAL).ShowAsync();
                    }
                }
            }
        }

        /// <summary>
        /// 验证表单数据合法性的方法
        /// </summary>
        /// <returns></returns>
        private string validateIsCorrectOfForm()
        {
            StringBuilder sbMsg = new StringBuilder();
            Regex emailAddressRegex = new Regex(@"^\w{3,15}.(qq|foxmail|gmail|hotmail|163).com$");

            //验证电子邮件地址
            if (!emailAddressRegex.IsMatch(inputEmailAddress.Text))
            {
                sbMsg.AppendLine("您输入的电子邮件地址格式不正确。\n请检查您的电子邮件地址。");
            }

            //验证昵称
            if (inputNickName.Text.Length <= 5)
            {
                sbMsg.AppendLine("您输入的昵称太短。\n请输入至少5位且包含中英文大小写字母的文本。");
            }

            //验证密码
            Regex passwordRegex = new Regex(@"^(\d|\D)[^_]{7,29}$");
            if (!passwordRegex.IsMatch(inputPassword.Password))
            {
                sbMsg.AppendLine("您输入的密码格式不符合安全验证要求。\n请输入至少8位至多30位且包含中英文大小写字母的密码。");
            }

            //验证确认密码
            if (!inputPassword.Password.Equals(inputRePassword.Password))
            {
                sbMsg.AppendLine("您输入的密码与确认密码不符。\n请检查后重新输入。");
            }

            //验证QQ号
            Regex qqNumberRegex = new Regex(@"^\d{8,11}$");
            if (!qqNumberRegex.IsMatch(inputQQNumber.Text))
            {
                sbMsg.AppendLine("您输入的QQ号不合法。\n请输入正确且有效的QQ号码。");
            }

            return sbMsg.ToString();
        }

        /// <summary>
        /// 验证表单是否为空的方法
        /// </summary>
        /// <returns></returns>
        private string validateIsNullOrEmptyOfForm()
        {
            StringBuilder sbMsg = new StringBuilder();

            if (string.IsNullOrEmpty(inputNickName.Text) || inputNickName.Text.Length <= 0)
            {
                sbMsg.AppendLine("请填写您的昵称。");
            }
            if (null == inputSelectAge.SelectedValue)
            {
                sbMsg.AppendLine("请选择您的年龄。");
            }
            if (inputGenderForBoy.IsChecked == false && inputGenderForGirl.IsChecked == false)
            {
                sbMsg.AppendLine("请选择您的性别。");
            }
            if (string.IsNullOrEmpty(inputPassword.Password) || inputPassword.Password.Length <= 0)
            {
                sbMsg.AppendLine("请设置您的密码。");
            }
            if (string.IsNullOrEmpty(inputSecurityQuestionAnswer.Text) || inputSecurityQuestionAnswer.Text.Length <= 0)
            {
                sbMsg.AppendLine("请设置您的密保问题答案。");
            }
            if (string.IsNullOrEmpty(inputEmailAddress.Text) || inputEmailAddress.Text.Length <= 0)
            {
                sbMsg.AppendLine("请设置您的电子邮件地址。");
            }
            if (string.IsNullOrEmpty(inputQQNumber.Text) || inputQQNumber.Text.Length <= 0)
            {
                sbMsg.AppendLine("请填写您的腾讯QQ号码。");
            }
            return sbMsg.ToString();
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
