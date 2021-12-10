using Csla;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.JSInterop;

namespace BlazorExample.Client
{
	public class CslaRemoteAuthenticationStateProvider : RemoteAuthenticationService<RemoteAuthenticationState, RemoteUserAccount, ApiAuthorizationProviderOptions>, IDisposable
	{

		private readonly ApplicationContext _applicationContext;

		public CslaRemoteAuthenticationStateProvider(IJSRuntime jsRuntime, 
			IOptionsSnapshot<RemoteAuthenticationOptions<ApiAuthorizationProviderOptions>> options, 
			NavigationManager navigation, AccountClaimsPrincipalFactory<RemoteUserAccount> accountClaimsPrincipalFactory, 
			ApplicationContext applicationContext) : base(jsRuntime, options, navigation, accountClaimsPrincipalFactory)
		{
			_applicationContext = applicationContext;
			base.AuthenticationStateChanged += OnAuthenticationStateChanged;
		}

		private async void OnAuthenticationStateChanged(Task<AuthenticationState> authenticationState)
		{
			var authStateResult = await authenticationState;
			_applicationContext.Principal = authStateResult.User;
		}

		public void Dispose()
		{
			base.AuthenticationStateChanged -= OnAuthenticationStateChanged;
		}
	}
}
