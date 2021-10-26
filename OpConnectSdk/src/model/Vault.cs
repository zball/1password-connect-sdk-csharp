using System;
using System.Text.Json.Serialization;

namespace OpConnectSdk.Model
{
    public class Vault
    {
        public long AttributeVersion { get; set; }
        public long ContentVersion { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Description { get; set; }
        public string Id { get; set; }
        public long Items { get; set; }
        public string Name { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public VaultType Type { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

}