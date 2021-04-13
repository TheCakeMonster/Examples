using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace CslaSerialization.Generators
{

	/// <summary>
	/// Source Generator for generating partial classes to complete decorated types that
	/// must offer automated serialization
	/// </summary>
	[Generator]
	public class SerializationPartialsGenerator : ISourceGenerator
	{

		/// <summary>
		/// Initialise the generator prior to performing any work
		/// </summary>
		/// <param name="context">The execution context provided by the Roslyn compiler</param>
		public void Initialize(GeneratorInitializationContext context)
		{
#if (DEBUG)
			// Uncomment this to enable debugging of the source generator
			//if (!Debugger.IsAttached)
			//{
			//	Debugger.Launch();
			//}
#endif
		}

		/// <summary>
		/// Called by Roslyn to request that the generator perform any required work
		/// </summary>
		/// <param name="context">The execution context provided by the Roslyn compiler</param>
		public void Execute(GeneratorExecutionContext context)
		{
			List<TypeDeclarationSyntax> serializableTypes;
			SerializationPartialGenerator generator;

			try
			{
				var syntaxTrees = context.Compilation?.SyntaxTrees;

				if (syntaxTrees is not null)
				{
					foreach (SyntaxTree syntaxTree in syntaxTrees)
					{
						// Identify the types that are to be acted upon by this generator and pass to the builder
						serializableTypes = syntaxTree.GetRoot().DescendantNodes().OfType<TypeDeclarationSyntax>()
							.Where(t => t.AttributeLists.Any(
								al => al.Attributes.Any(
								attr => attr.Name.ToString().Equals("AutoSerializable"))))
							.ToList();
						if (serializableTypes is not null && serializableTypes.Count > 0)
						{
							// Generate a partial class for each of the types identified
							foreach (TypeDeclarationSyntax typeDefinition in serializableTypes)
							{
								generator = new SerializationPartialGenerator();
								generator.GeneratePartialClass(context, typeDefinition);
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				// Debug.WriteLine($"Exception in SerializationPartialGenerator!\r\n{ex.ToString()}");
			}
		}

	}

}
