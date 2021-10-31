using System.Collections.Generic;
using System.Threading.Tasks;
using OpConnectSdk.Model;

namespace OpConnectSdk.Lib.Core.Interfaces
{
    public interface IItemService
    {
        public Task<List<Item>> GetListAsync(string vaultUuid, string filter = null);
        public Task<Item> GetAsync(string vaultUuid, string itemUuid);

        /// <summary>
        /// Creates Item in a Vault.
        /// Transforms an Item to a CreateItemDto and sends to the 1Password Connect instance.
        /// </summary>
        /// <param name="item">
        /// The Item to be created.
        /// <example>
        /// <code>
///            var item = new Item 
///            {
///                Vault = new Vault {
///                    Id = "rqkbwo4uhwdvk3xrzgv62vmdsq"
///                },
///                Title = "Initital SDK Test - From Code - 4Realz",
///                Category = Category.LOGIN,
///                Sections = new Section[] {
///                    section
///                },
///                Fields = new Field[] {
///                    new Field 
///                    {
///                        Value = "wendy",
///                        Purpose = Purpose.USERNAME
///                    },
///                    new Field
///                    {
///                        Purpose = Purpose.PASSWORD,
///                        Generate = true,
///                        Recipe = new Recipe
///                            {
///                                Length = 55,
///                                CharacterSets = new CharacterSet[] { 
///                                    CharacterSet.LETTERS, 
///                                    CharacterSet.DIGITS 
///                                }
///                            }
///                    },
///                    new Field
///                    {
///                        Type = FieldType.URL,
///                        Label = "Example URL",
///                        Value = "https://www.example.com",
///                        Section = section
///                        
///                    }
///                }
///            };
        /// </code>
        /// </example>
        /// </param>
        /// <returns>Created Item</returns>
        public Task<Item> CreateAsync(Item item);
    }
}