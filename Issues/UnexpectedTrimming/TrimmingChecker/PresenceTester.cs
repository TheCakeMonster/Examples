using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace TrimmingChecker
{

	/// <summary>
	/// Tests for the presence of types and members at runtime using reflection
	/// </summary>
	public static class PresenceTester
	{

		/// <summary>
		/// Check if a method is present on a type at runtime, using reflection
		/// </summary>
		/// <param name="typeName">The full type name of the type under test</param>
		/// <param name="methodName">The method name whose presence we are testing for</param>
		/// <param name="bindingFlags">The binding flags necessary to identify the method</param>
		/// <returns>Boolean; true of the method is present, false if not</returns>
		public static bool IsMethodPresent(string typeName, string methodName, BindingFlags bindingFlags)
		{
			Type testType;
			MethodInfo methodInfo;

			testType = Type.GetType(typeName);
			if (testType is null) return false;

			methodInfo = testType.GetMethod(methodName, bindingFlags);

			return !(methodInfo is null);
		}

		/// <summary>
		/// Check if a type is present at runtime, using reflection
		/// </summary>
		/// <param name="typeName">The full type name of the type under test</param>
		/// <returns>Boolean; true of the type is present, false if not</returns>
		public static bool IsTypePresent(string typeName)
		{
			Type testType;

			testType = Type.GetType(typeName);

			return !(testType is null);
		}

	}
}
