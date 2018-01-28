using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace JacobZ.WebInterface.BangumiTv
{
    /// <summary>
    /// 为bangumi.tv的API提供接口整理
    /// </summary>
    public static class ApiCore
    {
        const string BaseAddress = "https://api.bgm.tv/";

        /// <summary>
        /// 获取条目详细信息
        /// </summary>
        /// <param name="Id">条目ID</param>
        /// <param name="simple">是否仅获取简要信息</param>
        /// <returns>新的条目对象</returns>
        public static async Task<Subject> GetSubject(uint Id, bool simple = true)
        {
            var client = new HttpClient();
            var simpletxt = simple ? "simple" : "large";
            using (var jstream = await client.GetStreamAsync(
                BaseAddress + $"/subject/{Id}?responseGroup={simpletxt}"))
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
    }
}
