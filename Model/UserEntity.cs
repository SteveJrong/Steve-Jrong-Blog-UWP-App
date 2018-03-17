namespace SJBlogLoginDemo1.Model
{
    public class User
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string userNickName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string userPassword { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string userGender { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        public string userRole { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public int userAge { get; set; }

        /// <summary>
        /// 安全验证问题答案
        /// </summary>
        public string userSecurityQuestionAnswer { get; set; }

        /// <summary>
        /// 用户状态
        /// </summary>
        public string userState { get; set; }

        /// <summary>
        /// 用户等级
        /// </summary>
        public int userLevel { get; set; }

        /// <summary>
        /// 电子邮件地址
        /// </summary>
        public string userEmail { get; set; }

        /// <summary>
        /// QQ号码
        /// </summary>
        public int userQQNumber { get; set; }

        /// <summary>
        /// 位置信息
        /// </summary>
        public string userLocation { get; set; }
    }
}
