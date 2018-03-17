using SJBlogLoginDemo1;
using SJBlogLoginDemo1.Util;
using SJBlogLoginDemo1.Util.Common;
using SJBlogUWP.Model;
using SJBlogUWP.ResponseEntity;
using SJBlogUWP.Util.Common;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using Windows.Security.ExchangeActiveSyncProvisioning;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace SJBlogUWP.View
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class Comments : Page
    {
        public Comments()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// 翻页页数属性（默认第一页为1）
        /// </summary>
        private int pageIndex = 1;

        ObservableCollection<CommentsOrRepliesEntity> commentsOrRepliesModel = new ObservableCollection<CommentsOrRepliesEntity>();

        ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;

        /// <summary>
        /// 订阅的组件属性更改事件对象
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 定义上拉刷新字段并赋初值为false
        /// </summary>
        bool _isPullRefresh = false;

        /// <summary>
        /// 定义数据初始化的字段并赋初值为false
        /// </summary>
        bool _loadInitData = false;

        /// <summary>
        /// 博文编号字段
        /// </summary>
        private int articleId;

        /// <summary>
        /// 上拉刷新标识属性
        /// </summary>
        public bool IsPullRefresh
        {
            get
            {
                return _isPullRefresh;
            }

            set
            {
                _isPullRefresh = value;
                OnPropertyChanged(nameof(IsPullRefresh));
            }
        }

        public bool LoadInitData
        {
            get
            {
                return _loadInitData;
            }

            set
            {
                _loadInitData = value;
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            articleId = int.Parse(e.Parameter.ToString());
            InitMainPageDatas(false);
        }

        /// <summary>
        /// 属性更改事件
        /// </summary>
        /// <param name="name"></param>
        private void OnPropertyChanged(string name)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        /// <summary>
        /// 初始化主页数据的方法
        /// </summary>

        public async void InitMainPageDatas(bool refreshButtonClicked)
        {
            noDataPicArea.Visibility = Visibility.Collapsed;
            scrollViewer.Visibility = Visibility.Visible;
            try
            {
                LoadInitData = true;
                commentsOrRepliesModel = await HTTPRequestHelper<CommentsOrRepliesEntity>.requestAndResponseCollections(MobileInterfaceFactory.GET_AN_ARTICLE_COMMENTS_OR_REPLIES_INFO, "{\"pageIndex\":" + (refreshButtonClicked == true ? 1 : pageIndex) + ",\"articleId\":" + articleId + "}");

                //为0则表示没有数据，给出提示
                if (commentsOrRepliesModel.Count == 0)
                {
                    noDataPicArea.Visibility = Visibility.Visible;
                    scrollViewer.Visibility = Visibility.Collapsed;
                }
                else
                {
                    noDataPicArea.Visibility = Visibility.Collapsed;
                    scrollViewer.Visibility = Visibility.Visible;

                    cOrRList.ItemsSource = commentsOrRepliesModel;
                }

                progressOfInitFirstCOrRDatas.Visibility = Visibility.Collapsed;
            }
            catch (HttpRequestException requestEx)
            {
                await ContentDialogTemplateUtil.showContentDialog(SystemMessage.DialogCommonTitleMsg, requestEx.Message, true, false, SystemMessage.DialogButtonByOkValueMsg, String.Empty, ContentDialogStyle.NORMAL).ShowAsync();
            }
            catch (Exception ex)
            {
                await ContentDialogTemplateUtil.showContentDialog(SystemMessage.DialogCommonTitleMsg, ex.Message, true, false, SystemMessage.DialogButtonByOkValueMsg, String.Empty, ContentDialogStyle.NORMAL).ShowAsync();
            }
            finally
            {
                LoadInitData = false;
            }
        }

        /// <summary>
        /// “返回”按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void goBackButton_Click(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }

        private void scrollViewer_Loaded(object sender, RoutedEventArgs e)
        {
            scrollViewer.ChangeView(null, 30, null);
        }

        private async void scrollViewer_ViewerChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            if (!e.IsIntermediate)
            {
                IsPullRefresh = true;
                //将服务器返回的JSON反序列化为对象集合
                foreach (CommentsOrRepliesEntity item in await HTTPRequestHelper<CommentsOrRepliesEntity>.requestAndResponseCollections(MobileInterfaceFactory.GET_AN_ARTICLE_COMMENTS_OR_REPLIES_INFO, "{\"pageIndex\":" + (pageIndex += 1).ToString() + ",\"articleId\":" + articleId + "}"))
                {
                    commentsOrRepliesModel.Add(item);
                }
                cOrRList.ItemsSource = commentsOrRepliesModel;
                IsPullRefresh = false;
            }
        }

        private void cOrRList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //int value = ((CommentsOrRepliesEntity)(cOrRList.SelectedItem)).id;
            //await ContentDialogTemplateUtil.showContentDialog("评论/回复消息", "此消息ID：" + value, true, false, "好的", null, ContentDialogStyle.NORMAL).ShowAsync();
        }

        /// <summary>
        /// “刷新”App Bar按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Refersh_AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            InitMainPageDatas(true);
        }

        private async void definitionDialogPrimaryButton_Click(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            if (string.IsNullOrEmpty(inputCommentsOrReplies.Text) || inputCommentsOrReplies.Text.Length <= 0)
            {
                await MessageDialogTemplateUtil.showMessageDialog("请填写您的评论", "评论不能为空，请输入您的评论。", MessageDialogStyle.HAVING_BOTH).ShowAsync();
            }
            else if (inputCommentsOrReplies.Text.Length <= 9)
            {
                await MessageDialogTemplateUtil.showMessageDialog("评论字数过少", "您发表的评论字数过少，请至少输入10个字符以上。", MessageDialogStyle.HAVING_BOTH).ShowAsync();
            }
            else
            {
                EasClientDeviceInformation systemInfo = new EasClientDeviceInformation();

                try
                {
                    SingleResultEntity resultEntity = await HTTPRequestHelper<SingleResultEntity>.requestAndResponseSingleResult(MobileInterfaceFactory.PUBLISH_COMMENTS_OR_REPLIES_INFO, "{\"articleId\": " + articleId + ",\"userId\" : " + localSettings.Values["userIdentity"] + ",\"content\" : " + inputCommentsOrReplies.Text + ",\"device\" : " + systemInfo.SystemSku + ",\"system\" : " + systemInfo.OperatingSystem + " }");

                    if (bool.Parse(resultEntity.result.ToString()) == true)
                    {
                        await MessageDialogTemplateUtil.showMessageDialog("评论成功", "恭喜，您的评论/回复消息发布成功！您的评论/回复消息需要人工审核之后才能正常显示，还请您耐心等待哦~", MessageDialogStyle.HAVING_BOTH).ShowAsync();
                        InitMainPageDatas(true);
                    }
                    cOrRList.IsSwipeEnabled = true;
                }
                catch (Exception ex)
                {
                    await MessageDialogTemplateUtil.showMessageDialog("评论失败", "非常抱歉，在您尝试发表评论/回复消息时发生了错误，请稍后再试。\n异常信息为：\n" + ex.Message, MessageDialogStyle.HAVING_BOTH).ShowAsync();
                }
            }
        }

        private void definitionDialogSecondButton_Click(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {

        }

        /// <summary>
        /// “发表评论”AppBar点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Publish_Comments_Or_Replies_AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            //先判断用户是否登录
            string userIdentity = localSettings.Values["userIdentity"] as string;
            if (string.IsNullOrEmpty(userIdentity) || userIdentity.Length <= 0)
            {
                Frame.Navigate(typeof(UserLogin));
            }
            else
            {
                await definitionDialog.ShowAsync();
            }
        }


        private async void PraiseCountButton_Click(object sender, RoutedEventArgs e)
        {
            string dialogTitle = "", dialogContent = "";

            var selectItem = cOrRList.SelectedItem;

            //如果没有选中项，则给出提示
            if (null == selectItem)
            {
                await ContentDialogTemplateUtil.showContentDialog("操作错误", "请选中一项评论/回复消息后再点赞。", true, false, "好的", null, ContentDialogStyle.NORMAL).ShowAsync();
            }
            else
            {
                string prepareParam = "{\"paramJsonDatas\":" + ((CommentsOrRepliesEntity)selectItem).id + "}";
                ObservableCollection<DoPraiseRespEntity> resultEntity = await HTTPRequestHelper<DoPraiseRespEntity>.requestAndResponseCollections(MobileInterfaceFactory.DO_PRAISE_FOR_COMMENTS_OR_REPLIES_INFO, prepareParam);
                //最终结果属性
                var resultFlag = bool.Parse(resultEntity[0].result.ToString());
                //是否点赞过属性
                var isPraisedFlag = bool.Parse(resultEntity[0].isParised.ToString());

                //如果最终结果为true，是否点赞为false，则表示点赞成功
                if (resultFlag == true && isPraisedFlag == false)
                {
                    dialogTitle = "点赞成功";
                    dialogContent = "恭喜，点赞成功！";
                    InitMainPageDatas(true);
                }
                //如果最终结果为false，是否点赞为true，则表示已点过赞
                else if (resultFlag == false && isPraisedFlag == true)
                {
                    dialogTitle = "点赞失败";
                    dialogContent = "请勿重复点赞！";
                }
                //如果最终结果和是否点赞都为false，则表示点赞失败
                else if (resultFlag == false && isPraisedFlag == false)
                {
                    dialogTitle = "点赞失败";
                    dialogContent = "非常抱歉，在您尝试点赞时发生错误，请稍候再试。";
                }

                await ContentDialogTemplateUtil.showContentDialog(dialogTitle, dialogContent, true, false, "好的", null, ContentDialogStyle.NORMAL).ShowAsync();
            }
        }
    }
}
