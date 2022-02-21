# Test 4 - Base Class and then 2 step Deserialization

This test uses child types that subclass a base type. The base type contains 
the property necessary to recognise the types, and then a second deserialisation step
is used to convert the whole thing back to Json and then deserialise as the correct type.

I've used separate types for serialise and deserialise to get what I want; I suspect in 
hindsight that it would be possible to avoid this using a private setter on one list property
and a private getter on the other - they need to be different types, you see. To output all of 
the properties, you need to output a `List<object>`, otherwise only the properties on the base 
type are serialised, for safety. Deserialising instead requires a `List<ValidationRuleBase>` 
with an ExtensionData property to deserialise enough to get the code, with all of the other 
properties bundled up into the catch-all ExtensionData property.


# Result
Not too shabby. This works pretty well; I'd be happy using this technique for a real 
application with a polymorphic list of items. Seems safe as only known types are deserialised
and it should be _reasonably_ efficient; not perfect, but not too bad.