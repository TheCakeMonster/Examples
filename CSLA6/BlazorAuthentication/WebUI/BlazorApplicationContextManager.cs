using Csla.Core;
using Csla.Security;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Csla.Blazor
{

  /// <summary>
  /// IContextManager implementation for use in Blazor
  /// </summary>
  public class BlazorApplicationContextManager : IContextManager, IDisposable
  {
    private readonly AuthenticationStateProvider _authenticationStateProvider;
    private CslaClaimsPrincipal _storedPrincipal { get; set; }
    private ContextDictionary _localContext { get; set; } = new ContextDictionary();
    private ContextDictionary _clientContext { get; set; } = new ContextDictionary();
    private ApplicationContext _applicationContext { get; set; }

    /// <summary>
    /// Creates an instance of the object, initializing it with its dependencies.
    /// </summary>
    /// <param name="authenticationStateProvider">Blazor's authentication state provider</param>
    public BlazorApplicationContextManager(AuthenticationStateProvider authenticationStateProvider)
    {
      if (authenticationStateProvider is not IHostEnvironmentAuthenticationStateProvider)
      {
        throw new InvalidOperationException("This context manager must be used with an implementer of IHostEnvironmentAuthenticationStateProvider");
      }
      _authenticationStateProvider = authenticationStateProvider;
      _authenticationStateProvider.AuthenticationStateChanged += AuthenticationStateProvider_AuthenticationStateChanged;
      TryStartPrincipalInitialization();
    }

    /// <summary>
    /// Attempt to start the initialization of the Principal from the state provider
    /// </summary>
    /// <remarks>
    /// This can fail if we are created before the state provider is set up. If it fails in 
    /// this way then it doesn't matter, as we will receive notification when it is set up via the 
    /// authentication state provider's AuthenticationStateChanged event to which we are wired
    /// </remarks>
    private void TryStartPrincipalInitialization()
    {
      Task<AuthenticationState> authStateTask;

      try
      {
        authStateTask = _authenticationStateProvider.GetAuthenticationStateAsync();
      }
      catch (InvalidOperationException)
      {
        // The task that can be used to determine the authenticate state has not yet been defined
        // That's OK; the AuthenticationStateChanged event will be raised when it has, and we listen for that
        // This occasionally fails if we are created very early in the Blazor session's startup
        return;
      }

      // Start the process of retrieving the results from the task
      AuthenticationStateProvider_AuthenticationStateChanged(authStateTask);
    }

    /// <summary>
    /// Event handler for the AuthenticationStateChanged event raised by the AuthenticationStateProvider
    /// </summary>
    /// <param name="stateTask">The task which can be used to retrieve the authentication state</param>
    private void AuthenticationStateProvider_AuthenticationStateChanged(Task<AuthenticationState> stateTask)
    {
      // Set up a continuation to be called when the retrieval task completes
      stateTask.ContinueWith(t => AuthenticationStateProvider_AuthenticationStateRetrieved(t)).ConfigureAwait(false);
    }

    /// <summary>
    /// Completion handler for when the authentication state retrieval task completes
    /// </summary>
    /// <param name="completedStateTask">The task on whose completion we were waiting</param>
    private void AuthenticationStateProvider_AuthenticationStateRetrieved(Task<AuthenticationState> completedStateTask)
    {
      // The task is now complete, and the Result property won't block
      // Use the Result property to get the ClaimsPrincipal in play in Blazor
      StorePrincipal(completedStateTask.Result.User);
    }

    /// <summary>
    /// Store the principal that has been provided for return to Csla when needed
    /// </summary>
    /// <param name="principal">The IPrincipal that is active at the moment</param>
    private void StorePrincipal(System.Security.Principal.IPrincipal principal)
    {
      CslaClaimsPrincipal? cslaPrincipal = principal as CslaClaimsPrincipal;
      if (cslaPrincipal is null)
      {
        cslaPrincipal = new CslaClaimsPrincipal(principal);
      }

      _storedPrincipal = cslaPrincipal;
    }

    /// <summary>
    /// Gets the current principal.
    /// </summary>
    public System.Security.Principal.IPrincipal GetUser()
    {
      var result = _storedPrincipal;
      if (result is null)
      {
        result = new CslaClaimsPrincipal();
        _storedPrincipal = result;
      }
      return result;
    }

    /// <summary>
    /// Sets the current principal.
    /// </summary>
    /// <param name="principal">Principal object.</param>
    public void SetUser(System.Security.Principal.IPrincipal principal)
    {
      // Store the principal and then tell Blazor that it has changed
      StorePrincipal(principal);
      NotifyAuthenticationStateChanged(principal);
    }

    /// <summary>
    /// Raise an event to inform Blazor and its consumers of the state change
    /// </summary>
    private void NotifyAuthenticationStateChanged(System.Security.Principal.IPrincipal principal)
    {
      ClaimsPrincipal? claimsPrincipal = principal as ClaimsPrincipal;
      if (claimsPrincipal is null)
      {
        claimsPrincipal = new ClaimsPrincipal(principal);
      }
      
      IHostEnvironmentAuthenticationStateProvider? stateProvider =
        _authenticationStateProvider as IHostEnvironmentAuthenticationStateProvider;
      Task<AuthenticationState> authenticationState = Task.FromResult(new AuthenticationState(claimsPrincipal));
      stateProvider?.SetAuthenticationState(authenticationState);
    }

    /// <summary>
    /// Gets the local context.
    /// </summary>
    public ContextDictionary GetLocalContext()
    {
      return _localContext;
    }

    /// <summary>
    /// Sets the local context.
    /// </summary>
    /// <param name="localContext">Local context.</param>
    public void SetLocalContext(ContextDictionary localContext)
    {
      _localContext = localContext;
    }

    /// <summary>
    /// Gets the client context.
    /// </summary>
    /// <param name="executionLocation"></param>
    public ContextDictionary GetClientContext(ApplicationContext.ExecutionLocations executionLocation)
    {
      return _clientContext;
    }

    /// <summary>
    /// Sets the client context.
    /// </summary>
    /// <param name="clientContext">Client context.</param>
    /// <param name="executionLocation"></param>
    public void SetClientContext(ContextDictionary clientContext, ApplicationContext.ExecutionLocations executionLocation)
    {
      _clientContext = clientContext;
    }

    /// <summary>
    /// Gets or sets a reference to the current ApplicationContext.
    /// </summary>
    public virtual ApplicationContext ApplicationContext
    {
      get
      {
        return _applicationContext;
      }
      set
      {
        _applicationContext = value;
      }
    }

    /// <summary>
    /// Gets a value indicating whether this context manager is valid for 
    /// use in the current environment.
    /// </summary>
    public bool IsValid => true;

    /// <summary>
    /// Disposal method called by .NET when the class is being disposed
    /// </summary>
    public void Dispose()
    {
      // Unregister the event handler to avoid memory leaks
      _authenticationStateProvider.AuthenticationStateChanged -= AuthenticationStateProvider_AuthenticationStateChanged;
    }
  }
}
