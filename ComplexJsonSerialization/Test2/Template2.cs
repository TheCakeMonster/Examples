using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ComplexJsonSerialization
{
    internal class Template2
    {

        public string TemplateName { get; set; }

        public string TemplateDescription { get; set; }

        public IList<IValidationRule> ValidationRules { get; private set; } = new List<IValidationRule>();

        [JsonExtensionData]
        public Dictionary<string, JsonElement>? ExtensionData { get; set; }

    }
}
