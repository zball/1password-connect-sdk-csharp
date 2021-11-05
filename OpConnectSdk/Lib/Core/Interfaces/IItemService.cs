using System.Collections.Generic;
using System.Threading.Tasks;
using OpConnectSdk.Model;

namespace OpConnectSdk.Lib.Core.Interfaces
{
    public interface IItemService
    {
        public Task<List<Item>> GetListAsync(string vaultId, string filter = null);
        public Task<Item> GetAsync(string vaultId, string itemId);

        /// <summary>
        /// Creates Item in a Vault.
        /// Transforms an Item to a CreateItemDto and sends to the 1Password Connect instance.
        /// </summary>
        /// <param name="item">
        /// The Item to be created.
        /// <example>
        /// <code>
        /// var item = new Item 
        /// {
        ///     Vault = new Vault {
        ///         Id = "VAULT_ID"
        ///     },
        ///     Title = "Sample Item Title",
        ///     Category = Category.LOGIN,
        ///     Sections = new Section[] {
        ///         new Section 
        ///         {
        ///             Label = "Brand New Section",
        ///             Id = 52d74740-b4fe-471c-b688-7c4e6b73c35e 
        ///         }
        ///     },
        ///     Fields = new Field[] {
        ///         new Field 
        ///         {
        ///             Value = "wendy",
        ///             Purpose = Purpose.USERNAME
        ///         },
        ///         new Field
        ///         {
        ///             Purpose = Purpose.PASSWORD,
        ///             Generate = true,
        ///             Recipe = new Recipe
        ///             {
        ///                 Length = 55,
        ///                 CharacterSets = new CharacterSet[] { 
        ///                     CharacterSet.LETTERS, 
        ///                     CharacterSet.DIGITS 
        ///                 }
        ///             }
        ///         },
        ///         new Field
        ///         {
        ///             Type = FieldType.URL,
        ///             Label = "Example URL",
        ///             Value = "https://www.example.com",
        ///             Section = new Section { Id = 52d74740-b4fe-471c-b688-7c4e6b73c35e }
        ///         }
        ///     }
        /// };
        /// </code>
        /// </example>
        /// </param>
        /// <returns>Created Item</returns>
        public Task<Item> CreateAsync(Item item);
        public Task<bool> DeleteAsync(string vaultId, string itemId);
        public Task<Item> ReplaceAsync(Item item);
    }
}