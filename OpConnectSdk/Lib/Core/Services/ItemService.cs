using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using OpConnectSdk.Lib.Core.Interfaces;
using OpConnectSdk.Model;

namespace OpConnectSdk.Lib.Core.Services
{
    public class ItemService : ApiService, IItemService
    {
        public const string BASE_URL = "/v1/vaults/{vaultUUID}/items";

        public ItemService(OpClient opClient) : base(opClient){ }

        // Example filter: "name eq \"Connect Test\""
        public async Task<List<Item>> GetListAsync(string vaultUuid, string filter = null)
        {
            var endpoint = new StringBuilder(BASE_URL).Replace("{vaultUUID}", vaultUuid);

            if(!String.IsNullOrEmpty(filter))
            {
                endpoint.AppendFormat("?filter={0}", filter);
            }

           return await _httpClient.GetAsync<List<Item>>(Uri.EscapeUriString(endpoint.ToString()));
        }

        public async Task<Item> GetAsync(string vaultUuid, string itemUuid)
        {
            var endpoint = new StringBuilder(BASE_URL)
                .Replace("{vaultUUID}", vaultUuid)
                .AppendFormat("/{0}", itemUuid);

           return await _httpClient.GetAsync<Item>(endpoint.ToString());
        }
    }
}
