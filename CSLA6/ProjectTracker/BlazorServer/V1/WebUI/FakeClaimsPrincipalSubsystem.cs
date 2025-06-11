using Csla;
using Csla.Core;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Security.Principal;

namespace Microsoft.Extensions.DependencyInjection
{

    /// <summary>
    /// Extension methods on the IServiceCollection type
    /// </summary>
	internal static class ServiceCollectionExtensions
    {

        /// <summary>
        /// Register services used in the fake claims principal subsystem, which exists for
        /// testing of code containing authorization rules prior to an authentication 
        /// system being put in place
        /// </summary>
        /// <param name="services">The service collection being extended</param>
        /// <returns>The service collection, to support method chaining</returns>
        /// <exception cref="InvalidOperationException">Incorrect usage, such as calling after CSLA has been registered</exception>
		internal static IServiceCollection AddFakeClaimsPrincipalSubsystem(this IServiceCollection services)
        {
            bool contextManagersRegistered; 

            // Order of registration of context manager is vital; check this is the first context manager registered
            contextManagersRegistered = services.Any(type => type.ServiceType.Equals(typeof(IContextManager)));
            if (contextManagersRegistered)
            {
                throw new InvalidOperationException("This system must be added BEFORE the call to AddCsla()");
            }

			services.AddScoped<FakeClaimsPrincipalSystem.FakeClaimsPrincipalStore>();
            services.AddScoped<AuthenticationStateProvider, FakeClaimsPrincipalSystem.FakeAuthenticationStateProvider>();
            services.AddScoped<IContextManager, FakeClaimsPrincipalSystem.FakeApplicationContextManager>();

            return services;
        }
    }
}

namespace FakeClaimsPrincipalSystem
{

    /// <summary>
    /// Fake implementation of a CSLA application context manager, used for testing of applications 
    /// that make use of code containing authorisation rules before an authentication system has
    /// been put in place
    /// </summary>
    /// <remarks>
    /// You should swap out this implementation; DO NOT use this in real sites!
    /// </remarks>
    internal class FakeApplicationContextManager : IContextManager
    {
        private IContextDictionary _clientContext = new ContextDictionary();
        private IContextDictionary _localContext = new ContextDictionary();
        private readonly FakeClaimsPrincipalStore _principalStore;

        public FakeApplicationContextManager(FakeClaimsPrincipalStore principalStore)
        {
            _principalStore = principalStore;
        }

        public bool IsStatefulContext { get; } = true;

        public bool IsValid { get; } = true;

        public ApplicationContext ApplicationContext { get; set; }

        public IContextDictionary GetClientContext(ApplicationContext.ExecutionLocations executionLocation)
        {
            return _clientContext;
        }

        public IContextDictionary GetLocalContext()
        {
            return _localContext;
        }

        public IPrincipal GetUser()
        {
            return _principalStore.GetPrincipal();
        }

        public void SetClientContext(IContextDictionary clientContext, ApplicationContext.ExecutionLocations executionLocation)
        {
            _clientContext = clientContext;
        }

        public void SetLocalContext(IContextDictionary localContext)
        {
            _localContext = localContext;
        }

        public void SetUser(IPrincipal principal)
        {
            _principalStore.SetPrincipal(principal);
        }
    }

    /// <summary>
    /// Fake implementation of a Blazor authentication state provider, used for testing of applications 
    /// that make use of code containing authorisation rules before an authentication system has
    /// been put in place
    /// </summary>
    /// <remarks>
    /// You should swap out this implementation; DO NOT use this in real sites!
    /// </remarks>
    internal class FakeAuthenticationStateProvider : AuthenticationStateProvider
	{

        private readonly FakeClaimsPrincipalStore _principalStore;

        public FakeAuthenticationStateProvider(FakeClaimsPrincipalStore principalStore)
        {
            _principalStore = principalStore;
        }

		/// <summary>
		/// Retrieve the (fake) authentication state for this scenario
		/// </summary>
		/// <returns>The authentication state for use in auth decisions</returns>
		public override Task<AuthenticationState> GetAuthenticationStateAsync()
		{
			return Task.FromResult(new AuthenticationState(_principalStore.GetPrincipal()));
		}
	}

    /// <summary>
    /// Claims principal storage for use in fake claims principal system, which 
    /// exists to support testing of code including authorization rules before any 
    /// authentication system has been put in place
    /// </summary>
	internal class FakeClaimsPrincipalStore
    {
		private ClaimsPrincipal _principal = CreateFakePrincipal();

		/// <summary>
		/// Create a fake ClaimsPrincipal with some basic information, for testing
		/// </summary>
		/// <returns>A populated ClaimsPrincipal with some basic claims</returns>
		private static ClaimsPrincipal CreateFakePrincipal()
		{
			ClaimsIdentity identity;

			identity = new ClaimsIdentity(new GenericIdentity("Test User"));
			identity.AddClaim(new Claim(ClaimTypes.Role, "Users"));
            identity.AddClaim(new Claim(ClaimTypes.Role, "ProjectManager"));
            return new ClaimsPrincipal(identity);
		}

        /// <summary>
        /// Get the claims principal from storage
        /// </summary>
        /// <returns>The stored ClaimsPrincipal instance</returns>
		public ClaimsPrincipal GetPrincipal()
        {
			return _principal;
        }

        /// <summary>
        /// Set the claims principal being stored
        /// </summary>
        /// <param name="principal">The principal that is to be stored</param>
        public void SetPrincipal(IPrincipal principal)
        {
            ClaimsPrincipal? claimsPrincipal;

            if (principal is null) principal = new ClaimsPrincipal();
            claimsPrincipal = principal as ClaimsPrincipal;
            if (claimsPrincipal is null)
            {
                claimsPrincipal = new ClaimsPrincipal(principal);
            }
            _principal = claimsPrincipal;
        }

    }
}
