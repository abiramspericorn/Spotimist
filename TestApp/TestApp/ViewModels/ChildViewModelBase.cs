using Prism;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using TestApp.Events;

namespace TestApp.ViewModels
{
    public class ChildViewModelBase : BaseViewModel, IActiveAware, INavigatingAware, IDestructible
    {
        protected bool HasInitialized { get; set; }
        protected INavigationService NavigationService { get; private set; }

        protected IPageDialogService DialogService { get; private set; }
        IEventAggregator _ea { get; }
        public event EventHandler IsActiveChanged;

        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        private bool _isActive;
        public bool IsActive
        {
            get { return _isActive; }
            set { SetProperty(ref _isActive, value, RaiseIsActiveChanged); }
        }
        //public bool IsActive
        //{
        //    get { return _isActive; }
        //    set { SetProperty(ref _isActive, value, () => System.Diagnostics.Debug.WriteLine($"{Title} IsActive Changed: {value}")); }
        //}
        public ChildViewModelBase(IEventAggregator eventAggregator, INavigationService navigationService, IPageDialogService dialogService)
        {
            _ea = eventAggregator;

            NavigationService = navigationService;

            DialogService = dialogService;

            _ea.GetEvent<InitializeTabbedChildrenEvent>().Subscribe(OnInitializationEventFired);

            IsActiveChanged += (sender, e) => System.Diagnostics.Debug.WriteLine($"{Title} IsActive: {IsActive}");
        }
        void OnInitializationEventFired(NavigationParameters parameters)
        {
            System.Diagnostics.Debug.WriteLine($"{Title} Detected an initialization event");
            var message = parameters.GetValue<string>("message");
            Message = $"{Title} Initialized by Event: {message}";
        }
        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            System.Diagnostics.Debug.WriteLine($"{Title} is executing OnNavigatingTo");
            var message = parameters.GetValue<string>("message");
            Message = $"{Title} Initialized by OnNavigatingTo: {message}";
        }
        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            System.Diagnostics.Debug.WriteLine($"{Title} is executing OnNavigatedTo");
            var message = parameters.GetValue<string>("message");
            Message = $"{Title} Initialized by OnNavigatedTo: {message}";
        }

        public override void Destroy()
        {
            System.Diagnostics.Debug.WriteLine($"{Title} is being Destroyed!");
            _ea.GetEvent<InitializeTabbedChildrenEvent>().Unsubscribe(OnInitializationEventFired);
        }
        protected virtual void RaiseIsActiveChanged()
        {
            IsActiveChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
