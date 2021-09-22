using System;
using System.Collections.Generic;
using System.Text;

namespace CslaSerialization.Generators.AutoSerialization
{

	/// <summary>
	/// The contract which a member definition must fulfil
	/// </summary>
	public interface IMemberDefinition
	{

		/// <summary>
		/// The name of the member
		/// </summary>
		string MemberName { get; }

		/// <summary>
		/// The type definition of the member
		/// </summary>
		ExtractedMemberTypeDefinition TypeDefinition { get; }

	}

}
