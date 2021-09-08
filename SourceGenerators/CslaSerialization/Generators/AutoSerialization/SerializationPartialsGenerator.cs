using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace CslaSerialization.Generators.AutoSerialization
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
			//// Uncomment this to enable debugging of the source generator
			//if (!Debugger.IsAttached)
			//{
			//	Debugger.Launch();
			//}

#endif
			context.RegisterForSyntaxNotifications(() => new AutoSerializableReceiver());
		}

		/// <summary>
		/// Called by Roslyn to request that the generator perform any required work
		/// </summary>
		/// <param name="context">The execution context provided by the Roslyn compiler</param>
		public void Execute(GeneratorExecutionContext context)
		{
			SerializationPartialGenerator generator;

			try
			{
				if (context.SyntaxContextReceiver is AutoSerializableReceiver receiver)
				{
					// Generate a partial class for each of the types identified
					foreach (ExtractedClassDefinition classDefinition in receiver.Targets)
					{
						generator = new SerializationPartialGenerator();
						generator.GeneratePartialClass(context, classDefinition);
					}
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine($"Exception in SerializationPartialsGenerator!\r\n{ex.ToString()}");
			}
		}

	}

}
