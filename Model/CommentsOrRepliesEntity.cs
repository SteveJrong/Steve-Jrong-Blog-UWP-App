namespace SJBlogUWP.Model
{
    /// <summary>
    /// 评论/回复实体类
    /// </summary>
    public class CommentsOrRepliesEntity
    {
        /// <summary>
        /// 评论/回复编号属性
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 评论/回复内容属性
        /// </summary>
        public string content { get; set; }

        /// <summary>
        /// 评论/回复的发表时间属性
        /// </summary>
        public string publishDate { get; set; }

        /// <summary>
        /// 评论/回复的消息类型属性
        /// </summary>
        public int messageType { get; set; }

        /// <summary>
        /// 评论/回复发布时的设备信息属性
        /// </summary>
        public string publishedDeviceInfo { get; set; }

        /// <summary>
        /// 评论/回复发布时的系统信息属性
        /// </summary>
        public string publishedSystemInfo { get; set; }

        /// <summary>
        /// 评论/回复消息的点赞数属性
        /// </summary>
        public int praiseCount { get; set; }
    }
}
