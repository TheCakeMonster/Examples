# Test 2 - ExtensionData

This test is uses a catch-all ExtensionData property to retrieve unrecognised Json.

# Warning
This may work only because of the private setter on Template2.ValidationRules, which 
stops the deserialization of the ValidationRules property, I think. That was accidental,
but is well worth knowing about. This is handy because excluding a property excludes it 
from both serialization and deserialization; using a private getter or setter allows it 
to work for one but not the other.

# Result
Interesting. This does allow the import of Json data in the sort of format I was after
However, I then wondered what pther possibiilities there were. Onwards ...