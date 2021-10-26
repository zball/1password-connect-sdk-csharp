using System.Threading.Tasks;
using OpConnectSdk.Model;

namespace OpConnectSdk.Lib.Core.Interfaces
{
    public interface IItemService
    {
        public Task<Item[]> GetListAsync(string vaultUuid, string filter = null);
        public Task<Item> GetAsync(string vaultUuid, string itemUuid);
    }
}