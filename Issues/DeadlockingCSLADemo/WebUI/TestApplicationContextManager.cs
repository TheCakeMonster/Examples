using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace DeadlockingCSLADemo.WebUI
{

	/// <summary>
	/// Fake implementation of a CSLA context manager, used for testing of applications that
	/// make use of CSLA classes with authorisation rules before an authentication system
	/// has been put in place
	/// </summary>
	/// <remarks>
	/// You should swap out this implementation; DO NOT use this in real sites!
	/// </remarks>
	public class TestApplicationContextManager : Csla.Core.ApplicationContextManager
	{

		public TestApplicationContextManager()
		{

		}

		/// <summary>
		/// Override that returns a default, test user principal that can be used
		/// to gain access to CSLA classes that have authorisation rules applied to them
		/// </summary>
		/// <returns>An IPrincipal that indicates that the user is logged in, and a member of a few default roles</returns>
		public override IPrincipal GetUser()
		{
			return new GenericPrincipal(new GenericIdentity("Test"), new string[] { "Users" });
		}

		/// <summary>
		/// Override for the default behaviour of the base class, which informs the caller
		/// that this class is not intended for scenarios involving any real authentication mechanism
		/// </summary>
		/// <param name="principal">The principal that is being assigned to the user</param>
		public override void SetUser(IPrincipal principal)
		{
			throw new NotImplementedException("TestApplicationContextManager should not be used for real sites!");
		}

	}
}
