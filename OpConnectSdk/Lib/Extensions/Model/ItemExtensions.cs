using System.Linq;
using OpConnectSdk.Model;

namespace OpConnectSdk.Lib.Extensions.Model
{
    public static class ItemExtensions
    {
        public static CreateItemDto ToCreateItemDto(this Item item) => 
            new CreateItemDto
            {
                Id = item.Id,
                Title = item.Title,
                Tags = item.Tags,
                Vault = item.Vault.ToVaultDto(),
                Category = item.Category.ToString(),
                Sections = item.Sections,
                Fields = item.Fields?.Select(f => f.ToFieldDto()).ToArray()
            };
    }
}