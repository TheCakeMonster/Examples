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
		public void OnVisitSyntaxNode(GeneratorSyntaxContext generatorSyntaxContext)
		{
			SyntaxNode syntaxNode;
			SemanticModel model;
			DefinitionExtractionContext context;
			ExtractedClassDefinition classDefinition;

			syntaxNode = generatorSyntaxContext.Node;
			model = generatorSyntaxContext.SemanticModel;

			if (syntaxNode is not TypeDeclarationSyntax typeDeclarationSyntax) return;
			context = new DefinitionExtractionContext(generatorSyntaxContext);

			if (context.IsTypeAutoSerializable(typeDeclarationSyntax))
			{
				classDefinition = ClassDefinitionExtractor.ExtractClassDefinition(context, typeDeclarationSyntax);
				Targets.Add(classDefinition);
			}
		}

	}
}
