using System;
using System.Collections.Generic;
using System.Text;

namespace ComplexJsonSerialization
{
    internal class Template4Import : Template4
    {
        public IList<ValidationRuleBase> ValidationRules { get; set; } = new List<ValidationRuleBase>();

    }
}
