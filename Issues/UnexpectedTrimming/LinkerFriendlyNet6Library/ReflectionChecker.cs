using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace LinkerFriendlyNet6Library
{
	public static class ReflectionChecker
	{

		public static bool IsMethodPresent(string typeName, string methodName, BindingFlags bindingFlags)
		{
			Type testType;
			MethodInfo methodInfo;

#pragma warning disable IL2026 // Methods annotated with 'RequiresUnreferencedCodeAttribute' require dynamic access otherwise can break functionality when trimming application code
			testType = Type.GetType(typeName);
			if (testType is null) return false;

#pragma warning restore IL2026 // Methods annotated with 'RequiresUnreferencedCodeAttribute' require dynamic access otherwise can break functionality when trimming application code
			methodInfo = testType.GetMethod(methodName, bindingFlags);

			return !(methodInfo is null);
		}

		public static bool IsTypePresent(string typeName)
		{
			Type testType;

#pragma warning disable IL2026 // Methods annotated with 'RequiresUnreferencedCodeAttribute' require dynamic access otherwise can break functionality when trimming application code
			testType = Type.GetType(typeName);
#pragma warning restore IL2026 // Methods annotated with 'RequiresUnreferencedCodeAttribute' require dynamic access otherwise can break functionality when trimming application code

			return !(testType is null);
		}

	}
}
