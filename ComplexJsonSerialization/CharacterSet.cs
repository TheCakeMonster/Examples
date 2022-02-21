using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ComplexJsonSerialization
{

    internal class CharacterSet : ValidationRuleBase, IValidationRule
    {

        #region IValidationRule interface

        public override string RuleCode => BuiltInRuleTypes.CharacterSet;

        public JsonConverter<IValidationRule> GetJsonConverter()
        {
            return new CharacterSetJsonConverter();
        }

        #endregion

        public string CharacterSetCode { get; set; } = "Blah";

    }
}
