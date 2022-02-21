using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ComplexJsonSerialization
{

    internal class NoTrailingSlash : ValidationRuleBase, IValidationRule
    {
        #region IValidationRule interface
        
        public override string RuleCode => BuiltInRuleTypes.NoTrailingSlash;

        public JsonConverter<IValidationRule> GetJsonConverter()
        {
            return new BasicValidationRuleConverter();
        }

        #endregion

    }
}
