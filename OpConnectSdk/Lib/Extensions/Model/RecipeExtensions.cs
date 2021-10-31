using System.Linq;
using OpConnectSdk.Model;

namespace OpConnectSdk.Lib.Extensions.Model
{
    public static class RecipeExtensions
    {
        public static RecipeDto ToRecipeDto(this Recipe recipe) => 
            new RecipeDto
            {
                Length = recipe.Length,
                CharacterSets = recipe.CharacterSets.Select(e => e.ToString()).ToArray()
            };
    }
}