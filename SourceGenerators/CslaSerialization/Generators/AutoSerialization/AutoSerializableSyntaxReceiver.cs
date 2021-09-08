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
	/// Class to determine the class declarations for which we must perform code generation
	/// </summary>
	public class AutoSerializableReceiver : ISyntaxContextReceiver
	{
		public IList<ExtractedClassDefinition> Targets = new List<ExtractedClassDefinition>();

		/// <summary>
		/// Test syntax nodes to see if they represent a type for which we must generate code
		/// </summary>
		/// <param name="context">The generator context supplied by Roslyn</param>
		public void OnVisitSyntaxNode(GeneratorSyntaxContext context)
		{
			INamedTypeSymbol desiredAttributeSymbol;
			INamedTypeSymbol typeSymbol;
			ExtractedClassDefinition classDefinition;
			SyntaxNode syntaxNode;
			SemanticModel model;

			syntaxNode = context.Node;
			model = context.SemanticModel;

			if (syntaxNode is not TypeDeclarationSyntax typeDeclarationSyntax) return;
			typeSymbol = model.GetDeclaredSymbol(syntaxNode) as INamedTypeSymbol;

			desiredAttributeSymbol = context.SemanticModel.Compilation.GetTypeByMetadataName(typeof(AutoSerializableAttribute).FullName);

			if (IsTypeDecoratedBy(typeSymbol, desiredAttributeSymbol))
			{
				classDefinition = ClassDefinitionExtractor.ExtractClassDefinition(context, typeDeclarationSyntax);
				Targets.Add(classDefinition);
			}
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
				attr => IsMatchingAttribute(attr.AttributeClass, desiredAttributeSymbol));
		}

		/// <summary>
		/// Determine if two symbols represent the same attribute
		/// </summary>
		/// <param name="appliedAttributeSymbol">The attribute applied to the type we are testing</param>
		/// <param name="desiredAttributeSymbol">The attribute whose presence we are testing for</param>
		/// <returns>Boolean true if the two symbols represent the same types</returns>
		private bool IsMatchingAttribute(INamedTypeSymbol appliedAttributeSymbol, INamedTypeSymbol desiredAttributeSymbol)
		{
			return SymbolEqualityComparer.Default.Equals(appliedAttributeSymbol, desiredAttributeSymbol);
		}

		#endregion

	}
}
