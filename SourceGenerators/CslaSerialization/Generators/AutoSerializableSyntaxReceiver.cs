using CslaSerialization.Core;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CslaSerialization.Generators
{

	/// <summary>
	/// Class to determine the class declarations for which we must perform code generation
	/// </summary>
	public class AutoSerializableReceiver : ISyntaxContextReceiver
	{
		public IList<TypeDeclarationSyntax> Targets = new List<TypeDeclarationSyntax>();

		public void OnVisitSyntaxNode(GeneratorSyntaxContext context)
		{
			INamedTypeSymbol matchingAttributeSymbol;
			SyntaxNode syntaxNode = context.Node;
			SemanticModel model = context.SemanticModel;

			if (syntaxNode is not TypeDeclarationSyntax typeDeclarationSyntax) return;

			matchingAttributeSymbol = context.SemanticModel.Compilation.GetTypeByMetadataName(typeof(AutoSerializableAttribute).FullName) as INamedTypeSymbol;
			var typeSymbol = model.GetDeclaredSymbol(syntaxNode) as INamedTypeSymbol;

			if (RepresentsClassOfInterest(typeSymbol, matchingAttributeSymbol))
			{
				Targets.Add(typeDeclarationSyntax);
			}
		}

		#region Private Helper Methods

		private bool RepresentsClassOfInterest(INamedTypeSymbol typeSymbol, INamedTypeSymbol matchingAttributeSymbol)
		{
			return typeSymbol.GetAttributes().Any(
				attr => RepresentsAttributeOfInterest(attr.AttributeClass, matchingAttributeSymbol));
		}

		private bool RepresentsAttributeOfInterest(INamedTypeSymbol attributeSyntax, INamedTypeSymbol matchingAttributeSymbol)
		{
			return SymbolEqualityComparer.Default.Equals(attributeSyntax, matchingAttributeSymbol);
		}

		#endregion
	}
}
