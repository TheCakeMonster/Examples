using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ComplexJsonSerialization
{
    internal class ValidationRuleBase
    {

        public virtual string RuleCode { get; set; }

        [JsonExtensionData]
        public Dictionary<string, JsonElement>? ExtensionData { get; set; }

    }
}
