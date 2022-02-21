using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace ComplexJsonSerialization
{
    internal class Template1Serialiser
    {
        private readonly JsonSerializerOptions _serializerOptions = LoadJsonSerializerOptions();
        private readonly ValidationRuleSerialiser _validationRuleSerialiser;

        private static JsonSerializerOptions LoadJsonSerializerOptions()
        {
            JsonSerializerOptions serializerOptions;

            serializerOptions = new JsonSerializerOptions();
            serializerOptions.WriteIndented = true;

            return serializerOptions;
        }

        public Template1Serialiser(ValidationRuleSerialiser validationRuleSerialiser)
        {
            _validationRuleSerialiser = validationRuleSerialiser;
        }

        public string Serialise(Template1 template, IList<IValidationRule> validationRules)
        {
            string rules;

            rules = _validationRuleSerialiser.Serialise(validationRules);
            template.ValidationRules = rules;

            return JsonSerializer.Serialize(template, _serializerOptions);
        }
    }
}
