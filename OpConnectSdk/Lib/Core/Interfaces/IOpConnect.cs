using System.Threading.Tasks;
using OpConnectSdk.Model;

namespace OpConnectSdk.Lib.Core.Interfaces
{
    public interface IOpConnect
    {
        IVaultService Vault { get; set; }
        IItemService Item { get; set; }
        Task<bool> GetHeartbeatAsync();
        Task<ServerHealth> GetHealthAsync();
    }
}