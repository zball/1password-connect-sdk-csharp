using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using OpConnectSdk.Lib.Core.Interfaces;
using OpConnectSdk.Model;

namespace OpConnectSdk.Lib.Core.Services
{
    public class VaultService : IVaultService
    {
        const string BASE_URL = "v1/vaults";

        private HttpClient _httpClient;

        public VaultService(
            OpClient opClient
        )
        {
            _httpClient = opClient.Client;
        }

        // Example filter: "name eq \"Connect Test\""
        public async Task<Vault[]> GetListAsync(string filter = null)
        {
            var endpoint = new StringBuilder(BASE_URL);

            if(!String.IsNullOrEmpty(filter))
            {
                endpoint.AppendFormat("?filter={0}", filter);
            }

            var response =  await _httpClient.GetAsync(Uri.EscapeUriString(endpoint.ToString()));
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();            

            return Deserialize<Vault[]>(json: content);
        }

        public async Task<Vault> GetAsync(string vaultUuid)
        {
            var endpoint = new StringBuilder(BASE_URL);
            endpoint.AppendFormat("/{0}", vaultUuid);

            var response =  await _httpClient.GetAsync(endpoint.ToString());
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            Console.WriteLine(content);

            return Deserialize<Vault>(json: content);
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
