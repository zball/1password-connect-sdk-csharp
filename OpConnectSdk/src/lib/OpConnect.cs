using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using OpConnectSdk.Model;

namespace OpConnectSdk
{
    public class OpConnect
    {
        private HttpClient _httpClient;

        public OpConnect(string baseUrl, string token)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(baseUrl);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Bearer", 
                token
            );
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json")
            );
        }

        // Example filter: "name eq \"Connect Test\""
        public async Task<Vault[]> GetVaultsAsync(string filter = null)
        {
            var endpoint = new StringBuilder("v1/vaults");

            if(!String.IsNullOrEmpty(filter))
            {
                endpoint.AppendFormat("?filter={0}", filter);
            }

            var response =  await _httpClient.GetAsync(Uri.EscapeUriString(endpoint.ToString()));
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            return Deserialize<Vault[]>(json: content);
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
