using System;
using System.Collections.Generic;
using System.Text;

namespace CslaSerialization.Generators.AutoSerialization
{

	/// <summary>
	/// The definition of a class, extracted from the syntax tree provided by Roslyn
	/// </summary>
	public class ExtractedClassDefinition
	{

		/// <summary>
		/// The namespace in which the class resides
		/// </summary>
		public string Namespace { get; set; }

		/// <summary>
		/// The scope of the class
		/// </summary>
		public string Scope { get; set; } = "public";

		/// <summary>
		/// The name of the class, excluding any namespace
		/// </summary>
		public string ClassName { get; set; }

		/// <summary>
		/// The properties to be included in serialization
		/// </summary>
		public IList<ExtractedPropertyDefinition> Properties { get; private set; } = new List<ExtractedPropertyDefinition>();

	}

}
