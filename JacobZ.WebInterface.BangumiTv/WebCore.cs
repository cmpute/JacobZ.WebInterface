using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JacobZ.WebInterface.BangumiTv
{
    /// <summary>
    /// 为bangumi.tv的网页提供抽象接口
    /// </summary>
    public class WebCore
    {
        const string BaseAddress = "https://bgm.tv/";

        /// <summary>
        /// 获取条目详细信息
        /// </summary>
        /// <param name="Id">条目ID</param>
        /// <returns>新的条目对象</returns>
        public static async Task<Subject> GetSubject(uint Id)
        {
            throw new NotImplementedException();
        }
    }
}
