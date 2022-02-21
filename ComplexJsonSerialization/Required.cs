using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ComplexJsonSerialization
{

    internal class Required : ValidationRuleBase, IValidationRule
    {
        #region IValidationRule interface
        
        public override string RuleCode => BuiltInRuleTypes.Required;

        public JsonConverter<IValidationRule> GetJsonConverter()
        {
            return new BasicValidationRuleConverter();
        }

        #endregion

    }
}
