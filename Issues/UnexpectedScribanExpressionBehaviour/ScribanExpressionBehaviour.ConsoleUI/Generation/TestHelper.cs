using System.Reflection.Metadata.Ecma335;

namespace ScribanExpressionBehaviour.ConsoleUI.Generation
{

	public static class TestHelper
	{

		/// <summary>
		/// Test method, which is used to test what happens in a comparison 
		/// when a method returns an empty string. This is a dumbed down example!
		/// </summary>
		/// <returns>An empty string, used for a comparison in the template</returns>
		public static string EmptyString() => string.Empty;

		/// <summary>
		/// Test method, which is used to test what happens in a comparison 
		/// when a method returns a non-empty string. This is a dumbed down example!
		/// </summary>
		/// <returns>A non-empty string, used for a comparison in the template</returns>
		public static string FullString() => "Full!";

	}

}