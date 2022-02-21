using System;
using System.Collections.Generic;
using System.Text;

namespace ComplexJsonSerialization
{
    internal class FullTypeDeserialiser
    {
        public ValidationRuleBase Deserialise(ValidationRuleBase baseRule)
        {
            IFullTypeDeserialiser fullTypeDeserialiser;
            FullTypeDeserialiserFactory fullTypeDeserialiserFactory;
            ValidationRuleBase fullyTypedRule;

            fullTypeDeserialiserFactory = new FullTypeDeserialiserFactory();
            fullTypeDeserialiser = fullTypeDeserialiserFactory.CreateFullTypeDeserialiser(baseRule);
            fullyTypedRule = fullTypeDeserialiser.Deserialise(baseRule);

            return fullyTypedRule;
        }
    }
}
