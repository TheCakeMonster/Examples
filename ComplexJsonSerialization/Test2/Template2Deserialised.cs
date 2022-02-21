using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ComplexJsonSerialization
{
    internal class Template2Deserialised
    {

        public string TemplateName { get; set; }

        public string TemplateDescription { get; set; }

        [JsonExtensionData]
        public Dictionary<string, JsonElement>? ExtensionData { get; set; }

        [JsonIgnore]
        public string ValidationRulesJson { get; set; }
    }
}
