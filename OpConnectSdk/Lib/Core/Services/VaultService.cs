using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using OpConnectSdk.Lib.Core.Interfaces;
using OpConnectSdk.Model;

namespace OpConnectSdk.Lib.Core.Services
{
    public class VaultService : ApiService, IVaultService
    {
        public const string BASE_URL = "v1/vaults";

        public VaultService(OpClient opClient) : base(opClient){ }

        // Example filter: "name eq \"Connect Test\""
        public virtual async Task<List<Vault>> GetListAsync(string filter = null)
        {
            var endpoint = new StringBuilder(BASE_URL);

            if(!String.IsNullOrEmpty(filter))
            {
                endpoint.AppendFormat("?filter={0}", filter);
            }

            return await _httpClient.GetAsync<List<Vault>>(Uri.EscapeUriString(endpoint.ToString()));
        }

        public virtual async Task<Vault> GetAsync(string vaultUuid)
        {
            var endpoint = new StringBuilder(BASE_URL);
            endpoint.AppendFormat("/{0}", vaultUuid);

            return await _httpClient.GetAsync<Vault>(endpoint.ToString());
        }
    }
}
