using OpConnectSdk.Lib.Core.Interfaces;

namespace OpConnectSdk
{
    public class OpConnect: IOpConnect
    {
        public IVaultService Vault { get; set; }
        public IItemService Item { get; set; }

        public OpConnect(
            IVaultService vaultService,
            IItemService itemService
        )
        {
           Vault = vaultService;
           Item = itemService;
        }
    }
}
