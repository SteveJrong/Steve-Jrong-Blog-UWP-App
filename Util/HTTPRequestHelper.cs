using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;

namespace SJBlogLoginDemo1.Util
{
    /// <summary>
    /// HTTP请求工具类
    /// </summary>
    public class HTTPRequestHelper<T>
    {
        /// <summary>
        /// Request请求并返回集合的方法
        /// </summary>
        /// <param name="urlAddress">请求路径（格式：请求类型://请求路径.action）</param>
        /// <param name="paramDatas">请求参数（直接传入参数）</param>
        public static async Task<ObservableCollection<T>> requestAndResponseCollections(string urlAddress, string paramDatas)
        {
            Uri uri = new Uri(urlAddress + "?paramJsonDatas=" + paramDatas);
            //实例化HttpClient对象
            HttpClient httpClient = new HttpClient();
            //异步获取服务器返回的结果字符串
            string result = await httpClient.GetStringAsync(uri);
            //服务器响应的JSON字符串反序列化为JSON对象
            JObject o = (JObject)JsonConvert.DeserializeObject(result);
            //将服务器返回的JSON反序列化为泛型对象的集合
            return JsonConvert.DeserializeObject<ObservableCollection<T>>(o["result"].ToString());
        }

        /// <summary>
        /// Request请求并返回单个结果集的方法
        /// </summary>
        /// <param name="urlAddress">请求路径（格式：请求类型://请求路径.action）</param>
        /// <param name="paramDatas">请求参数（直接传入参数）</param>
        public static async Task<T> requestAndResponseSingleResult(string urlAddress, string paramDatas)
        {
            Uri uri = new Uri(urlAddress + "?paramJsonDatas=" + paramDatas);
            //实例化HttpClient对象
            HttpClient httpClient = new HttpClient();
            //异步获取服务器返回的结果字符串
            string result = await httpClient.GetStringAsync(uri);
            //服务器响应的JSON字符串反序列化为JSON对象
            return JsonConvert.DeserializeObject<T>(result);
        }

        ///// <summary>
        ///// Request请求并返回单个序列化实体的方法
        ///// </summary>
        ///// <param name="urlAddress">请求路径（格式：请求类型://请求路径.action）</param>
        ///// <param name="paramDatas">请求参数（直接传入参数）</param>
        //public static async Task<T> requestAndResponseSingleSerializableEntityResult(string urlAddress, string paramDatas)
        //{
        //    Uri uri = new Uri(urlAddress + "?paramJsonDatas=" + paramDatas);
        //    //实例化HttpClient对象
        //    HttpClient httpClient = new HttpClient();
        //    //异步获取服务器返回的结果字符串
        //    string result = await httpClient.GetStringAsync(uri);
        //    //服务器响应的JSON字符串反序列化为JSON对象
        //    JObject o = (JObject)JsonConvert.DeserializeObject(result);
        //    //将服务器返回的JSON反序列化为泛型对象的集合
        //    return JsonConvert.DeserializeObject<T>(o["result"].ToString());
        //}
    }
}
