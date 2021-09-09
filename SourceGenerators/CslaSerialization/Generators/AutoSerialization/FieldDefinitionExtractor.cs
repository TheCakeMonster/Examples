using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Text;

namespace CslaSerialization.Generators.AutoSerialization
{

	/// <summary>
	/// Extract the definition of a single field of a class for which code generation is being performed
	/// This is used to detach the builder from the Roslyn infrastructure, to enable testing
	/// </summary>
	public static class FieldDefinitionExtractor
	{

		/// <summary>
		/// Extract information about a single property from its declaration in the syntax tree
		/// </summary>
		/// <param name="extractionContext">The definition extraction context in which the extraction is being performed</param>
		/// <param name="fieldDeclaration">The FieldDeclarationSyntax from which to extract the necessary data</param>
		/// <returns>A readonly list of ExtractedPropertyDefinition containing the data extracted from the syntax tree</returns>
		public static ExtractedPropertyDefinition ExtractFieldDefinition(DefinitionExtractionContext extractionContext, FieldDeclarationSyntax fieldDeclaration)
		{
			ExtractedPropertyDefinition propertyDefinition = new ExtractedPropertyDefinition();

			propertyDefinition.PropertyName = GetFieldName(extractionContext, fieldDeclaration);
			propertyDefinition.PropertyTypeName = GetFieldTypeName(extractionContext, fieldDeclaration);
			propertyDefinition.IsAutoSerializable = extractionContext.IsTypeAutoSerializable(fieldDeclaration.Declaration.Type);
			// propertyDefinition.IsIMobileObject = ???

			return propertyDefinition;
		}

		#region Private Helper Methods

		/// <summary>
		/// Extract the name of the field for which we are building information
		/// </summary>
		/// <param name="extractionContext">The definition extraction context in which the extraction is being performed</param>
		/// <param name="targetTypeDeclaration">The FieldDeclarationSyntax from which to extract the necessary information</param>
		/// <returns>The name of the field for which we are extracting information</returns>
		private static string GetFieldName(DefinitionExtractionContext extractionContext, FieldDeclarationSyntax fieldDeclaration)
		{
			return fieldDeclaration.ToString();
		}

		/// <summary>
		/// Extract the type name of the field for which we are building information
		/// </summary>
		/// <param name="extractionContext">The definition extraction context in which the extraction is being performed</param>
		/// <param name="targetTypeDeclaration">The FieldDeclarationSyntax from which to extract the necessary information</param>
		/// <returns>The type name of the field for which we are extracting information</returns>
		private static string GetFieldTypeName(DefinitionExtractionContext extractionContext, FieldDeclarationSyntax fieldDeclaration)
		{
			return fieldDeclaration.Declaration.Type.ToString();
		}

		#endregion

	}
}
