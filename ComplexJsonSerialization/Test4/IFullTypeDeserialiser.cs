namespace ComplexJsonSerialization
{
    internal interface IFullTypeDeserialiser
    {
        ValidationRuleBase Deserialise(ValidationRuleBase baseRule);
    }
}