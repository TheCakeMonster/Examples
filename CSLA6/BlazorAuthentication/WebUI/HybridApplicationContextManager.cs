using Csla.Core;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Principal;

namespace Csla.Blazor
{
  /// <summary>
  /// An application context manager which is able to switch between several implementations based
  /// on whether we are within or outside of Blazor. All operations are delegated to the context 
  /// manager that is chosen during the initialisation of this type
  /// </summary>
  public class HybridApplicationContextManager : IContextManager, IDisposable
  {
    private readonly IContextManager _wrappedContextManager;

    /// <summary>
    /// Constructor, during which we decide which implementation of the context manager is needed
    /// </summary>
    /// <param name="blazorIdentification">The identification of in what context we are running</param>
    /// <param name="authenticationStateProvider">The provider used to retrieve authentication state in Blazor</param>
    public HybridApplicationContextManager(BlazorIdentification blazorIdentification, AuthenticationStateProvider authenticationStateProvider, IHttpContextAccessor httpContextAccessor)
    {
      if (blazorIdentification.IsServerSideBlazor)
      {
        _wrappedContextManager = new BlazorApplicationContextManager(authenticationStateProvider);
        return;
      }
      // TODO: This needs to be the context manager from Csla.AspNetCore. I have currently copied that
      // implementation into this demo project, as the Csla.Blazor assembly doesn't reference Csla.AspNetCore :-(
      // This suggests we would probably need to copy it into Csla.Blazor rather than reference it
      _wrappedContextManager = new HttpApplicationContextManager(httpContextAccessor);
    }

    /// <summary>
    /// Gets or sets a reference to the current ApplicationContext.
    /// </summary>
    public ApplicationContext ApplicationContext
    {
      get { return _wrappedContextManager.ApplicationContext; }
      set { _wrappedContextManager.ApplicationContext = value; }
    }

    /// <summary>
    /// Gets the current principal.
    /// </summary>
    public IPrincipal GetUser()
    {
      return _wrappedContextManager.GetUser();
    }

    /// <summary>
    /// Sets the current principal.
    /// </summary>
    /// <param name="principal">Principal object.</param>
    public void SetUser(IPrincipal principal)
    {
      _wrappedContextManager.SetUser(principal);
    }

    /// <summary>
    /// Gets the client context.
    /// </summary>
    /// <param name="executionLocation"></param>
    public ContextDictionary GetClientContext(ApplicationContext.ExecutionLocations executionLocation)
    {
      return _wrappedContextManager.GetClientContext(executionLocation);
    }

    /// <summary>
    /// Sets the client context.
    /// </summary>
    /// <param name="clientContext">Client context.</param>
    /// <param name="executionLocation">Execution location.</param>
    public void SetClientContext(ContextDictionary clientContext, ApplicationContext.ExecutionLocations executionLocation)
    {
      _wrappedContextManager.SetClientContext(clientContext, executionLocation);
    }

    /// <summary>
    /// Gets the local context.
    /// </summary>
    public ContextDictionary GetLocalContext()
    {
      return _wrappedContextManager.GetLocalContext();
    }

    /// <summary>
    /// Sets the local context.
    /// </summary>
    /// <param name="localContext">Local context.</param>
    public void SetLocalContext(ContextDictionary localContext)
    {
      _wrappedContextManager.SetLocalContext(localContext);
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
      // Delegate the disposal of us to any wrapped implementation requiring it
      if (_wrappedContextManager is IDisposable disposable)
        disposable.Dispose();
    }
  }
}
