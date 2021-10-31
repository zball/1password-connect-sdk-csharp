using OpConnectSdk.Model;

namespace OpConnectSdk.Lib.Extensions.Model
{
    public static class VaultExtensions
    {
        public static VaultDto ToVaultDto(this Vault vault) => 
            new VaultDto
            {
                AttributeVersion = vault.AttributeVersion,
                ContentVersion = vault.ContentVersion,
                CreatedAt = vault.CreatedAt,
                Description = vault.Description,
                Id = vault.Id,
                Items = vault.Items,
                Name = vault.Name,
                Type = vault.Type.ToString(),
                UpdatedAt = vault.UpdatedAt
            };
    }
}