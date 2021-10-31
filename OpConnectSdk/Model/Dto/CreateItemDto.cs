namespace OpConnectSdk.Model
{
    public class CreateItemDto
    {
        public VaultDto Vault { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string[] Tags { get; set; }
        public Section[] Sections { get; set; }
        public FieldDto[] Fields { get; set; }
    }

}