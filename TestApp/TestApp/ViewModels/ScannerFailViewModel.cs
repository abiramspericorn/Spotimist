using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TestApp.ViewModels
{
	public class ScannerFailViewModel : ViewModelBase
	{
        #region Properties

        public DelegateCommand ScanAgain { get; set; }
        public DelegateCommand Close { get; set; }

        #endregion
        public ScannerFailViewModel(INavigationService navigationService, IPageDialogService dialogService) : base(navigationService, dialogService)
        {
            Close = new DelegateCommand(closenavigation);
            ScanAgain = new DelegateCommand(scanagainnaviagtion);
        }

        private void scanagainnaviagtion()
        {
            //NavigationService.NavigateAsync("CustomScanPage");
        }

        private void closenavigation()
        {
            NavigationService.NavigateAsync(new Uri("http://www.testapp.com/Home", UriKind.Absolute));
        }

        public override void OnNavigatedFrom(NavigationParameters parameters)
        {
            NavigationService.NavigateAsync(new Uri("http://www.testapp.com/Home", UriKind.Absolute));
        }
    }
}
