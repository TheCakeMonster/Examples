using Csla;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace BlazorExample.Client
{
	public class CslaAuthenticationStateWatcher : IDisposable
	{
		private readonly AuthenticationStateProvider _authenticationStateProvider;
		private readonly ApplicationContext _applicationContext;

		public CslaAuthenticationStateWatcher(AuthenticationStateProvider authenticationStateProvider, ApplicationContext applicationContext)
		{
			_authenticationStateProvider = authenticationStateProvider;
			_applicationContext = applicationContext;
			_applicationContext.Principal = new ClaimsPrincipal(new ClaimsIdentity());
			_authenticationStateProvider.AuthenticationStateChanged += OnAuthenticationStateChanged;
		}

		private async void OnAuthenticationStateChanged(Task<AuthenticationState> authStateTask)
		{
			AuthenticationState? authState = await authStateTask;
			if (authState is null || authState.User is null)
			{
				_applicationContext.Principal = new ClaimsPrincipal(new ClaimsIdentity());
				return;
			}
			_applicationContext.Principal = authState.User;
		}

		public void Dispose()
		{
			_authenticationStateProvider.AuthenticationStateChanged -= OnAuthenticationStateChanged;
		}

	}
}
