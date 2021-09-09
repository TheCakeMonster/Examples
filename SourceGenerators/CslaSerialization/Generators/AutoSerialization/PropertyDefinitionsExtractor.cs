using CslaSerialization.Core;
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
	public static class PropertyDefinitionsExtractor
	{

		/// <summary>
		/// Extract information about the properties which must be serialized from a part of the syntax tree
		/// </summary>
		/// <param name="extractionContext">The definition extraction context in which the extraction is being performed</param>
		/// <param name="targetTypeDeclaration">The TypeDeclarationSyntax from which to extract the necessary data</param>
		/// <returns>A readonly list of ExtractedPropertyDefinition containing the data extracted from the syntax tree</returns>
		public static IReadOnlyList<ExtractedPropertyDefinition> ExtractPropertyDefinitions(DefinitionExtractionContext extractionContext, TypeDeclarationSyntax targetTypeDeclaration)
		{
			List<ExtractedPropertyDefinition> propertyDefinitions = new List<ExtractedPropertyDefinition>();
			ExtractedPropertyDefinition propertyDefinition;
			IReadOnlyList<PropertyDeclarationSyntax> serializableProperties;

			serializableProperties = GetSerializablePropertyDeclarations(extractionContext, targetTypeDeclaration);
			foreach (PropertyDeclarationSyntax propertyDeclaration in serializableProperties)
			{
				propertyDefinition = PropertyDefinitionExtractor.ExtractPropertyDefinition(extractionContext, propertyDeclaration);
				propertyDefinitions.Add(propertyDefinition);
			}

			return propertyDefinitions;
		}

		#region Private Helper Methods

		/// <summary>
		/// Get the property declarations for all properties which are to be serialized
		/// </summary>
		/// <param name="extractionContext">The definition extraction context in which the extraction is being performed</param>
		/// <param name="targetTypeDeclaration">The TypeDeclarationSyntax from which to extract the necessary data</param>
		/// <returns>A readonly list of property declarations to be included in serialization</returns>
		private static IReadOnlyList<PropertyDeclarationSyntax> GetSerializablePropertyDeclarations(DefinitionExtractionContext extractionContext, TypeDeclarationSyntax targetTypeDeclaration)
		{
			List<PropertyDeclarationSyntax> serializableProperties;
			List<PropertyDeclarationSyntax> optedInSerializableProperties;

			// Get public or internal properties that are not specifically opted out with the [AutoSerializationExcluded] attribute
			serializableProperties = GetPublicNonExcludedProperties(extractionContext, targetTypeDeclaration);

			// Add any private or protected properties that are opted in with the use of the [AutoSerializationIncluded] attribute
			optedInSerializableProperties = GetNonPublicIncludedProperties(extractionContext, targetTypeDeclaration);
			serializableProperties.AddRange(optedInSerializableProperties);

			return serializableProperties;
		}

		private static List<PropertyDeclarationSyntax> GetPublicNonExcludedProperties(DefinitionExtractionContext extractionContext, TypeDeclarationSyntax targetTypeDeclaration)
		{
			List<PropertyDeclarationSyntax> serializableProperties;

			// Get public or internal properties that are not specifically opted out with the [AutoSerializationExcluded] attribute
			serializableProperties = targetTypeDeclaration.Members.Where(
				m => m is PropertyDeclarationSyntax propertyDeclaration &&
				HasOneOfScopes(extractionContext, propertyDeclaration, "public", "internal") &&
				!extractionContext.IsPropertyAutoSerializationExcluded(propertyDeclaration))
				.Cast<PropertyDeclarationSyntax>()
				.ToList();

			return serializableProperties;
		}

		private static List<PropertyDeclarationSyntax> GetNonPublicIncludedProperties(DefinitionExtractionContext extractionContext, TypeDeclarationSyntax targetTypeDeclaration)
		{
			List<PropertyDeclarationSyntax> serializableProperties;

			// Get private or protected properties that are specifically opted in with the [AutoSerializationIncluded] attribute
			serializableProperties = targetTypeDeclaration.Members.Where(
				m => m is PropertyDeclarationSyntax propertyDeclaration &&
				!HasOneOfScopes(extractionContext, propertyDeclaration, "public", "internal") &&
				extractionContext.IsPropertyAutoSerializationIncluded(propertyDeclaration))
				.Cast<PropertyDeclarationSyntax>()
				.ToList();

			return serializableProperties;
		}

		private static bool HasOneOfScopes(DefinitionExtractionContext context, PropertyDeclarationSyntax propertyDeclaration, params string[] scopes)
		{
			foreach (string scope in scopes)
			{
				if (propertyDeclaration.Modifiers.Any(m => m.ValueText.Equals(scope, StringComparison.InvariantCultureIgnoreCase)))
				{
					return true;
				}
			}

			return false;
		}

		#endregion

	}
}
