using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using TestApp.Helpers;

namespace TestApp.ViewModels
{
	public class LogoutViewModel : ChildViewModelBase
    {
        public LogoutViewModel(INavigationService navigationService, IPageDialogService dialogService, IEventAggregator ea) : base(ea, navigationService, dialogService)
        {
            IsActiveChanged += HandleIsActiveTrue;
            IsActiveChanged += HandleIsActiveFalse;
            
        }
        private void HandleIsActiveTrue(object sender, EventArgs args)
        {
            if (IsActive == false) return;
            Settings.UserID = 0;
            //Settings.Token = "";
            NavigationService.NavigateAsync(new Uri("http://www.testapp.com/Login", UriKind.Absolute));
            // Handle Logic Here
        }

        private void HandleIsActiveFalse(object sender, EventArgs args)
        {
            if (IsActive == true) return;

            // Handle Logic Here
        }
    }
}
