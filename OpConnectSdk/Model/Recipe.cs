using System.Text.Json.Serialization;
using OpConnectSdk.Model.Enums;

namespace OpConnectSdk.Model
{
    public class Recipe
    {
        public int Length { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public CharacterSet[] CharacterSets { get; set; }
    }

}