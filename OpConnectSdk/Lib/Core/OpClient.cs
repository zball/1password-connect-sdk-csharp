using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
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

        public virtual async Task<T> GetAsync<T>(string endpoint)
        {
            var response =  await Client.GetAsync(endpoint.ToString());
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            return Deserialize<T>(json: content);
        }

        public virtual async Task<TResult> PostAsync<T, TResult>(string endpoint, T resource)
        {
            var data = new StringContent(Serialize(resource), Encoding.UTF8, "application/json");

            var response =  await Client.PostAsync(endpoint.ToString(), data);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            return Deserialize<TResult>(json: content);
        }

        public virtual async Task<bool> DeleteAsync(string endpoint)
        {
            var response =  await Client.DeleteAsync(endpoint.ToString());
            response.EnsureSuccessStatusCode();

            return true;
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

        private string Serialize<T>(T resource)
        {
            return JsonSerializer.Serialize(
                resource, 
                new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                }
            );
        }

        #endregion Private Methods
    }
}