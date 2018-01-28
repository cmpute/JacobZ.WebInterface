using System;
using System.Collections.Generic;
using System.Net.Http;

namespace JacobZ.WebInterface.BangumiTv
{
    public static class ClientManager
    {
        private static HttpClient _client;
        private static bool _disposed;

        private static void CreateClient()
        {
            _client = new HttpClient();
            _disposed = false;
        }

        public static HttpClient Client
        {
            get
            {
                if (_client == null || _disposed)
                    CreateClient();
                return _client;
            }
        }
    }
}
