namespace SJBlogUWP.ResponseEntity
{
    /// <summary>
    /// 用户登录的Response实体类
    /// </summary>
    public class UserLoginRespEntity
    {
        /// <summary>
        /// 登录结果标识符属性
        /// </summary>
        public bool result { get; set; }

        /// <summary>
        /// 用户编号属性
        /// </summary>
        public string userId { get; set; }
    }
}
