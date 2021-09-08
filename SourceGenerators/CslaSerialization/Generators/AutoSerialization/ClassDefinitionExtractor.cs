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
		/// <param name="context">The execution context in which the source generator is running</param>
		/// <param name="targetTypeDeclaration">The TypeDeclarationSyntax from which to extract the necessary data</param>
		/// <returns>ExtractedClassDefinition containing the data extracted from the syntax tree</returns>
		public static ExtractedClassDefinition ExtractClassDefinition(GeneratorSyntaxContext context, TypeDeclarationSyntax targetTypeDeclaration)
		{
			ExtractedClassDefinition definition = new ExtractedClassDefinition();

			definition.ClassName = GetClassName(context, targetTypeDeclaration);
			definition.Namespace = GetNamespaceName(context, targetTypeDeclaration);
			definition.Scope = GetScopeName(context, targetTypeDeclaration);

			foreach (ExtractedPropertyDefinition propertyDefinition in PropertyDefinitionsExtractor.ExtractPropertyDefinitions(context, targetTypeDeclaration))
			{
				definition.Properties.Add(propertyDefinition);
			}

			return definition;
		}

		#region Private Helper Methods

		/// <summary>
		/// Extract the namespace of the type for which we will be generating code
		/// </summary>
		/// <param name="context">The execution context in which the source generator is running</param>
		/// <param name="targetTypeDeclaration">The TypeDeclarationSyntax from which to extract the necessary information</param>
		/// <returns>The namespace of the type for which generation is being performed</returns>
		private static string GetNamespaceName(GeneratorSyntaxContext context, TypeDeclarationSyntax targetTypeDeclaration)
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
		/// <param name="context">The execution context in which the source generator is running</param>
		/// <param name="targetTypeDeclaration">The TypeDeclarationSyntax from which to extract the necessary information</param>
		/// <returns>The scope of the type for which generation is being performed</returns>
		private static string GetScopeName(GeneratorSyntaxContext context, TypeDeclarationSyntax targetTypeDeclaration)
		{
			return "public";
		}

		/// <summary>
		/// Extract the class name of the type for which we will be generating code
		/// </summary>
		/// <param name="context">The execution context in which the source generator is running</param>
		/// <param name="targetTypeDeclaration">The TypeDeclarationSyntax from which to extract the necessary information</param>
		/// <returns>The class name of the type for which generation is being performed</returns>
		private static string GetClassName(GeneratorSyntaxContext context, TypeDeclarationSyntax targetTypeDeclaration)
		{
			return targetTypeDeclaration.Identifier.ToString();
		}

		#endregion

	}
}
