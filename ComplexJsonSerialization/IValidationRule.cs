using System.Text.Json.Serialization;

namespace ComplexJsonSerialization
{
    internal interface IValidationRule
    {

        string RuleCode { get; }

        JsonConverter<IValidationRule> GetJsonConverter();

    }
}