using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Text;
using TestApp.Services;
namespace TestApp.ViewModels
{
    public class ViewModelBase : BindableBase, INavigationAware, IDestructible
    {
        protected INavigationService NavigationService { get; private set; }

        protected IPageDialogService DialogService { get; private set; }
        protected readonly IGoogleManager _googleManager;
        protected readonly IFacebookManager _facebookManager;
        public ViewModelBase(IFacebookManager facebookManager)
        {
            this._facebookManager = facebookManager;
        }
		public ViewModelBase(IGoogleManager googleManager)
        {
            this._googleManager = googleManager;
        }
        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public ViewModelBase(INavigationService navigationService , IPageDialogService dialogService)
        {
            NavigationService = navigationService;
            DialogService = dialogService;
        }

		//public ViewModelBase(INavigationService navigationService, IPageDialogService dialogService, IFacebookManager facebookManager)
   //     {
			//this._facebookManager = facebookManager;
        //    NavigationService = navigationService;
        //    DialogService = dialogService;
        //}

		public ViewModelBase(INavigationService navigationService, IPageDialogService dialogService, IFacebookManager facebookManager, IGoogleManager googleManager)
        {
            this._facebookManager = facebookManager;
            this._googleManager = googleManager;
            NavigationService = navigationService;
            DialogService = dialogService;
        }


        public virtual void OnNavigatedFrom(NavigationParameters parameters)
        {
            
        }

        public virtual void OnNavigatedTo(NavigationParameters parameters)
        {
            
        }

        public virtual void OnNavigatingTo(NavigationParameters parameters)
        {
            
        }

        public virtual void Destroy()
        {
            
        }
    }
}
