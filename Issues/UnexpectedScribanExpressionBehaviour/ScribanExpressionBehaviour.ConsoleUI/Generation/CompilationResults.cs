using System.Collections.Generic;

namespace ScribanExpressionBehaviour.ConsoleUI.Generation
{
	internal class CompilationResults
	{
		public IList<CompilationError> CompilationErrors { get; private set; } = new List<CompilationError>();

		public CompilationOutcomes Result { get; internal set; }

	}
}