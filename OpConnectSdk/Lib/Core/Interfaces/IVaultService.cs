using System.Threading.Tasks;
using OpConnectSdk.Model;

namespace OpConnectSdk.Lib.Core.Interfaces
{
    public interface IVaultService
    {
        public Task<Vault[]> GetListAsync(string filter = null);
        public Task<Vault> GetAsync(string vaultUuid);
    }
}