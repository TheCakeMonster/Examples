using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace ComplexJsonSerialization
{
    internal class Template5Serialiser
    {
        private static readonly JsonSerializerOptions _serializeOptions = LoadJsonSerializeOptions();
        private static readonly JsonSerializerOptions _deserializeOptions = LoadJsonDeserializeOptions();

        private static JsonSerializerOptions LoadJsonSerializeOptions()
        {
            JsonSerializerOptions serializerOptions;

            serializerOptions = new JsonSerializerOptions();
            serializerOptions.WriteIndented = true;

            return serializerOptions;
        }

        private static JsonSerializerOptions LoadJsonDeserializeOptions()
        {
            JsonSerializerOptions serializerOptions;

            serializerOptions = new JsonSerializerOptions();
            serializerOptions.WriteIndented = true;

            return serializerOptions;
        }

        public string Serialise(Template5 template)
        {
            string validationRuleText;
            List<object> validationRules;

            validationRules = template.ValidationRules.Select(t => (object)t).ToList();

            validationRuleText = JsonSerializer.Serialize(validationRules, _serializeOptions);
            template.SerialisedValidationRules = JsonDocument.Parse(validationRuleText).RootElement;
            return JsonSerializer.Serialize(template, _serializeOptions);
        }

        public Template5 Deserialise(string templateText)
        {
            Template5 deserialisedTemplate;
            deserialisedTemplate = JsonSerializer.Deserialize<Template5>(templateText, _deserializeOptions);

            return deserialisedTemplate;
        }


    }
}
