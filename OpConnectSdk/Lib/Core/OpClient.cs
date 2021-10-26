using System;
using System.Net.Http;
using System.Net.Http.Headers;
using OpConnectSdk.Model;

namespace OpConnectSdk.Lib.Core
{
    public class OpClient
    {
        public HttpClient Client { get; }

        public OpClient(
            HttpClient client,
            OpConnectOptions options
        )
        {
            Client = client;
            Client.BaseAddress = new Uri(options.BaseUrl);
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Bearer", 
                options.Token
            );
            Client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json")
            );
        }
    }
}