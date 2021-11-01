using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using OpConnectSdk.Lib.Core;
using OpConnectSdk.Lib.Core.Interfaces;
using OpConnectSdk.Model;

namespace OpConnectSdk
{
    public class OpConnect: IOpConnect
    {
        private OpClient _httpClient { get; set; }

        public IVaultService Vault { get; set; }
        public IItemService Item { get; set; }

        public OpConnect(
            IVaultService vaultService,
            IItemService itemService,
            OpClient opClient
        )
        {
           Vault = vaultService;
           Item = itemService;
           _httpClient = opClient;
        }

        public virtual async Task<bool> GetHeartbeatAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/heartbeat");
            request.Headers.Accept.Add(
                 new MediaTypeWithQualityHeaderValue("text/plain")
            );

            var response = await _httpClient.Client.SendAsync(request);            

            return await response.Content.ReadAsStringAsync() == ".";
        }

        public virtual async Task<ServerHealth> GetHealthAsync()
        {
            return await _httpClient.GetAsync<ServerHealth>("/health");
        }
    }
}
