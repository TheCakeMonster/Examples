using System;
using System.Collections.Generic;
using System.Text;

namespace ComplexJsonSerialization
{
    internal class FullTypeDeserialiserFactory
    {

        public IFullTypeDeserialiser CreateFullTypeDeserialiser(ValidationRuleBase baseRule)
        {
            switch (baseRule.RuleCode)
            {
                case BuiltInRuleTypes.Required:
                    return new FullTypeDeserialiser<Required>();
                case BuiltInRuleTypes.NoTrailingSlash:
                    return new FullTypeDeserialiser<NoTrailingSlash>();
                case BuiltInRuleTypes.CharacterSet:
                    return new FullTypeDeserialiser<CharacterSet>();
                default:
                    throw new ArgumentOutOfRangeException("Requested rule type is not known!");
            }
        }
    }
}
