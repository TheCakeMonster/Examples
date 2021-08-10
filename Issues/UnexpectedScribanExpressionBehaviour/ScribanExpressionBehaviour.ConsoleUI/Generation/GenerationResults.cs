using System.Collections;
using System.Collections.Generic;

namespace ScribanExpressionBehaviour.ConsoleUI.Generation
{
	internal class GenerationResults
	{

		public string GeneratedText { get; internal set; }

		public GenerationOutcomes Result { get; internal set; }

		public IList<GenerationError> GenerationErrors { get; private set; } = new List<GenerationError>();

	}
}