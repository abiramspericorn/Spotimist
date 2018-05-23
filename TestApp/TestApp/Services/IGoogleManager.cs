using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Models;

namespace TestApp.Services
{
	public interface IGoogleManager
	{
        void Login(Action<GoogleUser, string> OnLoginComplete);

		void Logout();
	}
}
