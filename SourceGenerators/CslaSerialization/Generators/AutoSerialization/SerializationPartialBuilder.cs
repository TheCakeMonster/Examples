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

				AppendDefaultUsingStatements(textWriter, classDefinition);

				AppendNamespaceDefinition(textWriter, classDefinition);
				textWriter.WriteLine("{");
				textWriter.Indent++;
				AppendClassDefinition(textWriter, classDefinition);
				textWriter.WriteLine("{");
				textWriter.Indent++;

				AppendGetChildrenMethod(textWriter, classDefinition);
				AppendGetStateMethod(textWriter, classDefinition);
				AppendSetChildrenMethod(textWriter, classDefinition);
				AppendSetStateMethod(textWriter, classDefinition);

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
		/// <param name="textWriter">The IndentedTextWriter instance to which to append the usings</param>
		/// <param name="classDefinition">The definition of the class for which we are generating</param>
		private static void AppendDefaultUsingStatements(IndentedTextWriter textWriter, ExtractedClassDefinition classDefinition)
		{
			textWriter.WriteLine("using System;");
			textWriter.WriteLine("using Csla.Serialization.Mobile;");
			textWriter.WriteLine();
		}

		/// <summary>
		/// Append the definition of the namespace in which the partial class is to reside
		/// </summary>
		/// <param name="textWriter">The IndentedTextWriter instance to which to append the usings</param>
		/// <param name="classDefinition">The definition of the class for which we are generating</param>
		private void AppendNamespaceDefinition(IndentedTextWriter textWriter, ExtractedClassDefinition classDefinition)
		{
			textWriter.Write("namespace ");
			textWriter.Write(classDefinition.Namespace);
			textWriter.WriteLine();
		}

		/// <summary>
		/// Append the class definition of the partial class we are generating
		/// </summary>
		/// <param name="textWriter">The IndentedTextWriter instance to which to append the class definition</param>
		/// <param name="classDefinition">The definition of the class for which we are generating</param>
		private static void AppendClassDefinition(IndentedTextWriter textWriter, ExtractedClassDefinition classDefinition)
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
		/// <param name="textWriter">The IndentedTextWriter instance to which to append the method definition</param>
		/// <param name="classDefinition">The definition of the class for which we are generating</param>
		private void AppendGetChildrenMethod(IndentedTextWriter textWriter, ExtractedClassDefinition classDefinition)
		{
			textWriter.WriteLine("void IMobileObject.GetChildren(SerializationInfo info, MobileFormatter formatter)");
			textWriter.WriteLine("{");
			textWriter.Indent++;
			if (HasChildrenToExpose(classDefinition))
			{
				textWriter.WriteLine("IMobileObject mobileObject;");
				textWriter.WriteLine("SerializationInfo childInfo;");
				textWriter.WriteLine();
			}

			foreach (ExtractedFieldDefinition fieldDefinition in classDefinition.Fields)
			{
				if (!fieldDefinition.IsTypeAutoSerializable && !fieldDefinition.IsTypeIMobileObject) continue;

				AppendSerializeChildFragment(textWriter, fieldDefinition.FieldName, fieldDefinition.FieldTypeName);
			}

			foreach (ExtractedPropertyDefinition propertyDefinition in classDefinition.Properties)
			{
				if (!propertyDefinition.IsTypeAutoSerializable && !propertyDefinition.IsTypeIMobileObject) continue;

				AppendSerializeChildFragment(textWriter, propertyDefinition.PropertyName, propertyDefinition.PropertyTypeName);
			}

			textWriter.Indent--;
			textWriter.WriteLine("}");
			textWriter.WriteLine();
		}

		/// <summary>
		/// Append the code fragment necessary to serialize an individual child member
		/// </summary>
		/// <param name="textWriter">The IndentedTextWriter instance to which to append the fragment</param>
		/// <param name="memberName">The name of the member we are writing for</param>
		/// <param name="memberTypeName">The name of the type of the member we are writing for</param>
		private void AppendSerializeChildFragment(IndentedTextWriter textWriter, string memberName, string memberTypeName)
		{
			textWriter.Write("mobileObject = ");
			textWriter.Write(memberName);
			textWriter.WriteLine(" as IMobileObject;");
			textWriter.WriteLine("childInfo = formatter.SerializeObject(mobileObject);");
			textWriter.Write("info.AddChild(nameof(");
			textWriter.Write(memberName);
			textWriter.WriteLine("), childInfo.ReferenceId, true);");
		}

		/// <summary>
		/// Append the definition of the SetChildren method required to fulfil the IMobileObject interface
		/// </summary>
		/// <param name="textWriter">The IndentedTextWriter instance to which to append the method definition</param>
		/// <param name="classDefinition">The definition of the class for which we are generating</param>
		private void AppendSetChildrenMethod(IndentedTextWriter textWriter, ExtractedClassDefinition classDefinition)
		{
			textWriter.WriteLine("void IMobileObject.SetChildren(SerializationInfo info, MobileFormatter formatter)");
			textWriter.WriteLine("{");
			textWriter.Indent++;
			if (HasChildrenToExpose(classDefinition))
			{
				textWriter.WriteLine("SerializationInfo.ChildData childData;");
				textWriter.WriteLine();
			}

			foreach (ExtractedFieldDefinition fieldDefinition in classDefinition.Fields)
			{
				if (!fieldDefinition.IsTypeAutoSerializable && !fieldDefinition.IsTypeIMobileObject) continue;

				AppendDeserializeChildFragment(textWriter, fieldDefinition.FieldName, fieldDefinition.FieldTypeName);
			}

			foreach (ExtractedPropertyDefinition propertyDefinition in classDefinition.Properties)
			{
				if (!propertyDefinition.IsTypeAutoSerializable && !propertyDefinition.IsTypeIMobileObject) continue;

				AppendDeserializeChildFragment(textWriter, propertyDefinition.PropertyName, propertyDefinition.PropertyTypeName);
			}

			textWriter.Indent--;
			textWriter.WriteLine("}");
			textWriter.WriteLine();
		}

		/// <summary>
		/// Append the code fragment necessary to deserialize an individual child member
		/// </summary>
		/// <param name="textWriter">The IndentedTextWriter instance to which to append the fragment</param>
		/// <param name="memberName">The name of the member we are writing for</param>
		/// <param name="memberTypeName">The name of the type of the member we are writing for</param>
		private void AppendDeserializeChildFragment(IndentedTextWriter textWriter, string memberName, string memberTypeName)
		{
			textWriter.Write("if (info.Children.ContainsKey(nameof(");
			textWriter.Write(memberName);
			textWriter.WriteLine(")))");
			textWriter.WriteLine("{");
			textWriter.Indent++;

			textWriter.Write("childData = info.Children[nameof(");
			textWriter.Write(memberName);
			textWriter.WriteLine(")];");
			textWriter.WriteLine();
			textWriter.Write(memberName);
			textWriter.Write(" = formatter.GetObject(childData.ReferenceId) as ");
			textWriter.Write(memberTypeName);
			textWriter.WriteLine(";");

			textWriter.Indent--;
			textWriter.WriteLine("}");
		}

		/// <summary>
		/// Append the definition of the GetState method required to fulfil the IMobileObject interface
		/// </summary>
		/// <param name="textWriter">The IndentedTextWriter instance to which to append the method definition</param>
		/// <param name="classDefinition">The definition of the class for which we are generating</param>
		private void AppendGetStateMethod(IndentedTextWriter textWriter, ExtractedClassDefinition classDefinition)
		{
			textWriter.WriteLine("void IMobileObject.GetState(SerializationInfo info)");
			textWriter.WriteLine("{");
			textWriter.Indent++;

			foreach (ExtractedFieldDefinition fieldDefinition in classDefinition.Fields)
			{
				if (fieldDefinition.IsTypeAutoSerializable || fieldDefinition.IsTypeIMobileObject) continue;

				AppendGetMemberStateFragment(textWriter, fieldDefinition.FieldName, fieldDefinition.FieldTypeName);
			}

			foreach (ExtractedPropertyDefinition propertyDefinition in classDefinition.Properties)
			{
				if (propertyDefinition.IsTypeAutoSerializable || propertyDefinition.IsTypeIMobileObject) continue;

				AppendGetMemberStateFragment(textWriter, propertyDefinition.PropertyName, propertyDefinition.PropertyTypeName);
			}

			textWriter.Indent--;
			textWriter.WriteLine("}");
			textWriter.WriteLine();
		}

		/// <summary>
		/// Append the code fragment necessary to get the state of an individual member
		/// </summary>
		/// <param name="textWriter">The IndentedTextWriter instance to which to append the fragment</param>
		/// <param name="memberName">The name of the member we are writing for</param>
		/// <param name="memberTypeName">The name of the type of the member we are writing for</param>
		private void AppendGetMemberStateFragment(IndentedTextWriter textWriter, string memberName, string memberTypeName)
		{
			textWriter.Write("info.AddValue(nameof(");
			textWriter.Write(memberName);
			textWriter.Write("), ");
			textWriter.Write(memberName);
			textWriter.WriteLine(");");
		}

		/// <summary>
		/// Append the definition of the SetState method required to fulfil the IMobileObject interface
		/// </summary>
		/// <param name="textWriter">The IndentedTextWriter instance to which to append the method definition</param>
		/// <param name="classDefinition">The definition of the class for which we are generating</param>
		private void AppendSetStateMethod(IndentedTextWriter textWriter, ExtractedClassDefinition classDefinition)
		{
			textWriter.WriteLine("void IMobileObject.SetState(SerializationInfo info)");
			textWriter.WriteLine("{");
			textWriter.Indent++;

			foreach (ExtractedFieldDefinition fieldDefinition in classDefinition.Fields)
			{
				if (fieldDefinition.IsTypeAutoSerializable || fieldDefinition.IsTypeIMobileObject) continue;

				AppendSetMemberStateMethod(textWriter, fieldDefinition.FieldName, fieldDefinition.FieldTypeName);
			}

			foreach (ExtractedPropertyDefinition propertyDefinition in classDefinition.Properties)
			{
				if (propertyDefinition.IsTypeAutoSerializable || propertyDefinition.IsTypeIMobileObject) continue;

				AppendSetMemberStateMethod(textWriter, propertyDefinition.PropertyName, propertyDefinition.PropertyTypeName);
			}

			textWriter.Indent--;
			textWriter.WriteLine("}");
			textWriter.WriteLine();
		}

		/// <summary>
		/// Append the code fragment necessary to set the state of an individual member
		/// </summary>
		/// <param name="textWriter">The IndentedTextWriter instance to which to append the fragment</param>
		/// <param name="memberName">The name of the member we are writing for</param>
		/// <param name="memberTypeName">The name of the type of the member we are writing for</param>
		private void AppendSetMemberStateMethod(IndentedTextWriter textWriter, string memberName, string memberTypeName)
		{
			textWriter.Write(memberName);
			textWriter.Write(" = info.GetValue<");
			textWriter.Write(memberTypeName);
			textWriter.Write(">(nameof(");
			textWriter.Write(memberName);
			textWriter.WriteLine("));");
		}

		/// <summary>
		/// Determine if a class definition exposes any members that must be treated as children
		/// </summary>
		/// <param name="classDefinition">The class definition to be checked for children</param>
		/// <returns>Boolean true of the definition exposes any members that have to be treated as children</returns>
		private bool HasChildrenToExpose(ExtractedClassDefinition classDefinition)
		{
			return classDefinition.Properties.Any(p => p.IsTypeAutoSerializable || p.IsTypeIMobileObject) ||
				classDefinition.Fields.Any(p => p.IsTypeAutoSerializable || p.IsTypeIMobileObject);
		}

		#endregion

	}

}
