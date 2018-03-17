using SJBlogLoginDemo1.Util;
using SJBlogUWP.Model;
using SJBlogUWP.ResponseEntity;
using SJBlogUWP.Util;
using System;
using Windows.Storage;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace SJBlogUWP.View
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class ArticleDetail : Page
    {

        bool _isLoaded = false;

        public bool IsLoaded
        {
            get
            {
                return _isLoaded;
            }

            set
            {
                _isLoaded = value;
            }
        }

        /// <summary>
        /// 博文编号字段
        /// </summary>
        private int articleId;

        /// <summary>
        /// 应用程序存储容器
        /// </summary>
        ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;

        public ArticleDetail()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            articleId = Int32.Parse(e.Parameter.ToString());

            InitArticleDetailPageDatas();
        }

        /// <summary>
        /// 初始化博文详情页数据的方法
        /// </summary>
        public async void InitArticleDetailPageDatas()
        {
            IsLoaded = true;
            //square1.Visibility = Visibility.Collapsed;
            //square2.Visibility = Visibility.Collapsed;

            await getArticleDetailInfo();

            //progress.IsActive = false;
            IsLoaded = false;
            progress.Visibility = Visibility.Collapsed;
            //square1.Visibility = Visibility.Visible;
            //square2.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// 获取文章详情，载入数据的方法
        /// </summary>
        /// <returns></returns>
        private async System.Threading.Tasks.Task getArticleDetailInfo()
        {
            SingleResultEntity resultEntity = await HTTPRequestHelper<SingleResultEntity>.requestAndResponseSingleResult(MobileInterfaceFactory.GET_AN_ARTCILE_TITLE_INFO, "{\"articleId\":" + articleId + "}");
            articleTitle.Text = resultEntity.result.ToString();
            //showArticleName.Text = articleDetail[0].articleName;
            //showArticleCreateDate.Text = articleDetail[0].articleCreateDate;
            //showArticleAuthor.Text = articleDetail[0].articleAuthor;
            //showArticleType.Text = articleDetail[0].articleType;
            //webBrowser.NavigateToString(articleDetail[0].articleContent);

            //HtmlDocument document = new HtmlDocument();
            //document.LoadHtml(articleDetail[0].articleContent);
            //HtmlNode node = document.DocumentNode;
            //showArticleContent.Text = document.DocumentNode.InnerText.Replace("&nbsp", "").Trim();
            //showArticleContent.Text = HttpUtility.HtmlDecode(node.InnerText);

            var themeValue = localSettings.Values["theme"] as string;
            String theme = String.Empty;
            if (!String.IsNullOrEmpty(themeValue) && themeValue.Equals("White"))
            {
                theme = "false";
            }
            else
            {
                theme = "true";
            }

            GetAnArticleDetailInfoRespEntity articleDetailEntity = new GetAnArticleDetailInfoRespEntity() { articleId = articleId.ToString(), enableNightMode = theme };

            Uri uri = new Uri(MobileInterfaceFactory.GET_AN_ARTCILE_DETAIL_INFO + "?paramJsonDatas=" + JsonUtil.convertObjectToJsonFmt(articleDetailEntity));

            webBrowser.Navigate(uri);
        }

        //public static string HtmlEntitiesEncode(string text)
        //{
        //    // 获取文本字符数组
        //    char[] chars = HttpUtility.HtmlEncode(text).ToCharArray();

        //    // 初始化输出结果
        //    StringBuilder result = new StringBuilder(text.Length + (int)(text.Length * 0.1));

        //    foreach (char c in chars)
        //    {
        //        // 将指定的 Unicode 字符的值转换为等效的 32 位有符号整数
        //        int value = Convert.ToInt32(c);

        //        // 内码为127以下的字符为标准ASCII编码，不需要转换，否则做 &#{数字}; 方式转换
        //        if (value > 127)
        //        {
        //            result.AppendFormat("&#{0};", value);
        //        }
        //        else
        //        {
        //            result.Append(c);
        //        }
        //    }
        //    return result.ToString();
        //}

        /// <summary>
        /// “返回”按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void goBackButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.Frame.CanGoBack)
            {
                this.Frame.GoBack();
            }
        }

        /// <summary>
        /// “网页”命令栏按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void HomeAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            await Launcher.LaunchUriAsync(new Uri(MobileInterfaceFactory.RootUrl + "detail_" + articleId));
        }

        /// <summary>
        /// “评论”命令栏按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContactAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Comments), articleId);
        }

        /// <summary>
        /// “刷新”命令栏按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void RefreshAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            await getArticleDetailInfo();
        }

        //private void webBrowser_ContentLoading(WebView sender, WebViewContentLoadingEventArgs args)
        //{
        //    state.Text = "正在加载……";
        //}

        //private void webBrowser_NavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        //{
        //    state.Text = "加载完成";
        //}
    }
}
