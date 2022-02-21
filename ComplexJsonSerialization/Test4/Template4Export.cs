using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ComplexJsonSerialization
{
    internal class Template4Export : Template4
    {

        public IList<object> ValidationRules { get; set; } = new List<object>();

    }
}
