using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace LinkerFriendlyNetStandardLibrary
{
	public static class ReflectionChecker
	{

		public static bool IsMethodPresent(string typeName, string methodName, BindingFlags bindingFlags)
		{
			Type testType;
			MethodInfo methodInfo;

			testType = Type.GetType(typeName);
			if (testType is null) return false;

			methodInfo = testType.GetMethod(methodName, bindingFlags);

			return !(methodInfo is null);
		}

		public static bool IsTypePresent(string typeName)
		{
			Type testType;

			testType = Type.GetType(typeName);

			return !(testType is null);
		}

	}
}
