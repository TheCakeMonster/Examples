using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ComplexJsonSerialization
{
    internal class CharacterSetJsonConverter : JsonConverter<IValidationRule>
    {
        public override IValidationRule Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string propertyName;
            CharacterSet rule = new CharacterSet();

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    return rule;
                }

                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    propertyName = reader.GetString();
                    reader.Read();
                    switch (propertyName)
                    {
                        case "CharacterSetCode":
                            rule.CharacterSetCode = reader.GetString();
                            break;
                    }
                }
            }

            throw new JsonException();
        }

        public override void Write(Utf8JsonWriter writer, IValidationRule value, JsonSerializerOptions options)
        {
            CharacterSet characterSet;

            characterSet = (CharacterSet)value;
            writer.WriteStartObject();
            writer.WriteString("RuleCode", characterSet.RuleCode);
            writer.WriteString("CharacterSetCode", characterSet.CharacterSetCode);
            writer.WriteEndObject();
        }
    }
}
