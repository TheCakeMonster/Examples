using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ComplexJsonSerialization
{
    internal class Template5
    {

        public string TemplateName { get; set; }

        public string TemplateDescription { get; set; }

        [JsonIgnore]
        public List<IValidationRule> ValidationRules { get; private set; } = new List<IValidationRule>();

        [JsonPropertyName("ValidationRules")]
        public JsonElement SerialisedValidationRules { get; set; } = JsonDocument.Parse("[]").RootElement;

    }
}
