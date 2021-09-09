using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CslaSerialization.Generators.AutoSerialization
{

	/// <summary>
	/// Extract the definition of all properties for a class for which code generation is required
	/// This is used to detach the builder from the Roslyn infrastructure, to enable testing
	/// </summary>
	/// <remarks>Only the properties to be included in serialization are extracted; those manually excluded
	/// from serialization through use of the [AutoSerializationExcluded] attribute are not returned</remarks>
	public static class FieldDefinitionsExtractor
	{

		/// <summary>
		/// Extract information about the properties which must be serialized from a part of the syntax tree
		/// </summary>
		/// <param name="extractionContext">The definition extraction context in which the extraction is being performed</param>
		/// <param name="targetTypeDeclaration">The TypeDeclarationSyntax from which to extract the necessary data</param>
		/// <returns>A readonly list of ExtractedPropertyDefinition containing the data extracted from the syntax tree</returns>
		public static IReadOnlyList<ExtractedPropertyDefinition> ExtractFieldDefinitions(DefinitionExtractionContext extractionContext, TypeDeclarationSyntax targetTypeDeclaration)
		{
			List<ExtractedPropertyDefinition> propertyDefinitions = new List<ExtractedPropertyDefinition>();
			ExtractedPropertyDefinition fieldDefinition;
			IReadOnlyList<FieldDeclarationSyntax> serializableFields;

			serializableFields = GetSerializableFieldDeclarations(extractionContext, targetTypeDeclaration);
			foreach (FieldDeclarationSyntax fieldDeclaration in serializableFields)
			{
				fieldDefinition = FieldDefinitionExtractor.ExtractFieldDefinition(extractionContext, fieldDeclaration);
				propertyDefinitions.Add(fieldDefinition);
			}

			return propertyDefinitions;
		}

		#region Private Helper Methods

		/// <summary>
		/// Get the property declarations for all fields which are to be serialized
		/// </summary>
		/// <param name="extractionContext">The definition extraction context in which the extraction is being performed</param>
		/// <param name="targetTypeDeclaration">The TypeDeclarationSyntax from which to extract the necessary data</param>
		/// <returns>A readonly list of field declarations to be included in serialization</returns>
		private static IReadOnlyList<FieldDeclarationSyntax> GetSerializableFieldDeclarations(DefinitionExtractionContext extractionContext, TypeDeclarationSyntax targetTypeDeclaration)
		{
			List<FieldDeclarationSyntax> serializableFields;
			List<FieldDeclarationSyntax> optedInSerializableFields;

			// Get all fields that are not specifically opted out with the [AutoSerializationExcluded] attribute
			serializableFields = GetPublicNonExcludedFields(extractionContext, targetTypeDeclaration);

			// Add any private or protected fields that are opted in with the use of the [AutoSerializationIncluded] attribute
			optedInSerializableFields = GetNonPublicIncludedFields(extractionContext, targetTypeDeclaration);
			serializableFields.AddRange(optedInSerializableFields);

			return serializableFields;
		}

		private static List<FieldDeclarationSyntax> GetPublicNonExcludedFields(DefinitionExtractionContext extractionContext, TypeDeclarationSyntax targetTypeDeclaration)
		{
			List<FieldDeclarationSyntax> serializableFields;

			// Get all fields that are not specifically opted out with the [AutoSerializationExcluded] attribute
			serializableFields = targetTypeDeclaration.Members.Where(m => m is FieldDeclarationSyntax fieldDeclaration &&
				fieldDeclaration.Modifiers.Any(m => !m.ValueText.Equals("public") && !m.ValueText.Equals("internal")) &&
				!extractionContext.IsFieldAutoSerializationExcluded(fieldDeclaration))
				.Cast<FieldDeclarationSyntax>()
				.ToList();

			return serializableFields;
		}

		private static List<FieldDeclarationSyntax> GetNonPublicIncludedFields(DefinitionExtractionContext extractionContext, TypeDeclarationSyntax targetTypeDeclaration)
		{
			List<FieldDeclarationSyntax> serializableFields;

			// Get any private or protected fields that are opted in with the use of the [AutoSerializationIncluded] attribute
			serializableFields = targetTypeDeclaration.Members.Where(m => m is FieldDeclarationSyntax fieldDeclaration &&
				!fieldDeclaration.Modifiers.Any(m => !m.ValueText.Equals("public") && !m.ValueText.Equals("internal")) &&
				extractionContext.IsFieldAutoSerializationIncluded(fieldDeclaration))
				.Cast<FieldDeclarationSyntax>()
				.ToList();

			return serializableFields;
		}

		#endregion

	}
}
