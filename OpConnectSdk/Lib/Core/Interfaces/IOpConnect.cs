namespace OpConnectSdk.Lib.Core.Interfaces
{
    public interface IOpConnect
    {
        IVaultService Vault { get; set; }
        IItemService Item { get; set; }
    }
}