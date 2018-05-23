using TestApp.ViewModels;
using TestApp.Views;
using Microsoft.Practices.Unity;
using Prism.Unity;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System;
using TestApp.Helpers;
using TestApp.Services;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace TestApp
{
    public partial class App : PrismApplication
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            //if (Settings.UserID != null && Settings.Token != "")
            if (Settings.UserID != 0)
            {
                await NavigationService.NavigateAsync(new Uri("http://www.testapp.com/Home", UriKind.Absolute));
            }else{
                await NavigationService.NavigateAsync("Login");
            }

        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<NavigationPage>();
            Container.RegisterTypeForNavigation<MainPage>();
            Container.RegisterTypeForNavigation<Detail>();
            Container.RegisterTypeForNavigation<Scanner>();
            Container.RegisterTypeForNavigation<Home>();
            Container.RegisterTypeForNavigation<ScannerSuccess>();
            Container.RegisterTypeForNavigation<ScannerFail>();
            Container.RegisterTypeForNavigation<Login>();
            Container.RegisterTypeForNavigation<CustomScanPage>();
            Container.RegisterTypeForNavigation<Logout>();

        }

        protected override void OnStart()
        {
            base.OnStart();
        }
        

        protected override void OnResume()
        {
            base.OnResume();
        }
        protected override void OnSleep()
        {
            base.OnSleep();
        }

    }
}
