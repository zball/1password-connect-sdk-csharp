using System.Text.Json.Serialization;
using OpConnectSdk.Model.Enums;

namespace OpConnectSdk.Model
{
    public class Field
    {
        public string Id { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public FieldType? Type { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Purpose? Purpose { get; set; }
        public string Label { get; set; }
        public string Value { get; set; }
        public double Entropy { get; set; }
        public bool Generate { get; set; }
        public Recipe Recipe { get; set; }
        public Section Section { get; set; }
    }

}