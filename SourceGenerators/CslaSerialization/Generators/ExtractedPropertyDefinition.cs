using System;
using System.Collections.Generic;
using System.Text;

namespace CslaSerialization.Generators
{

	/// <summary>
	/// The definition of a property, extracted from the syntax tree provided by Roslyn
	/// </summary>
	public class ExtractedPropertyDefinition
	{

		/// <summary>
		/// The name of the property
		/// </summary>
		public string PropertyName { get; set; }

		/// <summary>
		/// The string type name representing the type of this property
		/// </summary>
		public string PropertyTypeName { get; set; }

	}

}
