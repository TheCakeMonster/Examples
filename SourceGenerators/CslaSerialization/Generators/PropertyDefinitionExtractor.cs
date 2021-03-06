﻿using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Text;

namespace CslaSerialization.Generators
{

	/// <summary>
	/// Extract the definition of a single property of a class for which code generation is being performed
	/// This is used to detach the builder from the Roslyn infrastructure, to enable testing
	/// </summary>
	public static class PropertyDefinitionExtractor
	{

		/// <summary>
		/// Extract information about a single property from its declaration in the syntax tree
		/// </summary>
		/// <param name="context">The execution context in which the source generator is running</param>
		/// <param name="targetTypeDeclaration">The PropertyDeclarationSyntax from which to extract the necessary data</param>
		/// <returns>A readonly list of ExtractedPropertyDefinition containing the data extracted from the syntax tree</returns>
		public static ExtractedPropertyDefinition ExtractPropertyDefinition(GeneratorExecutionContext context, PropertyDeclarationSyntax propertyDeclaration)
		{
			ExtractedPropertyDefinition propertyDefinition = new ExtractedPropertyDefinition();

			propertyDefinition.PropertyName = GetPropertyName(context, propertyDeclaration);
			propertyDefinition.PropertyTypeName = GetPropertyTypeName(context, propertyDeclaration);

			return propertyDefinition;
		}

		#region Private Helper Methods

		/// <summary>
		/// Extract the name of the property for which we are building information
		/// </summary>
		/// <param name="context">The execution context in which the source generator is running</param>
		/// <param name="targetTypeDeclaration">The PropertyDeclarationSyntax from which to extract the necessary information</param>
		/// <returns>The name of the property for which we are extracting information</returns>
		private static string GetPropertyName(GeneratorExecutionContext context, PropertyDeclarationSyntax propertyDeclaration)
		{
			return propertyDeclaration.Identifier.ValueText;
		}

		/// <summary>
		/// Extract the type name of the property for which we are building information
		/// </summary>
		/// <param name="context">The execution context in which the source generator is running</param>
		/// <param name="targetTypeDeclaration">The PropertyDeclarationSyntax from which to extract the necessary information</param>
		/// <returns>The type name of the property for which we are extracting information</returns>
		private static string GetPropertyTypeName(GeneratorExecutionContext context, PropertyDeclarationSyntax propertyDeclaration)
		{
			return propertyDeclaration.Type.ToString();
		}

		#endregion

	}
}
