namespace SJBlogUWP.ResponseEntity
{
    /// <summary>
    /// 为评论/回复消息点赞的Response实体类
    /// </summary>
    public class DoPraiseRespEntity
    {
        /// <summary>
        /// 返回结果标识符属性
        /// </summary>
        public bool result { get; set; }

        /// <summary>
        /// 是否点赞过标识符属性
        /// </summary>
        public bool isParised { get; set; }
    }
}
