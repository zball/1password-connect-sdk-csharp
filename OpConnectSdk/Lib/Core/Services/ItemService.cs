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
    public class ItemService : IItemService
    {
        const string BASE_URL = "/v1/vaults/{vaultUUID}/items";

        private HttpClient _httpClient;

        public ItemService(
            OpClient opClient
        )
        {
            _httpClient = opClient.Client;
        }

        // Example filter: "name eq \"Connect Test\""
        public async Task<Item[]> GetListAsync(string vaultUuid, string filter = null)
        {
            var endpoint = new StringBuilder(BASE_URL).Replace("{vaultUUID}", vaultUuid);

            if(!String.IsNullOrEmpty(filter))
            {
                endpoint.AppendFormat("?filter={0}", filter);
            }

            var response =  await _httpClient.GetAsync(Uri.EscapeUriString(endpoint.ToString()));
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            return Deserialize<Item[]>(json: content);
        }

        public async Task<Item> GetAsync(string vaultUuid, string itemUuid)
        {
            var endpoint = new StringBuilder(BASE_URL)
                .Replace("{vaultUUID}", vaultUuid)
                .AppendFormat("/{0}", itemUuid);

            var response =  await _httpClient.GetAsync(endpoint.ToString());
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            return Deserialize<Item>(json: content);
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
