using ScribanExpressionBehaviour.ConsoleUI.Generation;
using System;
using System.Threading.Tasks;

namespace ScribanExpressionBehaviour.ConsoleUI
{
	class Program
	{
		static async Task Main(string[] args)
		{
			CompilationParameters compilationParameters;
			GenerationParameters generationParameters;
			CompilationResults compilationResults;
			GenerationResults generationResults;
			EmbeddedTemplateTextRetriever textRetriever;
			Generator generator;

			try
			{

				// New up the objects we need
				compilationParameters = new CompilationParameters();
				generationParameters = new GenerationParameters();

				textRetriever = new EmbeddedTemplateTextRetriever();
				generator = new Generator();

				// Get the text of the template
				compilationParameters.TemplateText = await textRetriever.RetrieveTemplateTextAsync("ScribanExpressionBehaviour.ConsoleUI.CorrectedTemplate.txt");

				// Compile the template
				compilationResults = await generator.CompileAsync(compilationParameters);

				if (compilationResults.Result != CompilationOutcomes.Success)
				{
					Console.WriteLine("Errors in compilation!");
					foreach (CompilationError error in compilationResults.CompilationErrors)
					{
						Console.WriteLine(error.ErrorText);
					}
					return;
				}

				// Generate the output
				generationResults = await generator.GenerateAsync(generationParameters);

				if (generationResults.Result != GenerationOutcomes.Success)
				{
					Console.WriteLine("Errors in generation!");
					foreach (GenerationError error in generationResults.GenerationErrors)
					{
						Console.WriteLine(error.ErrorText);
					}
					return;
				}

				// Write the results to the screen
				Console.WriteLine("Generation results:");
				Console.WriteLine(generationResults.GeneratedText);

			}
			catch (Exception ex)
			{
				Console.WriteLine("Exception detected during execution!");
				Console.WriteLine(ex.ToString());
			}
		}
	}
}
