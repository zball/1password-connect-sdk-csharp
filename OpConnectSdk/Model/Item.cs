using System;
using System.Text.Json.Serialization;
using OpConnectSdk.Model.Enums;

namespace OpConnectSdk.Model
{
    public class Item
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string[] Tags { get; set; }
        public Vault Vault { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Category Category { get; set; }
        public Section[] Sections { get; set; }
        public Field[] Fields { get; set; }
        //public File[] Files { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool Favorite { get; set; }
    }

}