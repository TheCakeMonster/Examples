using System;
using System.Collections.Generic;
using System.Text;

namespace CslaSerialization.Generators.AutoSerialization
{

	/// <summary>
	/// The definition of a field, extracted from the syntax tree provided by Roslyn
	/// </summary>
	public class ExtractedFieldDefinition : IMemberDefinition
	{

		/// <summary>
		/// The name of the field
		/// </summary>
		public string FieldName { get; set; }

		/// <summary>
		/// The definition of the type of this field
		/// </summary>
		public ExtractedTypeDefinition TypeDefinition { get; } = new ExtractedTypeDefinition();

		/// <summary>
		/// The member name for the field
		/// </summary>
		string IMemberDefinition.MemberName => FieldName;

	}

}
