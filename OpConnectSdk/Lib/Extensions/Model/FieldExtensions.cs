using OpConnectSdk.Model;

namespace OpConnectSdk.Lib.Extensions.Model
{
    public static class FieldExtensions
    {
        public static FieldDto ToFieldDto(this Field field) => 
            new FieldDto
            {
                Id = field.Id,
                Type = field.Type?.ToString(),
                Purpose =  field.Purpose?.ToString(),
                Label = field.Label,
                Value = field.Value,
                Entropy = field.Entropy,
                Generate = field.Generate,
                Recipe = field.Recipe?.ToRecipeDto(),
                Section = field.Section
            };
    }
}