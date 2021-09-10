﻿using CslaSerialization.Core;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CslaSerialization.Generators.AutoSerialization
{

	/// <summary>
	/// Helper for definition extraction
	/// </summary>
	public class DefinitionExtractionContext
	{

		private readonly GeneratorSyntaxContext _context;
		private readonly INamedTypeSymbol _autoSerializableAttributeSymbol;
		private readonly INamedTypeSymbol _autoSerializationIncludedAttributeSymbol;
		private readonly INamedTypeSymbol _autoSerializationExcludedAttributeSymbol;

		public DefinitionExtractionContext(GeneratorSyntaxContext context)
		{
			_context = context;
			_autoSerializableAttributeSymbol = context.SemanticModel.Compilation.GetTypeByMetadataName(typeof(AutoSerializableAttribute).FullName);
			_autoSerializationIncludedAttributeSymbol = context.SemanticModel.Compilation.GetTypeByMetadataName(typeof(AutoSerializationIncludedAttribute).FullName);
			_autoSerializationExcludedAttributeSymbol = context.SemanticModel.Compilation.GetTypeByMetadataName(typeof(AutoSerializationExcludedAttribute).FullName);
		}

		public GeneratorSyntaxContext Context => _context;

		/// <summary>
		/// Determine if a type declaration represents a type that is auto serializable
		/// </summary>
		/// <param name="typeSymbol">The declaration representing the type to be tested</param>
		/// <returns>Boolean true if the type is decorated with the AutoSerializable attribute, otherwise false</returns>
		public bool IsTypeAutoSerializable(TypeDeclarationSyntax typeDeclarationSyntax)
		{
			INamedTypeSymbol typeSymbol;

			typeSymbol = _context.SemanticModel.GetDeclaredSymbol(typeDeclarationSyntax) as INamedTypeSymbol;
			return IsTypeDecoratedBy(typeSymbol, _autoSerializableAttributeSymbol);
		}

		/// <summary>
		/// Determine if a type declaration represents a type that is auto serializable
		/// </summary>
		/// <param name="typeSymbol">The declaration representing the type to be tested</param>
		/// <returns>Boolean true if the type is decorated with the AutoSerializable attribute, otherwise false</returns>
		public bool IsTypeAutoSerializable(TypeSyntax typeSyntax)
		{
			INamedTypeSymbol typeSymbol;

			typeSymbol = _context.SemanticModel.GetSymbolInfo(typeSyntax).Symbol as INamedTypeSymbol;
			if (typeSymbol is null) return false;
			return IsTypeDecoratedBy(typeSymbol, _autoSerializableAttributeSymbol);
		}

		/// <summary>
		/// Determine if a property declaration is marked as included in serialization
		/// </summary>
		/// <param name="propertyDeclaration">The declaration of the property being inspected</param>
		/// <returns>Boolean true if the property is decorated with the AutoSerializationIncluded attribute, otherwise false</returns>
		public bool IsPropertyAutoSerializationIncluded(PropertyDeclarationSyntax propertyDeclaration)
		{
			return IsPropertyDecoratedWith(propertyDeclaration, _autoSerializationIncludedAttributeSymbol);
		}

		/// <summary>
		/// Determine if a property declaration is marked as excluded from serialization
		/// </summary>
		/// <param name="propertyDeclaration">The declaration of the property being inspected</param>
		/// <returns>Boolean true if the property is decorated with the AutoSerializationExcluded attribute, otherwise false</returns>
		public bool IsPropertyAutoSerializationExcluded(PropertyDeclarationSyntax propertyDeclaration)
		{
			return IsPropertyDecoratedWith(propertyDeclaration, _autoSerializationExcludedAttributeSymbol);
		}

		/// <summary>
		/// Determine if a field declaration is marked as included in serialization
		/// </summary>
		/// <param name="fieldDeclaration">The declaration of the field being inspected</param>
		/// <returns>Boolean true if the field is decorated with the AutoSerializationIncluded attribute, otherwise false</returns>
		public bool IsFieldAutoSerializationIncluded(FieldDeclarationSyntax fieldDeclaration)
		{
			return IsFieldDecoratedWith(fieldDeclaration, _autoSerializationIncludedAttributeSymbol);
		}

		/// <summary>
		/// Determine if a field declaration is marked as excluded from serialization
		/// </summary>
		/// <param name="fieldDeclaration">The declaration of the field being inspected</param>
		/// <returns>Boolean true if the field is decorated with the AutoSerializationExcluded attribute, otherwise false</returns>
		public bool IsFieldAutoSerializationExcluded(FieldDeclarationSyntax fieldDeclaration)
		{
			return IsFieldDecoratedWith(fieldDeclaration, _autoSerializationExcludedAttributeSymbol);
		}

		#region Private Helper Methods

		/// <summary>
		/// Determine if the type symbol represents a type decorated by an attribute of interest
		/// </summary>
		/// <param name="typeSymbol">The symbol representing the type</param>
		/// <param name="desiredAttributeSymbol">The symbol representing the attribute of interest</param>
		/// <returns>Boolean true if the type is decorated with the attribute, otherwise false</returns>
		private bool IsTypeDecoratedBy(INamedTypeSymbol typeSymbol, INamedTypeSymbol desiredAttributeSymbol)
		{
			return typeSymbol.GetAttributes().Any(
				attr => IsMatchingTypeSymbol(attr.AttributeClass, desiredAttributeSymbol));
		}

		/// <summary>
		/// Determine if a property declaration syntax is decorated with an attribute of interest
		/// </summary>
		/// <param name="propertyDeclaration">The syntax node representing the property being investigated</param>
		/// <param name="desiredAttributeSymbol">The symbol representing the attribute of interest</param>
		/// <returns>Boolean true if the type is decorated with the attribute, otherwise false</returns>
		private bool IsPropertyDecoratedWith(PropertyDeclarationSyntax propertyDeclaration, INamedTypeSymbol desiredAttributeSymbol)
		{
			INamedTypeSymbol appliedAttributeSymbol;

			foreach (AttributeSyntax attributeSyntax in propertyDeclaration.AttributeLists.SelectMany(al => al.Attributes))
			{
				appliedAttributeSymbol = _context.SemanticModel.GetTypeInfo(attributeSyntax).Type as INamedTypeSymbol;
				if (IsMatchingTypeSymbol(appliedAttributeSymbol, desiredAttributeSymbol))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// Determine if a field declaration syntax is decorated with an attribute of interest
		/// </summary>
		/// <param name="fieldDeclaration">The syntax node representing the field being investigated</param>
		/// <param name="desiredAttributeSymbol">The symbol representing the attribute of interest</param>
		/// <returns>Boolean true if the type is decorated with the attribute, otherwise false</returns>
		private bool IsFieldDecoratedWith(FieldDeclarationSyntax fieldDeclaration, INamedTypeSymbol desiredAttributeSymbol)
		{
			INamedTypeSymbol appliedAttributeSymbol;

			foreach (AttributeSyntax attributeSyntax in fieldDeclaration.AttributeLists.SelectMany(al => al.Attributes))
			{
				appliedAttributeSymbol = _context.SemanticModel.GetTypeInfo(attributeSyntax).Type as INamedTypeSymbol;
				if (IsMatchingTypeSymbol(appliedAttributeSymbol, desiredAttributeSymbol))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// Determine if two symbols represent the same attribute
		/// </summary>
		/// <param name="appliedAttributeSymbol">The attribute applied to the type we are testing</param>
		/// <param name="desiredAttributeSymbol">The attribute whose presence we are testing for</param>
		/// <returns>Boolean true if the two symbols represent the same types</returns>
		private bool IsMatchingTypeSymbol(INamedTypeSymbol appliedAttributeSymbol, INamedTypeSymbol desiredAttributeSymbol)
		{
			return SymbolEqualityComparer.Default.Equals(appliedAttributeSymbol, desiredAttributeSymbol);
		}

		#endregion

	}
}