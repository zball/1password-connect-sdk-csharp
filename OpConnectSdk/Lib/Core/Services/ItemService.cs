using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using OpConnectSdk.Lib.Core.Interfaces;
using OpConnectSdk.Lib.Extensions.Model;
using OpConnectSdk.Model;

namespace OpConnectSdk.Lib.Core.Services
{
    public class ItemService : ApiService, IItemService
    {
        public const string ERROR_NO_VAULT_ID = "Item Vault Id can not be null";

        public const string BASE_URL = "/v1/vaults/{vaultUUID}/items";

        public ItemService(OpClient opClient) : base(opClient){ }

        // Example filter: "name eq \"Connect Test\""
        public async Task<List<Item>> GetListAsync(string vaultId, string filter = null)
        {
            var endpoint = new StringBuilder(BASE_URL).Replace("{vaultUUID}", vaultId);

            if(!String.IsNullOrEmpty(filter))
            {
                endpoint.AppendFormat("?filter={0}", filter);
            }

           return await _httpClient.GetAsync<List<Item>>(Uri.EscapeUriString(endpoint.ToString()));
        }

        public async Task<Item> GetAsync(string vaultId, string itemId)
        {
            var endpoint = new StringBuilder(BASE_URL)
                .Replace("{vaultUUID}", vaultId)
                .AppendFormat("/{0}", itemId);

           return await _httpClient.GetAsync<Item>(endpoint.ToString());
        }

        public async Task<Item> CreateAsync(Item item)
        {
            if(
                String.IsNullOrEmpty(item.Vault?.Id) 
                || String.IsNullOrWhiteSpace(item.Vault?.Id))
            {
                throw new ArgumentException($"ItemService.CreateAsync: {ERROR_NO_VAULT_ID}");
            }

            var endpoint = new StringBuilder(BASE_URL)
                .Replace("{vaultUUID}", item.Vault.Id);

            //TODO: Add some validation
            var itemDto = item.ToCreateItemDto();

           return await _httpClient.PostAsync<CreateItemDto, Item>(endpoint.ToString(), itemDto);
        }

        public async Task<bool> DeleteAsync(string vaultId, string itemId)
        {
            var endpoint = new StringBuilder(BASE_URL)
                .Replace("{vaultUUID}", vaultId)
                .AppendFormat("/{0}", itemId);

            return await _httpClient.DeleteAsync(endpoint.ToString());
        }
        
    }
}
