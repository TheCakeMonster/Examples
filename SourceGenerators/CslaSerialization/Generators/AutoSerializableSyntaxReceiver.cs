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
		public IList<ClassDeclarationSyntax> Targets = new List<ClassDeclarationSyntax>();

		public void OnVisitSyntaxNode(GeneratorSyntaxContext context)
		{
			INamedTypeSymbol matchingAttributeSymbol;

			if (context.Node is not ClassDeclarationSyntax classDeclarationSyntax) return;
			
			matchingAttributeSymbol = context.SemanticModel.Compilation.GetTypeByMetadataName(typeof(AutoSerializableAttribute).FullName);
			// var typeSymbol = context.SemanticModel.GetDeclaredSymbol(context.Node) as INamedTypeSymbol;

			if (RepresentsClassOfInterest(classDeclarationSyntax, matchingAttributeSymbol))
			{
				Targets.Add(classDeclarationSyntax);
			}
		}

		#region Private Helper Methods

		private bool RepresentsClassOfInterest(ClassDeclarationSyntax classDeclarationSyntax, INamedTypeSymbol matchingAttributeSymbol)
		{
			return classDeclarationSyntax.AttributeLists.Any(
				al => al.Attributes.Any(
					a => RepresentsAttributeOfInterest(a.Name, matchingAttributeSymbol)));
		}

		private bool RepresentsAttributeOfInterest(TypeSyntax typeSyntax, INamedTypeSymbol matchingAttributeSymbol)
		{
			return SymbolEqualityComparer.Default.Equals((ISymbol)typeSyntax, matchingAttributeSymbol);
		}

		#endregion
	}
}
