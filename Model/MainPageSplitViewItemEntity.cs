namespace SJBlogUWP.Model
{
    public class MainPageSplitViewItemEntity /*: INotifyPropertyChanged, INotifyCollectionChanged*/
    {
        /// <summary>
        /// 图标字体
        /// </summary>
        public string IconFont { get; set; }

        /// <summary>
        /// 字体家族
        /// </summary>
        public string FontFamilyProperty { get; set; }

        /// <summary>
        /// SplitView显示的标题
        /// </summary>
        public string SplitViewTitle { get; set; }

        //public event PropertyChangedEventHandler PropertyChanged;
        //public event NotifyCollectionChangedEventHandler CollectionChanged;

    //    public void NotifyPropertyChanged(string propertyName)
    //    {
    //        PropertyChanged?.Invoke(this,
    //new PropertyChangedEventArgs(propertyName));
    //    }

        //    public void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        //    {
        //        CollectionChanged?.Invoke(this, e);
        //    }
    }
}
