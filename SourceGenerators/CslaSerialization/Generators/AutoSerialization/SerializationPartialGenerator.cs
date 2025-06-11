﻿using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.Text;

namespace CslaSerialization.Generators.AutoSerialization
{

	/// <summary>
	/// Source generator capable of completing the generation of a single partial class that is
	/// needed to automatically perform mobile serialization of a type in a consuming assembly
	/// </summary>
	internal class SerializationPartialGenerator
	{

		/// <summary>
		/// Generate a partial type to complete a type that has been identified as a target
		/// </summary>
		/// <param name="context">The execution context of generation</param>
		/// <param name="typeDeclaration">The declaration of the type for which to generate code</param>
		public void GeneratePartialType(IncrementalGeneratorInitializationContext context, ExtractedTypeDefinition typeDefinition)
		{
			GenerationResults generationResults;
			SerializationPartialBuilder builder = new SerializationPartialBuilder();

			// Build the text for the generated type using the builder
			generationResults = builder.BuildPartialTypeDefinition(typeDefinition);

            // Add the generated source to the output
            context.RegisterPostInitializationOutput(ctx => ctx.AddSource($"{generationResults.FullyQualifiedName}.g",
				SourceText.From(generationResults.GeneratedSource, Encoding.UTF8)));
		}

	}

}
