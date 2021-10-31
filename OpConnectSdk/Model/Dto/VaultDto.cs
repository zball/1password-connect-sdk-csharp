using System;

namespace OpConnectSdk.Model
{
    public class VaultDto

    {
        public long AttributeVersion { get; set; }
        public long ContentVersion { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Description { get; set; }
        public string Id { get; set; }
        public long Items { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

}