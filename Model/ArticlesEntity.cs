namespace SJBlogLoginDemo1.Model
{
    /// <summary>
    /// 博文实体类
    /// </summary>
    public class ArticlesEntity
    {
        /// <summary>
        /// 博文编号属性
        /// </summary>
        public int articleId { get; set; }

        /// <summary>
        /// 博文创建时间
        /// </summary>
        public string articleCreateDate { get; set; }

        /// <summary>
        /// 博文名称属性
        /// </summary>
        public string articleName { get; set; }

        /// <summary>
        /// 博文发布者属性
        /// </summary>
        public string articleAuthor { get; set; }

        /// <summary>
        /// 博文类型属性
        /// </summary>
        public string articleType { get; set; }

        /// <summary>
        /// 博文内容属性
        /// </summary>
        public string articleContent { get; set; }
    }
}
