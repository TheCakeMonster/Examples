
using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace ExecutionFramework
{
	public class ControlledExecutor
	{

		/// <summary>
		/// Perform controlled execution of a private method on a type defined by the consumer
		/// </summary>
		/// <param name="fullTypeName">The name of the type that we are to execute a method upon</param>
		/// <param name="methodName">The name of the method we are to execute</param>
		public ExecutionResults Execute([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]string fullTypeName, string methodName)
		{
			Type? executorType;
			object? executor;
			MethodInfo? methodInfo;

			executorType = Type.GetType(fullTypeName);

			// Validate that it is a known type that is intended for this purpose
			if (executorType == null) return ExecutionResults.UnrecognisedType;
			// if (!executorType.IsSubclassOf(typeof(ExecutorBase))) return ExecutionResults.InvalidType;

			// Gain access to the method we are to execute
			methodInfo = executorType.GetMethod(methodName, BindingFlags.Instance | BindingFlags.NonPublic);
			if (methodInfo is null) return ExecutionResults.UnrecognisedMethod;

			// Create an instance of the type and execute the method
			executor = Activator.CreateInstance(executorType);
			if (executor is null) return ExecutionResults.Unknown;
			methodInfo.Invoke(executor, null);

			return ExecutionResults.Success;
		}

	}
}