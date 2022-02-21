# Test 5 - JsonElement

This test uses JsonElement to represent a subset of Json.

# Result
Not awful. JsonElement is read-only. What we really want is JsonArray - *but* - 
and it's a big but - that's only available in .NET 5 or 6, and not 3.1

There is a slightly messy bit here; the rules are serialised, deserialised and 
then eventually reserialised during the serialisation process! However, in usage 
scenarios that need to be more efficient in deserialisation than serialisation, that 
might not be the end of the world.