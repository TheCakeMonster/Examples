using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;

namespace CslaSerialization.Generators.AutoSerialization
{

	/// <summary>
	/// Extract the definition of a class for which generation is required
	/// This is used to detach the builder from the Roslyn infrastructure, to enable testing
	/// </summary>
	public static class ClassDefinitionExtractor
	{

		/// <summary>
		/// Extract the data that will be needed for code generation from the syntax tree provided
		/// </summary>
		/// <param name="extractionContext">The definition extraction context in which the extraction is being performed</param>
		/// <param name="targetTypeDeclaration">The TypeDeclarationSyntax from which to extract the necessary data</param>
		/// <returns>ExtractedClassDefinition containing the data extracted from the syntax tree</returns>
		public static ExtractedClassDefinition ExtractClassDefinition(DefinitionExtractionContext extractionContext, TypeDeclarationSyntax targetTypeDeclaration)
		{
			ExtractedClassDefinition definition = new ExtractedClassDefinition();

			definition.ClassName = GetClassName(extractionContext, targetTypeDeclaration);
			definition.Namespace = GetNamespaceName(extractionContext, targetTypeDeclaration);
			definition.Scope = GetScopeName(extractionContext, targetTypeDeclaration);

			foreach (ExtractedPropertyDefinition propertyDefinition in PropertyDefinitionsExtractor.ExtractPropertyDefinitions(extractionContext, targetTypeDeclaration))
			{
				definition.Properties.Add(propertyDefinition);
			}

			foreach (ExtractedFieldDefinition fieldDefinition in FieldDefinitionsExtractor.ExtractFieldDefinitions(extractionContext, targetTypeDeclaration))
			{
				definition.Fields.Add(fieldDefinition);
			}

			return definition;
		}

		#region Private Helper Methods

		/// <summary>
		/// Extract the namespace of the type for which we will be generating code
		/// </summary>
		/// <param name="extractionContext">The definition extraction context in which the extraction is being performed</param>
		/// <param name="targetTypeDeclaration">The TypeDeclarationSyntax from which to extract the necessary information</param>
		/// <returns>The namespace of the type for which generation is being performed</returns>
		private static string GetNamespaceName(DefinitionExtractionContext extractionContext, TypeDeclarationSyntax targetTypeDeclaration)
		{
			string namespaceName = string.Empty;
			NamespaceDeclarationSyntax namespaceDeclaration;

			namespaceDeclaration = targetTypeDeclaration.Parent as NamespaceDeclarationSyntax;
			if (namespaceDeclaration is not null)
			{
				namespaceName = namespaceDeclaration.Name.ToString();
			}

			return namespaceName;
		}

		/// <summary>
		/// Extract the scope of the type for which we will be generating code
		/// </summary>
		/// <param name="extractionContext">The definition extraction context in which the extraction is being performed</param>
		/// <param name="targetTypeDeclaration">The TypeDeclarationSyntax from which to extract the necessary information</param>
		/// <returns>The scope of the type for which generation is being performed</returns>
		private static string GetScopeName(DefinitionExtractionContext extractionContext, TypeDeclarationSyntax targetTypeDeclaration)
		{
			return "public";
		}

		/// <summary>
		/// Extract the class name of the type for which we will be generating code
		/// </summary>
		/// <param name="extractionContext">The definition extraction context in which the extraction is being performed</param>
		/// <param name="targetTypeDeclaration">The TypeDeclarationSyntax from which to extract the necessary information</param>
		/// <returns>The class name of the type for which generation is being performed</returns>
		private static string GetClassName(DefinitionExtractionContext extractionContext, TypeDeclarationSyntax targetTypeDeclaration)
		{
			return targetTypeDeclaration.Identifier.ToString();
		}

		#endregion

	}
}
