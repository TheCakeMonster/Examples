using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace ComplexJsonSerialization
{
    internal class Template4Serialiser
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

        public string Serialise(Template4Export template)
        {
            return JsonSerializer.Serialize(template, _serializerOptions);
        }

        public Template4Import Deserialise(string templateText)
        {
            Template4Import template;
            ValidationRuleBase baseRule;
            ValidationRuleBase fullyTypedRule;
            FullTypeDeserialiser fullTypeDeserialiser = new FullTypeDeserialiser();

            // Do initial deserialisation
            template = JsonSerializer.Deserialize<Template4Import>(templateText, _serializerOptions);

            // Now iterate through each validation rule, completing its deserialisation
            for (int index = 0; index < template.ValidationRules.Count; index++)
            {
                baseRule = template.ValidationRules[index];
                fullyTypedRule = fullTypeDeserialiser.Deserialise(baseRule);
                template.ValidationRules[index] = fullyTypedRule;
            }

            return template;
        }
    }
}
