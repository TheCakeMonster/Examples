using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ComplexJsonSerialization
{
    internal class ValidationRuleJsonConverter : JsonConverter<IValidationRule>
    {
        private static readonly IReadOnlyDictionary<string, Type> _ruleTypes = LoadTypesDictionary();

        private static IReadOnlyDictionary<string, Type> LoadTypesDictionary()
        {
            Dictionary<string, Type> ruleTypes = new Dictionary<string, Type>();

            ruleTypes.Add(BuiltInRuleTypes.Required, typeof(Required));
            ruleTypes.Add(BuiltInRuleTypes.NoTrailingSlash, typeof(NoTrailingSlash));
            ruleTypes.Add(BuiltInRuleTypes.CharacterSet, typeof(CharacterSet));

            return ruleTypes;
        }

        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert == typeof(IValidationRule);
        }

        public override IValidationRule Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            reader.Read();
            if (reader.TokenType != JsonTokenType.PropertyName)
            {
                throw new JsonException();
            }

            string? propertyName = reader.GetString();
            if (propertyName != "RuleCode")
            {
                throw new JsonException();
            }

            reader.Read();
            if (reader.TokenType != JsonTokenType.String)
            {
                throw new JsonException();
            }

            string ruleCode = reader.GetString();

            if (!_ruleTypes.ContainsKey(ruleCode))
            {
                throw new JsonException();
            }
            return ReadValidationRule(ruleCode, ref reader, options);
        }

        public override void Write(Utf8JsonWriter writer, IValidationRule value, JsonSerializerOptions options)
        {
            JsonConverter<IValidationRule> converter;

            converter = value.GetJsonConverter();
            converter.Write(writer, value, options);
        }

        #region Private Helper Methods

        private IValidationRule ReadValidationRule(string ruleCode, ref Utf8JsonReader reader, JsonSerializerOptions options)
        {
            Type ruleType;
            IValidationRule rule;
            JsonConverter<IValidationRule> converter;

            if (!_ruleTypes.ContainsKey(ruleCode))
            {
                throw new JsonException();
            }
            ruleType = _ruleTypes[ruleCode];
            rule = (IValidationRule)Activator.CreateInstance(ruleType);

            converter = rule.GetJsonConverter();

            rule = converter.Read(ref reader, ruleType, options);
            return rule;
        }

        //private void WriteValidationRule<T>(T ruleType, IValidationRule value, Utf8JsonWriter writer, JsonSerializerOptions options) where T: class, IValidationRule
        //{
        //    JsonConverter<T> converter;
        //    Type converterType;

        //    if (!_converterTypes.ContainsKey(value.RuleCode))
        //    {
        //        throw new JsonException();
        //    }
        //    converterType = _converterTypes[value.RuleCode].RuleConverterType;
        //    converter = (JsonConverter<T>)Activator.CreateInstance(converterType);

        //    converter.Write(writer, (T)value, options);
        //}

        #endregion

    }
}
