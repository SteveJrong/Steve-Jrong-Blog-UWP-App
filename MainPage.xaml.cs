using SJBlogLoginDemo1.Model;
using SJBlogLoginDemo1.Util;
using SJBlogLoginDemo1.Util.Common;
using SJBlogLoginDemo1.View;
using SJBlogUWP.Model;
using SJBlogUWP.View;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

//“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

namespace SJBlogLoginDemo1
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page, INotifyPropertyChanged
    {
        public MainPage()
        {
            this.InitializeComponent();

            this.InitMainPageDatas();
        }

        /// <summary>
        /// 翻页页数属性（默认第一页为1）
        /// </summary>
        private int pageIndex = 1;

        /// <summary>
        /// 应用程序存储容器
        /// </summary>
        ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;

        /// <summary>
        /// 博文实体集合属性
        /// </summary>
        ObservableCollection<ArticlesEntity> articlesModel = new ObservableCollection<ArticlesEntity>();

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

        /// <summary>
        /// 属性更改事件
        /// </summary>
        /// <param name="name"></param>
        private void OnPropertyChanged(string name)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        /// <summary>
        /// 汉堡菜单点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            Spiltter.IsPaneOpen = (Spiltter.IsPaneOpen == true) ? false : true;
        }

        ObservableCollection<MainPageSplitViewItemEntity> svModel = new ObservableCollection<MainPageSplitViewItemEntity>();

        int initDataFailedCount = 0;

        /// <summary>
        /// 初始化主页数据的方法
        /// </summary>
        public async void InitMainPageDatas()
        {
            //使用导航缓存
            NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Disabled;

            MainPageSplitViewItemEntity svItemLoginOrLogOff = new MainPageSplitViewItemEntity();

            //判断用户是否登录过
            string userIdentity = localSettings.Values["userIdentity"] as string;

            //不为空则表示登录过
            if (!string.IsNullOrEmpty(userIdentity))
            {
                svItemLoginOrLogOff.IconFont = "\ue603";
                svItemLoginOrLogOff.SplitViewTitle = "退 出";
                svItemLoginOrLogOff.FontFamilyProperty = "ms-appx:///Assets/Resources/iconfont.ttf#iconfont";
            }
            else
            {
                svItemLoginOrLogOff.IconFont = "\ue604";
                svItemLoginOrLogOff.SplitViewTitle = "登 录";
                svItemLoginOrLogOff.FontFamilyProperty = "ms-appx:///Assets/Resources/iconfont.ttf#iconfont";
            }

            MainPageSplitViewItemEntity svItemRegister = new MainPageSplitViewItemEntity();
            svItemRegister.IconFont = "\ue605";
            svItemRegister.SplitViewTitle = "注 册";
            svItemRegister.FontFamilyProperty = "ms-appx:///Assets/Resources/iconfont.ttf#iconfont";

            splitViewListItems.ItemsSource = null;

            svModel.Add(svItemLoginOrLogOff);
            svModel.Add(svItemRegister);

            splitViewListItems.ItemsSource = svModel;

            try
            {
                LoadInitData = true;
                articlesModel = await HTTPRequestHelper<ArticlesEntity>.requestAndResponseCollections(MobileInterfaceFactory.GET_ALL_ARTICLES_INFO, "{\"paramJsonDatas\":" + pageIndex + "}");
                mainPageList.ItemsSource = articlesModel;
                progressOfInitFirstDatas.Visibility = Visibility.Collapsed;
            }
            catch (HttpRequestException requestEx)
            {
                //初始化失败计数+1
                initDataFailedCount += 1;

                //异常之后再次请求
                InitMainPageDatas();

                //如果请求次数超出2次，说明不是MySQL超时自动关闭连接的问题而是其他问题，弹出消息框
                if (initDataFailedCount > 2)
                {
                    await ContentDialogTemplateUtil.showContentDialog(SystemMessage.DialogCommonTitleMsg + "HttpRequestException", requestEx.Message, true, false, SystemMessage.DialogButtonByOkValueMsg, String.Empty, ContentDialogStyle.NORMAL).ShowAsync();
                }
            }
            catch (Exception ex)
            {
                //异常之后再次请求
                InitMainPageDatas();

                await ContentDialogTemplateUtil.showContentDialog(SystemMessage.DialogCommonTitleMsg + "Exception", ex.Message, true, false, SystemMessage.DialogButtonByOkValueMsg, String.Empty, ContentDialogStyle.NORMAL).ShowAsync();
            }
        }

        private void scrollViewer_Loaded(object sender, RoutedEventArgs e)
        {
            scrollViewer.ChangeView(null, 30, null);
        }

        private async void scrollViewer_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            if (!e.IsIntermediate)
            {
                IsPullRefresh = true;
                //将服务器返回的JSON反序列化为对象集合
                foreach (ArticlesEntity item in await HTTPRequestHelper<ArticlesEntity>.requestAndResponseCollections(MobileInterfaceFactory.GET_ALL_ARTICLES_INFO, "{\"paramJsonDatas\":" + (pageIndex += 1).ToString() + "}"))
                {
                    articlesModel.Add(item);
                }
                mainPageList.ItemsSource = articlesModel;
                IsPullRefresh = false;
            }
        }

        /// <summary>
        /// ListView子项选中事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mainPageList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int value = ((ArticlesEntity)(mainPageList.SelectedItem)).articleId;
            Frame.Navigate(typeof(ArticleDetail), value);
        }

        /// <summary>
        /// “关于”按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AboutButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AboutSJBlog));
        }

        /// <summary>
        /// SplitView下部的设置按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void settingsButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SysSettings));
        }

        /// <summary>
        /// SplitView中ListItem选中项事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void splitViewListItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (null != svModel && svModel.Count > 0)
            {
                var selectItem = svModel[0];

                //如果当前显示“退 出”，则表示已经登录，要清除用户信息
                if (selectItem.SplitViewTitle.Contains("退 出"))
                {
                    localSettings.Values["userIdentity"] = null;

                    MainPageSplitViewItemEntity svItemLoginOrLogOff = new MainPageSplitViewItemEntity();
                    //判断用户是否登录过
                    string userIdentity = localSettings.Values["userIdentity"] as string;

                    //不为空则表示登录过
                    if (!string.IsNullOrEmpty(userIdentity))
                    {
                        svItemLoginOrLogOff.IconFont = "\ue603";
                        svItemLoginOrLogOff.SplitViewTitle = "退 出";
                        svItemLoginOrLogOff.FontFamilyProperty = "ms-appx:///Assets/Resources/iconfont.ttf#iconfont";
                    }
                    else
                    {
                        svItemLoginOrLogOff.IconFont = "\ue604";
                        svItemLoginOrLogOff.SplitViewTitle = "登 录";
                        svItemLoginOrLogOff.FontFamilyProperty = "ms-appx:///Assets/Resources/iconfont.ttf#iconfont";
                    }

                    MainPageSplitViewItemEntity svItemRegister = new MainPageSplitViewItemEntity();
                    svItemRegister.IconFont = "\ue605";
                    svItemRegister.SplitViewTitle = "注 册";
                    svItemRegister.FontFamilyProperty = "ms-appx:///Assets/Resources/iconfont.ttf#iconfont";

                    if (null != svModel)
                    {
                        svModel.Clear();
                    }

                    svModel.Add(svItemLoginOrLogOff);
                    svModel.Add(svItemRegister);

                    splitViewListItems.ItemsSource = svModel;
                }

                else if (splitViewListItems.SelectedIndex == 1)
                {
                    //导航到“注册”页
                    this.Frame.Navigate(typeof(UserRegister));
                }//如果当前显示“登录”，则表示未登录，要转向登录页面
                else if (selectItem.SplitViewTitle.Contains("登 录"))
                {
                    //导航到“登录”页
                    this.Frame.Navigate(typeof(UserLogin));
                }
            }
        }
    }
}
