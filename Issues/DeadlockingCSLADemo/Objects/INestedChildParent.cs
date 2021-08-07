using System;
using System.Collections.Generic;
using System.Text;

namespace DeadlockingCSLADemo.Objects
{
	public interface INestedChildParent
	{

		int PersonId { get; }

		int? ParentNestedChildId { get; }

	}
}
