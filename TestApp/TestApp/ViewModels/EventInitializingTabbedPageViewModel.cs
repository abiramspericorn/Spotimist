using Prism.Events;
using System;
using System.Collections.Generic;
using System.Text;
using TestApp.Events;

namespace TestApp.ViewModels
{
    public class EventInitializingTabbedPageViewModel : BaseViewModel
    {
        IEventAggregator _ea { get; }
        public EventInitializingTabbedPageViewModel(IEventAggregator eventAggregator)
        {
            _ea = eventAggregator;
            Title = "Event Initialized";
        }

        public override void OnNavigatingTo(Prism.Navigation.NavigationParameters parameters)
        {
            System.Diagnostics.Debug.WriteLine($"{Title} OnNavigatingTo");
            _ea.GetEvent<InitializeTabbedChildrenEvent>().Publish(parameters);
        }
    }
}
