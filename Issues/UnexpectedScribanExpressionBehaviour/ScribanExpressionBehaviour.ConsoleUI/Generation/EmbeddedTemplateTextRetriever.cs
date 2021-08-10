using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ScribanExpressionBehaviour.ConsoleUI.Generation
{

	/// <summary>
	/// Retriever of template text from an embedded resource file
	/// </summary>
	internal class EmbeddedTemplateTextRetriever
	{

		/// <summary>
		/// Retrieve the generation text for a template from an embedded resource file
		/// </summary>
		/// <param name="templatePath">The path of the template</param>
		/// <returns>The text of the template</returns>
		public async Task<string> RetrieveTemplateTextAsync(string templatePath)
		{
			string templateText;
			Type templateSourceType = typeof(EmbeddedTemplateTextRetriever);
			Stream templateSourceStream;

			// Get the manifest resource for the requested item from the assembly in which the type is nested
			using (templateSourceStream = templateSourceType.Assembly.GetManifestResourceStream(templatePath))
			{
				// Check that the manifest was found
				if (templateSourceStream is null)
				{
					throw new ArgumentOutOfRangeException(nameof(templatePath), $"Logic error; unknown manifest requested with path '{templatePath}'!");
				}

				templateText = await ReadAllStreamTextAsync(templateSourceStream);
			}

			// Return the text that was loaded from the template
			return templateText;
		}

		#region Private Helper Methods

		/// <summary>
		/// Read all of the text from a stream and return that text
		/// </summary>
		/// <param name="stream">The stream from which to read all of the text</param>
		/// <returns>A string of all of the text retrieved from the file</returns>
		private async Task<string> ReadAllStreamTextAsync(Stream stream)
		{
			string contents;

			// Load the contents of the embedded resource into memory
			using (StreamReader reader = new StreamReader(stream))
			{
				contents = await reader.ReadToEndAsync();
			}

			return contents;
		}

		#endregion

	}
}
