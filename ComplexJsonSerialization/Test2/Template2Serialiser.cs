using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace ComplexJsonSerialization
{
    internal class Template2Serialiser
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

        public string Serialise(Template2 template)
        {
            return JsonSerializer.Serialize(template, _serializeOptions);
        }

        public Template2Deserialised Deserialise(string templateText)
        {
            Template2Deserialised deserialisedTemplate;
            deserialisedTemplate = JsonSerializer.Deserialize<Template2Deserialised>(templateText, _deserializeOptions);

            var rules = deserialisedTemplate.ExtensionData["ValidationRules"];
            deserialisedTemplate.ValidationRulesJson = JsonSerializer.Serialize(rules, _deserializeOptions);

            return deserialisedTemplate;
        }


    }
}
