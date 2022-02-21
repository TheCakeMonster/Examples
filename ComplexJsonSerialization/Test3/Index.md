# Test 3 - Custom JsonConverter

This test uses a custom JsonConverter for deserializing instances of an interface.

# Result
It works, but the code inside the converter is pretty ugly; you have to deserialise each 
individual property by hand, and more fool you if you forget one!

Not a great option.