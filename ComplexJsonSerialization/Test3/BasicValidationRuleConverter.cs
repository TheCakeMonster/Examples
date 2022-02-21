using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ComplexJsonSerialization
{
    internal class BasicValidationRuleConverter : JsonConverter<IValidationRule>
    {
        public override IValidationRule Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            IValidationRule rule;

            rule = (IValidationRule)Activator.CreateInstance(typeToConvert);

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    return rule;
                }
            }

            throw new JsonException();
        }

        public override void Write(Utf8JsonWriter writer, IValidationRule value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteString("RuleCode", value.RuleCode);
            writer.WriteEndObject();
            // Nothing else to write
            return;
        }
    }
}
