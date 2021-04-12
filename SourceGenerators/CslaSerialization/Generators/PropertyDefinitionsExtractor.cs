using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CslaSerialization.Generators
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
		/// <param name="context">The execution context in which the source generator is running</param>
		/// <param name="targetTypeDeclaration">The TypeDeclarationSyntax from which to extract the necessary data</param>
		/// <returns>A readonly list of ExtractedPropertyDefinition containing the data extracted from the syntax tree</returns>
		public static IReadOnlyList<ExtractedPropertyDefinition> ExtractPropertyDefinitions(GeneratorExecutionContext context, TypeDeclarationSyntax targetTypeDeclaration)
		{
			List<ExtractedPropertyDefinition> propertyDefinitions = new List<ExtractedPropertyDefinition>();
			ExtractedPropertyDefinition propertyDefinition;
			IReadOnlyList<PropertyDeclarationSyntax> serializableProperties;

			serializableProperties = GetSerializablePropertyDeclarations(context, targetTypeDeclaration);
			foreach (PropertyDeclarationSyntax propertyDeclaration in serializableProperties)
			{
				propertyDefinition = PropertyDefinitionExtractor.ExtractPropertyDefinition(context, propertyDeclaration);
				propertyDefinitions.Add(propertyDefinition);
			}

			return propertyDefinitions;
		}

		#region Private Helper Methods

		/// <summary>
		/// Get the property declarations for all properties which are to be serialized
		/// </summary>
		/// <param name="context">The execution context in which the source generator is running</param>
		/// <param name="targetTypeDeclaration">The TypeDeclarationSyntax from which to extract the necessary data</param>
		/// <returns>A readonly list of property declarations to be included in serialization</returns>
		private static IReadOnlyList<PropertyDeclarationSyntax> GetSerializablePropertyDeclarations(GeneratorExecutionContext context, TypeDeclarationSyntax targetTypeDeclaration)
		{
			List<PropertyDeclarationSyntax> serializableProperties;

			serializableProperties = targetTypeDeclaration.Members.Where(m => m is PropertyDeclarationSyntax propertyDeclaration &&
				!propertyDeclaration.AttributeLists.Any(
					al => al.Attributes.Any(
					attr => attr.ToString().Equals("AutoSerializationExcluded"))))
				.Cast<PropertyDeclarationSyntax>()
				.ToList();

			return serializableProperties;
		}

		#endregion

	}
}
