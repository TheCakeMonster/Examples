using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;

// Generated from the built-in Scriban Navigator template

namespace ProjectTracker.UIControl.Navigation
{
	public class Navigator : INavigator
	{

		private readonly NavigationManager _navigationManager;

		public Navigator(NavigationManager navigationManager)
		{
			_navigationManager = navigationManager;
		}

		public void NavigateTo(string uri)
		{
			_navigationManager.NavigateTo(uri);
		}
	}
}
