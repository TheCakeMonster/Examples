using Csla;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace BlazorExample.Client
{
	public class CslaAuthenticationState : ComponentBase
	{
		[Microsoft.AspNetCore.Components.Inject]
		private ApplicationContext? _applicationContext { get; set; }

		[CascadingParameter]
		public Task<AuthenticationState>? AuthenticationStateTask { get; set; }

		protected override void OnInitialized()
		{
			base.OnInitialized();
			if (_applicationContext is not null && 
				(_applicationContext.Principal is null || _applicationContext.Principal.Identity is null))
			{
				_applicationContext.Principal = new ClaimsPrincipal(new ClaimsIdentity());
			}
		}

		protected override async Task OnParametersSetAsync()
		{
			await base.OnParametersSetAsync();
			if (_applicationContext is not null && AuthenticationStateTask is not null)
			{
				var authState = await AuthenticationStateTask;
				_applicationContext.Principal = authState.User;
			}
		}

	}
}
