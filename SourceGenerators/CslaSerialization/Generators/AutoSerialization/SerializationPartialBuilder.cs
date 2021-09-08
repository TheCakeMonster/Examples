using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
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
			GenerationResults generationResults;
			IndentedTextWriter textWriter;

			using (StringWriter stringWriter = new StringWriter())
			{ 
				textWriter = new IndentedTextWriter(stringWriter, "\t");

				AppendDefaultUsingStatements(classDefinition, textWriter);
				AppendNamespaceDefinition(classDefinition, textWriter);
				textWriter.WriteLine("{");
				textWriter.Indent++;
				AppendClassDefinition(classDefinition, textWriter);
				textWriter.WriteLine("{");
				textWriter.Indent++;
				AppendGetChildrenMethod(classDefinition, textWriter);
				AppendGetStateMethod(classDefinition, textWriter);
				AppendSetChildrenMethod(classDefinition, textWriter);
				AppendSetStateMethod(classDefinition, textWriter);
				textWriter.Indent--;
				textWriter.WriteLine("}");
				textWriter.Indent--;
				textWriter.WriteLine("}");

				generationResults = new GenerationResults()
				{
					ClassName = classDefinition.ClassName,
					GeneratedSource = stringWriter.ToString()
				};
			}

			return generationResults;
		}

		#region Private Helper Methods

		/// <summary>
		/// Append the default using statements required on a partial class in
		/// order for it to compile the code we have generated
		/// </summary>
		/// <param name="classDefinition">The definition of the class for which we are generating</param>
		/// <param name="textWriter">The IndentedTextWriter instance to which to append the usings</param>
		private static void AppendDefaultUsingStatements(ExtractedClassDefinition classDefinition, IndentedTextWriter textWriter)
		{
			textWriter.WriteLine("using System;");
			textWriter.WriteLine("using Csla.Serialization.Mobile;");
			textWriter.WriteLine();
		}

		/// <summary>
		/// Append the definition of the namespace in which the partial class is to reside
		/// </summary>
		/// <param name="classDefinition">The definition of the class for which we are generating</param>
		/// <param name="textWriter">The IndentedTextWriter instance to which to append the usings</param>
		private void AppendNamespaceDefinition(ExtractedClassDefinition classDefinition, IndentedTextWriter textWriter)
		{
			textWriter.Write("namespace ");
			textWriter.Write(classDefinition.Namespace);
			textWriter.WriteLine();
		}

		/// <summary>
		/// Append the class definition of the partial class we are generating
		/// </summary>
		/// <param name="classDefinition">The definition of the class for which we are generating</param>
		/// <param name="textWriter">The IndentedTextWriter instance to which to append the class definition</param>
		private static void AppendClassDefinition(ExtractedClassDefinition classDefinition, IndentedTextWriter textWriter)
		{
			textWriter.WriteLine("[Serializable]");
			textWriter.Write(classDefinition.Scope);
			textWriter.Write(" partial class ");
			textWriter.Write(classDefinition.ClassName);
			textWriter.Write(" : IMobileObject");
			textWriter.WriteLine();
		}

		/// <summary>
		/// Append the definition of the GetChildren method required to fulfil the IMobileObject interface
		/// </summary>
		/// <param name="classDefinition">The definition of the class for which we are generating</param>
		/// <param name="textWriter">The IndentedTextWriter instance to which to append the method definition</param>
		private void AppendGetChildrenMethod(ExtractedClassDefinition classDefinition, IndentedTextWriter textWriter)
		{
			textWriter.WriteLine("void IMobileObject.GetChildren(SerializationInfo info, MobileFormatter formatter)");
			textWriter.WriteLine("{");
			textWriter.WriteLine("}");
			textWriter.WriteLine();
		}

		/// <summary>
		/// Append the definition of the SetChildren method required to fulfil the IMobileObject interface
		/// </summary>
		/// <param name="classDefinition">The definition of the class for which we are generating</param>
		/// <param name="textWriter">The IndentedTextWriter instance to which to append the method definition</param>
		private void AppendSetChildrenMethod(ExtractedClassDefinition classDefinition, IndentedTextWriter textWriter)
		{
			textWriter.WriteLine("void IMobileObject.SetChildren(SerializationInfo info, MobileFormatter formatter)");
			textWriter.WriteLine("{");
			textWriter.WriteLine("}");
			textWriter.WriteLine();
		}

		/// <summary>
		/// Append the definition of the GetState method required to fulfil the IMobileObject interface
		/// </summary>
		/// <param name="classDefinition">The definition of the class for which we are generating</param>
		/// <param name="textWriter">The IndentedTextWriter instance to which to append the method definition</param>
		private void AppendGetStateMethod(ExtractedClassDefinition classDefinition, IndentedTextWriter textWriter)
		{
			textWriter.WriteLine("void IMobileObject.GetState(SerializationInfo info)");
			textWriter.WriteLine("{");
			textWriter.Indent++;

			foreach (ExtractedPropertyDefinition propertyDefinition in classDefinition.Properties)
			{
				textWriter.Write("info.AddValue(nameof(");
				textWriter.Write(propertyDefinition.PropertyName);
				textWriter.Write("), ");
				textWriter.Write(propertyDefinition.PropertyName);
				textWriter.WriteLine(");");
			}

			textWriter.Indent--;
			textWriter.WriteLine("}");
			textWriter.WriteLine();
		}

		/// <summary>
		/// Append the definition of the SetState method required to fulfil the IMobileObject interface
		/// </summary>
		/// <param name="classDefinition">The definition of the class for which we are generating</param>
		/// <param name="textWriter">The IndentedTextWriter instance to which to append the method definition</param>
		private void AppendSetStateMethod(ExtractedClassDefinition classDefinition, IndentedTextWriter textWriter)
		{
			textWriter.WriteLine("void IMobileObject.SetState(SerializationInfo info)");
			textWriter.WriteLine("{");
			textWriter.Indent++;

			foreach (ExtractedPropertyDefinition propertyDefinition in classDefinition.Properties)
			{
				textWriter.Write(propertyDefinition.PropertyName);
				textWriter.Write(" = info.GetValue<");
				textWriter.Write(propertyDefinition.PropertyTypeName);
				textWriter.Write(">(nameof(");
				textWriter.Write(propertyDefinition.PropertyName);
				textWriter.WriteLine("));");
			}

			textWriter.Indent--;
			textWriter.WriteLine("}");
			textWriter.WriteLine();
		}

		#endregion

	}

}
