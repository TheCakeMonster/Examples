using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace ComplexJsonSerialization
{
    internal class FullTypeDeserialiser<T> : IFullTypeDeserialiser where T : ValidationRuleBase
    {
        private static readonly JsonSerializerOptions _serializerOptions = LoadJsonSerializerOptions();

        private static JsonSerializerOptions LoadJsonSerializerOptions()
        {
            JsonSerializerOptions serializerOptions;

            serializerOptions = new JsonSerializerOptions();

            return serializerOptions;
        }

        public ValidationRuleBase Deserialise(ValidationRuleBase baseRule)
        {
            string serializedRule;
            T fullyTypedRule;

            // Reserialize base type back to full Json, then deserialize as correct, fully typed, final type
            serializedRule = JsonSerializer.Serialize(baseRule, _serializerOptions);
            fullyTypedRule = JsonSerializer.Deserialize<T>(serializedRule, _serializerOptions);

            return fullyTypedRule;
        }
    }
}
