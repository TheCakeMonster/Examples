using System;
using System.Collections.Generic;
using System.Text;

namespace CslaSerialization.Generators.AutoSerialization
{

	/// <summary>
	/// The definition of a field, extracted from the syntax tree provided by Roslyn
	/// </summary>
	public class ExtractedFieldDefinition
	{

		/// <summary>
		/// The name of the field
		/// </summary>
		public string FieldName { get; set; }

		/// <summary>
		/// The string type name representing the type of this field
		/// </summary>
		public string FieldTypeName { get; set; }

		/// <summary>
		/// Whether this field is of a type that is itself auto serializable
		/// </summary>
		public bool IsTypeAutoSerializable { get; set; } = false;

		/// <summary>
		/// Whether this field is of a type that implements IMobileObject
		/// </summary>
		public bool IsTypeIMobileObject { get; set; } = false;

	}

}
