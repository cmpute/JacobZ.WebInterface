using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace JacobZ.WebInterface.BangumiTv
{
    /// <summary>
    /// 为bangumi.tv的API提供接口整理
    /// </summary>
    public static class ApiCore
    {
        const string BaseAddress = "https://api.bgm.tv/";

        public static string ApplicationName { get; set; } = "Bgm.Net";

        /// <summary>
        /// 获取条目详细信息
        /// </summary>
        /// <param name="Id">条目ID</param>
        /// <param name="simple">是否仅获取简要信息</param>
        /// <returns>新的条目对象</returns>
        // TODO: 使用 https://api.bgm.tv/subject/{id}/ep API可以获得介于simple和large之间详细程度的结果
        public static async Task<Subject> GetSubject(uint Id, bool simple = true)
        {
            var client = ClientManager.Client;
            var simpletxt = simple ? "simple" : "large";
            using (var jstream = await client.GetStreamAsync(
                BaseAddress + $"subject/{Id}?responseGroup={simpletxt}"))
            {
                using (var sr = new StreamReader(jstream))
                using (var jr = new JsonTextReader(sr))
                {
                    var serializer = new JsonSerializer();
                    serializer.ContractResolver = simple ? BangumiContractResolver.Simple : BangumiContractResolver.Detailed;
                    return serializer.Deserialize<Subject>(jr);
                }
            }
        }

        /// <summary>
        /// 用户认证
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="source">用于告诉服务器验证的请求源，可设为应用的名字</param>
        /// <returns>返回用户对象，若验证失败则返回空</returns>
        public static async Task<User> Authenticate(string username, string password)
        {
            var client = ClientManager.Client;
            var content = new FormUrlEncodedContent(new Dictionary<string, string>() { { "username", username }, { "password", password } });
            var result = await client.PostAsync(BaseAddress + $"auth?&source={ApplicationName}", content);
            if (!result.IsSuccessStatusCode) return null;
            using (var jstream = await result.Content.ReadAsStreamAsync())
            {
                using (var sr = new StreamReader(jstream))
                using (var jr = new JsonTextReader(sr))
                    return new JsonSerializer().Deserialize<User>(jr);
            }
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="Id">用户ID</param>
        /// <returns>用户对象</returns>
        public static Task<User> GetUser(uint Id) => GetUserInternal($"user/{Id}");

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="Id">用户名</param>
        /// <returns>用户对象</returns>
        public static Task<User> GetUser(string username) => GetUserInternal($"user/{username}");

        private static async Task<User> GetUserInternal(string url)
        {
            var client = ClientManager.Client;
            using (var jstream = await client.GetStreamAsync(BaseAddress + url))
            {
                using (var sr = new StreamReader(jstream))
                using (var jr = new JsonTextReader(sr))
                    return new JsonSerializer().Deserialize<User>(jr);
            }
        }

        /// <summary>
        /// 获取用户收藏列表
        /// </summary>
        /// <param name="authUser">认证通过的用户对象</param>
        /// <returns></returns>
        public static async Task<IEnumerable<Collection>> GetWatchingCollections(User authUser)
        {
            var client = ClientManager.Client;
            var auth = WebUtility.UrlDecode(authUser.Authentication);
            using (var jstream = await client.GetStreamAsync(BaseAddress +
                $"user/{authUser.ID}/collection?cat=watching&source={ApplicationName}&auth={auth}"))
            {
                using (var sr = new StreamReader(jstream))
                using (var jr = new JsonTextReader(sr))
                {
                    var serializer = new JsonSerializer();
                    serializer.ContractResolver = BangumiContractResolver.Simple;
                    // XXX: 考虑这里是否需要将Collection的User属性赋值
                    return serializer.Deserialize<IEnumerable<Collection>>(jr);
                }
            }
        }

        /// <summary>
        /// 获取通知数量
        /// </summary>
        /// <param name="authUser">认证通过的用户对象</param>
        /// <returns>新消息数量</returns>
        public static async Task<int> GetNotificationCount(User authUser)
        {
            var client = ClientManager.Client;
            var auth = WebUtility.UrlDecode(authUser.Authentication);
            using (var jstream = await client.GetStreamAsync(BaseAddress + $"notify/count?source={ApplicationName}&auth={auth}"))
            {
                using (var sr = new StreamReader(jstream))
                using (var jr = new JsonTextReader(sr))
                {
                    var obj = JObject.Load(jr);
                    return obj["count"].Value<int>();
                }
            }
        }

        /// <summary>
        /// 获取追番进度信息
        /// </summary>
        /// <param name="authUser">认证通过的用户对象</param>
        /// <param name="subjectID">条目ID</param>
        /// <returns>键为集编号，值为收藏状态的字典</returns>
        public static async Task<IReadOnlyDictionary<uint, CollectionStatus>> GetSubjectProgress(User authUser, uint subjectID)
        {
            var client = ClientManager.Client;
            var auth = WebUtility.UrlDecode(authUser.Authentication);
            using (var jstream = await client.GetStreamAsync(BaseAddress + $"user/{authUser.ID}/progress?subject_id={subjectID}&source={ApplicationName}&auth={auth}"))
            {
                using (var sr = new StreamReader(jstream))
                using (var jr = new JsonTextReader(sr))
                {
                    var dic = new Dictionary<uint, CollectionStatus>();
                    foreach (var item in JObject.Load(jr)["eps"])
                    {
                        CollectionStatus status;
                        switch (item["status"]["id"].Value<int>())
                        {
                            default:
                            case 1: status = CollectionStatus.Wish; break;
                            case 2: status = CollectionStatus.Collect; break;
                            case 3: status = CollectionStatus.Doing; break;
                            case 4: status = CollectionStatus.On_Hold; break;
                            case 5: status = CollectionStatus.Dropped; break;
                        }
                        dic.Add(item["id"].Value<uint>(), status);
                    }
                    return dic;
                }
            }
        }
    }
}
