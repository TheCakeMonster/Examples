using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CslaSerialization.Generators.AutoSerialization
{

	/// <summary>
	/// Builds the text of a partial class that implements the IMobileObject
	/// interface so that the target class automatically offers mobile serialization
	/// </summary>
	internal class SerializationPartialBuilder
	{

		/// <summary>
		/// Build the text of a partial class that implements the IMobileObject
		/// interface so that the target class automatically offers mobile serialization
		/// </summary>
		/// <param name="classDefinition">The definition of the class for which serialization is required</param>
		/// <returns>Generated code to fulfil the required serialization</returns>
		internal GenerationResults BuildPartialClass(ExtractedClassDefinition classDefinition)
		{
			StringBuilder stringBuilder = new StringBuilder();

			AppendDefaultUsingStatements(classDefinition, stringBuilder);
			AppendNamespaceDefinition(classDefinition, stringBuilder);
			stringBuilder.Append("{\r\n");
			AppendClassDefinition(classDefinition, stringBuilder);
			stringBuilder.Append("\t{\r\n");
			AppendGetChildrenMethod(classDefinition, stringBuilder);
			AppendGetStateMethod(classDefinition, stringBuilder);
			AppendSetChildrenMethod(classDefinition, stringBuilder);
			AppendSetStateMethod(classDefinition, stringBuilder);
			stringBuilder.Append("\t}\r\n");
			stringBuilder.Append("}\r\n");

			return new GenerationResults()
			{
				ClassName = classDefinition.ClassName,
				GeneratedSource = stringBuilder.ToString()
			};
		}

		#region Private Helper Methods

		/// <summary>
		/// Append the default using statements required on a partial class in
		/// order for it to compile the code we have generated
		/// </summary>
		/// <param name="classDefinition">The definition of the class for which we are generating</param>
		/// <param name="stringBuilder">The StringBuilder instance to which to append the usings</param>
		private static void AppendDefaultUsingStatements(ExtractedClassDefinition classDefinition, StringBuilder stringBuilder)
		{
			stringBuilder.AppendLine("using System;");
			stringBuilder.AppendLine("using Csla.Serialization.Mobile;");
			stringBuilder.AppendLine();
		}

		/// <summary>
		/// Append the definition of the namespace in which the partial class is to reside
		/// </summary>
		/// <param name="classDefinition">The definition of the class for which we are generating</param>
		/// <param name="stringBuilder">The StringBuilder instance to which to append the usings</param>
		private void AppendNamespaceDefinition(ExtractedClassDefinition classDefinition, StringBuilder stringBuilder)
		{
			stringBuilder.Append("namespace ");
			stringBuilder.Append(classDefinition.Namespace);
			stringBuilder.Append("\r\n");
		}

		/// <summary>
		/// Append the class definition of the partial class we are generating
		/// </summary>
		/// <param name="classDefinition">The definition of the class for which we are generating</param>
		/// <param name="stringBuilder">The StringBuilder instance to which to append the class definition</param>
		private static void AppendClassDefinition(ExtractedClassDefinition classDefinition, StringBuilder stringBuilder)
		{
			stringBuilder.Append("\t[Serializable]\r\n");
			stringBuilder.Append("\t");
			stringBuilder.Append(classDefinition.Scope);
			stringBuilder.Append(" partial class ");
			stringBuilder.Append(classDefinition.ClassName);
			stringBuilder.Append(" : IMobileObject");
			stringBuilder.Append("\r\n");
		}

		/// <summary>
		/// Append the definition of the GetChildren method required to fulfil the IMobileObject interface
		/// </summary>
		/// <param name="classDefinition">The definition of the class for which we are generating</param>
		/// <param name="stringBuilder">The StringBuilder instance to which to append the method definition</param>
		private void AppendGetChildrenMethod(ExtractedClassDefinition classDefinition, StringBuilder stringBuilder)
		{
			stringBuilder.AppendLine("\t\tvoid IMobileObject.GetChildren(SerializationInfo info, MobileFormatter formatter)");
			stringBuilder.AppendLine("\t\t{");
			stringBuilder.AppendLine("\t\t}");
			stringBuilder.AppendLine();
		}

		/// <summary>
		/// Append the definition of the SetChildren method required to fulfil the IMobileObject interface
		/// </summary>
		/// <param name="classDefinition">The definition of the class for which we are generating</param>
		/// <param name="stringBuilder">The StringBuilder instance to which to append the method definition</param>
		private void AppendSetChildrenMethod(ExtractedClassDefinition classDefinition, StringBuilder stringBuilder)
		{
			stringBuilder.AppendLine("\t\tvoid IMobileObject.SetChildren(SerializationInfo info, MobileFormatter formatter)");
			stringBuilder.AppendLine("\t\t{");
			stringBuilder.AppendLine("\t\t}");
			stringBuilder.AppendLine();
		}

		/// <summary>
		/// Append the definition of the GetState method required to fulfil the IMobileObject interface
		/// </summary>
		/// <param name="classDefinition">The definition of the class for which we are generating</param>
		/// <param name="stringBuilder">The StringBuilder instance to which to append the method definition</param>
		private void AppendGetStateMethod(ExtractedClassDefinition classDefinition, StringBuilder stringBuilder)
		{
			stringBuilder.AppendLine("\t\tvoid IMobileObject.GetState(SerializationInfo info)");
			stringBuilder.AppendLine("\t\t{");

			foreach (ExtractedPropertyDefinition propertyDefinition in classDefinition.Properties)
			{
				stringBuilder.Append("\t\t\tinfo.AddValue(nameof(");
				stringBuilder.Append(propertyDefinition.PropertyName);
				stringBuilder.Append("), ");
				stringBuilder.Append(propertyDefinition.PropertyName);
				stringBuilder.Append(");\r\n");
			}

			stringBuilder.AppendLine("\t\t}");
			stringBuilder.AppendLine();
		}

		/// <summary>
		/// Append the definition of the SetState method required to fulfil the IMobileObject interface
		/// </summary>
		/// <param name="classDefinition">The definition of the class for which we are generating</param>
		/// <param name="stringBuilder">The StringBuilder instance to which to append the method definition</param>
		private void AppendSetStateMethod(ExtractedClassDefinition classDefinition, StringBuilder stringBuilder)
		{
			stringBuilder.AppendLine("\t\tvoid IMobileObject.SetState(SerializationInfo info)");
			stringBuilder.AppendLine("\t\t{");

			foreach (ExtractedPropertyDefinition propertyDefinition in classDefinition.Properties)
			{
				stringBuilder.Append("\t\t\t");
				stringBuilder.Append(propertyDefinition.PropertyName);
				stringBuilder.Append(" = info.GetValue<");
				stringBuilder.Append(propertyDefinition.PropertyTypeName);
				stringBuilder.Append(">(nameof(");
				stringBuilder.Append(propertyDefinition.PropertyName);
				stringBuilder.Append("));\r\n");
			}

			stringBuilder.AppendLine("\t\t}");
			stringBuilder.AppendLine();
		}

		#endregion

	}

}
