using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.Text;

namespace CslaSerialization.Generators
{

	/// <summary>
	/// Takes responsibility for completing the generation of a single partial class that is
	/// needed to automatically perform mobile serialization of a type in a consuming assembly
	/// </summary>
	public class SerializationPartialGenerator
	{

		/// <summary>
		/// Generate a partial class to complete a type that has been identified
		/// </summary>
		/// <param name="context">The execution context of generation</param>
		/// <param name="typeDeclaration">The declaration syntax of the type for which to generate</param>
		public void GeneratePartialClass(GeneratorExecutionContext context, TypeDeclarationSyntax typeDeclaration)
		{
			ExtractedClassDefinition classDefinition;
			GenerationResults generatedClassDefinition;
			SerializationPartialBuilder builder = new SerializationPartialBuilder();

			// Extract the definition from the declaration provided by the environment
			classDefinition = ClassDefinitionExtractor.ExtractClassDefinition(context, typeDeclaration);

			// Build the generated class using the builder
			generatedClassDefinition = builder.BuildPartialClass(classDefinition);

			// Add the generated source to the output
			context.AddSource($"SerializerPartialGenerator_{classDefinition.ClassName}",
				SourceText.From(generatedClassDefinition.GeneratedSource, Encoding.UTF8));
		}

	}

}
