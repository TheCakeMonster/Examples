using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace ComplexJsonSerialization
{
    internal class ValidationRuleSerialiser
    {
        private static readonly JsonSerializerOptions _serializerOptions = LoadJsonSerializerOptions();

        private static JsonSerializerOptions LoadJsonSerializerOptions()
        {
            JsonSerializerOptions serializerOptions;

            serializerOptions = new JsonSerializerOptions();
            serializerOptions.WriteIndented = true;

            return serializerOptions;
        }

        public string Serialise(IList<IValidationRule> validationRules)
        {
            return JsonSerializer.Serialize(validationRules, _serializerOptions);
        }
    }
}
