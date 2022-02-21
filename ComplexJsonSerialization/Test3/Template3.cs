using System;
using System.Collections.Generic;
using System.Text;

namespace ComplexJsonSerialization
{
    internal class Template3
    {

        public string TemplateName { get; set; }

        public string TemplateDescription { get; set; }

        public IList<IValidationRule> ValidationRules { get; set; } = new List<IValidationRule>();

    }
}
