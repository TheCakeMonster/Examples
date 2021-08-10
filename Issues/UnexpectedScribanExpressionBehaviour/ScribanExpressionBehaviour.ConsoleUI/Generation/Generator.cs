using Scriban;
using Scriban.Parsing;
using Scriban.Runtime;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ScribanExpressionBehaviour.ConsoleUI.Generation
{
	internal class Generator
	{

		private Template _template;

		public Task<CompilationResults> CompileAsync(CompilationParameters parameters)
		{
			CompilationResults results = new CompilationResults();
			ParserOptions parserOptions = new ParserOptions() { ExpressionDepthLimit = 1024 };
			LexerOptions lexerOptions = new LexerOptions() { Lang = ScriptLang.Default };

			try
			{
				// Attempt to parse the template
				_template = Template.Parse(parameters.TemplateText, null, parserOptions, lexerOptions);

				// Transfer any parsing errors that we encountered
				if (_template.HasErrors)
				{
					foreach (LogMessage message in _template.Messages)
					{
						results.CompilationErrors.Add(new CompilationError() { ErrorText = message.Message });
					}
				}
				else
				{
					// Parsing was successful, so we can inform the consumer of that
					results.Result = CompilationOutcomes.Success;
				}

			}
			catch (Exception ex)
			{
				// Record parsing as a compilation error
				results.CompilationErrors.Add(new CompilationError() { ErrorText = ex.Message });
			}

			return Task.FromResult(results);
		}

		public async Task<GenerationResults> GenerateAsync(GenerationParameters parameters)
		{
			GenerationResults results = new GenerationResults();
			TemplateContext context = new TemplateContext { MemberRenamer = member => member.Name };
			ScriptObject parametersScriptObject = new ScriptObject();

			try
			{
				ImportHelperType(parametersScriptObject, "TestHelper", typeof(TestHelper));

				// Set up passing of the necessary parameters via the context
				parametersScriptObject.Import(parameters, renamer: member => member.Name);
				context.PushGlobal(parametersScriptObject);

				// Perform rendering
				results.GeneratedText = await _template.RenderAsync(context);

				// Mark the generation as successful
				results.Result = GenerationOutcomes.Success;
			}
			catch (Exception ex)
			{
				// Record rendering failures
				results.GenerationErrors.Add(new GenerationError() { ErrorText = ex.Message });
			}

			return results;
		}

		/// <summary>
		/// Method to import the methods of a single type into a script object
		/// </summary>
		/// <param name="rootScriptObject">The script object to which to add the type's methods</param>
		/// <param name="moniker">The name through which the type will be known</param>
		/// <param name="helperType">The type whose methods we are making available</param>
		private void ImportHelperType(ScriptObject rootScriptObject, string moniker, Type helperType)
		{
			ScriptObject childScriptObject = new ScriptObject();

			// Import all of the methods into a child type, then add that child to the root
			childScriptObject.Import(helperType, renamer: member => member.Name);
			rootScriptObject.Add(moniker, childScriptObject);
		}

	}

}
