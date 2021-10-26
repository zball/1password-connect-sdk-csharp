using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
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

        public async Task<T> GetAsync<T>(string endpoint)
        {
            var response =  await Client.GetAsync(endpoint.ToString());
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            return Deserialize<T>(json: content);
        }

        #region Private Methods

        private T Deserialize<T>(string json)
        {
            return JsonSerializer.Deserialize<T>(
                json, 
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }

        #endregion Private Methods
    }
}