using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace ComplexJsonSerialization
{
    internal class Template3Serialiser
    {
        private static readonly JsonSerializerOptions _serializerOptions = LoadJsonSerializerOptions();

        private static JsonSerializerOptions LoadJsonSerializerOptions()
        {
            JsonSerializerOptions serializerOptions;

            serializerOptions = new JsonSerializerOptions();
            serializerOptions.WriteIndented = true;
            serializerOptions.Converters.Add(new ValidationRuleJsonConverter());

            return serializerOptions;
        }

        public Template3Serialiser()
        {
        }

        public string Serialise(Template3 template)
        {
            return JsonSerializer.Serialize(template, _serializerOptions);
        }

        public Template3 Deserialise(string templateText)
        {
            return JsonSerializer.Deserialize<Template3>(templateText, _serializerOptions);
        }
    }
}
