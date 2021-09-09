using System;
using System.Collections.Generic;
using System.Text;

namespace CslaSerialization.Generators.AutoSerialization
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

		/// <summary>
		/// Whether this property is of a type that is itself auto serializable
		/// </summary>
		public bool IsAutoSerializable { get; set; } = false;

		/// <summary>
		/// Whether this property is of a type that implements IMobileObject
		/// </summary>
		public bool IsIMobileObject { get; set; } = false;

	}

}
