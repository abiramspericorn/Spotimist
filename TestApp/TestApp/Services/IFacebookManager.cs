using TestApp.Models;
using System;

namespace TestApp.Services
{
	public interface IFacebookManager
	{				
		void Login(Action<FacebookUser, string> onLoginComplete);

		void Logout();
	}
}
