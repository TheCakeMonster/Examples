using Csla.Serialization;
using CslaSerialization.Generators.AutoSerialization.Discovery;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;

namespace CslaSerialization.Generators.AutoSerialization
{

	/// <summary>
	/// Source Generator for generating partial classes to complete decorated types that
	/// must offer automated serialization through the IMobileObject interface
	/// </summary>
	[Generator(LanguageNames.CSharp)]
	public class SerializationPartialsGenerator : IIncrementalGenerator
	{

        /// <summary>
        /// Called by Roslyn to request that the generator perform any required work
        /// </summary>
        /// <param name="context">The execution context provided by the Roslyn compiler</param>
        public void Initialize(IncrementalGeneratorInitializationContext context)
		{
#if (DEBUG)
            //// Uncomment this to enable debugging of the source generator
            //if (!Debugger.IsAttached)
            //{
            //	Debugger.Launch();
            //}

#endif
            IncrementalValuesProvider<ExtractedTypeDefinition?> classDeclarations = context.SyntaxProvider
				.CreateSyntaxProvider(
					predicate: static (s, _) => IsSyntaxTargetForGeneration(s),
					transform: static (ctx, _) => GetSemanticTargetForGeneration(ctx))
				.Where(static m => m is not null);

            IncrementalValueProvider<(Compilation, ImmutableArray<ExtractedTypeDefinition?>)> compilationAndClasses
                = context.CompilationProvider.Combine(classDeclarations.Collect());

            context.RegisterSourceOutput(compilationAndClasses,
                static (spc, source) => Execute(source.Item1, source.Item2, spc));
		}

		static bool IsSyntaxTargetForGeneration(SyntaxNode node)
			=> node is TypeDeclarationSyntax m && m.AttributeLists.Count > 0;

		static ExtractedTypeDefinition? GetSemanticTargetForGeneration(GeneratorSyntaxContext context)
		{
            // we know the node is a TypeDeclarationSyntax thanks to IsSyntaxTargetForGeneration
            var typeDeclarationSyntax = (TypeDeclarationSyntax)context.Node;

            // Check the attributes on the class for the one we are interested in
            foreach (AttributeListSyntax attributeListSyntax in typeDeclarationSyntax.AttributeLists)
            {
                foreach (AttributeSyntax attributeSyntax in attributeListSyntax.Attributes)
                {
                    IMethodSymbol? attributeSymbol = context.SemanticModel.GetSymbolInfo(attributeSyntax).Symbol as IMethodSymbol;
                    if (attributeSymbol == null)
                    {
                        // weird, we couldn't get the symbol, ignore it
                        continue;
                    }

                    INamedTypeSymbol attributeContainingTypeSymbol = attributeSymbol.ContainingType;
                    string fullName = attributeContainingTypeSymbol.ToDisplayString();

                    // Is the attribute the [AutoSerializable] attribute?
                    if (fullName == nameof(AutoSerializableAttribute))
                    {
                        // return the type for which generation is required
                        DefinitionExtractionContext extractionContext = new DefinitionExtractionContext(context);
                        var result = TypeDefinitionExtractor.ExtractTypeDefinition(extractionContext, typeDeclarationSyntax);
                        return result;
                    }
                }
            }
            
            return null;
		}

        private static void Execute(Compilation compilation, ImmutableArray<ExtractedTypeDefinition?> typeDefinitions, SourceProductionContext context)
        {
            SerializationPartialGenerator generator;

            try
            {
                // Generate a partial type to extend each of the types identified
                foreach (ExtractedTypeDefinition? typeDefinition in typeDefinitions)
                {
                    if (typeDefinition is null)
                    {
                        continue;
                    }

                    // Generate the code needed for the identified type
                    generator = new SerializationPartialGenerator();
                    generator.GeneratePartialType(context, typeDefinition);
                }
            }
            catch (Exception ex)
            {
                context.ReportDiagnostic(ex.ToUsageDiagnostic());
            }
        }
    }
}
