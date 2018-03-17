namespace SJBlogUWP.ResponseEntity
{
    /// <summary>
    /// 获取文章详情的Response实体类
    /// </summary>
    public class GetAnArticleDetailInfoRespEntity
    {
        /// <summary>
        /// 是否开启夜间模式
        /// "true" - 开启；"false" - 关闭
        /// </summary>
        public string enableNightMode { get; set; }

        /// <summary>
        /// 文章编号
        /// </summary>
        public string articleId { get; set; }
    }
}
