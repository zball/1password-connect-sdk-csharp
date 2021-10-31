namespace OpConnectSdk.Model
{
    public class FieldDto
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public string Purpose { get; set; }
        public string Label { get; set; }
        public string Value { get; set; }
        public double Entropy { get; set; }
        public bool Generate { get; set; }
        public RecipeDto Recipe { get; set; }
        public Section Section { get; set; }
    }

}