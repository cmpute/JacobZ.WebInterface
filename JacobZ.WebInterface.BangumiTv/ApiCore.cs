using System;
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

        /// <summary>
        /// 获取条目详细信息
        /// </summary>
        /// <param name="Id">条目ID</param>
        /// <param name="simple">是否仅获取简要信息</param>
        /// <returns>新的条目对象</returns>
        public static async Task<Subject> GetSubject(uint Id, bool simple = true)
        {
            throw new NotImplementedException();
        }
    }
}
