﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CslaSerialization.Generators.AutoSerialization
{

	/// <summary>
	/// The results of source generation
	/// </summary>
	public class GenerationResults
	{

		/// <summary>
		/// The fully qualified name of the generated type
		/// </summary>
		public string FullyQualifiedName { get; set; }

		/// <summary>
		/// The source code that has been generated by the builder
		/// </summary>
		public string GeneratedSource { get; set; }

	}

}
