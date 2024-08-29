using Newtonsoft.Json.Linq;

namespace NewtonsoftComparison;

internal class Program
{
    static void Main(string[] args)
    {
        var jProperty = new JProperty("Property1", "Value1");

        var value1 = GetValue1(jProperty);
        var value2 = GetValue2(jProperty);

        if (value1 is not null && value1.Equals(value2))
        {
            Console.WriteLine("They match.");
        }
        else
        {
            Console.WriteLine("They don't match.");
        }

        Console.WriteLine("Press a key to exit");
        Console.ReadKey();
    }

    private static object? GetValue1(JProperty jProperty)
         => jProperty.Value.Type switch
         {
             JTokenType.Null => (object?)null,
             JTokenType.Undefined => (object?)null,
             JTokenType.String => jProperty.Value.ToObject<string>(),
             JTokenType.Integer => jProperty.Value.ToObject<int>(),
             JTokenType.Float => jProperty.Value.ToObject<double>(),
             JTokenType.Boolean => jProperty.Value.ToObject<bool>(),
             _ => jProperty.Value
         };

    private static object? GetValue2(JProperty jProperty)
         => jProperty.Value.Type switch
         {
             JTokenType.Null => null,
             JTokenType.Undefined => null,
             JTokenType.String => jProperty.Value.ToObject<string>(),
             JTokenType.Integer => jProperty.Value.ToObject<int>(),
             JTokenType.Float => jProperty.Value.ToObject<double>(),
             JTokenType.Boolean => jProperty.Value.ToObject<bool>(),
             _ => jProperty.Value
         };
}
