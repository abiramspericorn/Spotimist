using Foundation;
using UIKit;
using Microsoft.Practices.Unity;
using Prism.Unity;
using Xamarin;
using TestApp.Services;
using Facebook.CoreKit;
using Xamarin.Forms;
using System;
using Google.SignIn;

namespace TestApp.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            try
            {
                global::Xamarin.Forms.Forms.Init();

                //Map Setup
                FormsMaps.Init();
				DependencyService.Register<IFacebookManager, FacebookManager>();
				DependencyService.Register<IGoogleManager, GoogleManager>();
				var googleServiceDictionary = NSDictionary.FromFile("GoogleService-Info.plist");
                Console.WriteLine(googleServiceDictionary);
            
                SignIn.SharedInstance.ClientID = googleServiceDictionary["CLIENT_ID"].ToString();

                //Scanner Setup
                ZXing.Net.Mobile.Forms.iOS.Platform.Init();

				LoadApplication(new App(new iOSInitializer()));
                return base.FinishedLaunching(app, options);
            }
            catch (System.Exception)
            {
                return true;
                //  throw;
            }

        }

        public override void OnActivated(UIApplication uiApplication)
        {
            base.OnActivated(uiApplication);
            AppEvents.ActivateApp();
        }
        public override bool OpenUrl(UIApplication app, NSUrl url, NSDictionary options)
        {
            var openUrlOptions = new UIApplicationOpenUrlOptions(options);
            var facebookDidHandle = ApplicationDelegate.SharedInstance.OpenUrl(app, url, openUrlOptions.SourceApplication, openUrlOptions.Annotation);
            var googleDidHandle = SignIn.SharedInstance.HandleUrl(url, openUrlOptions.SourceApplication, openUrlOptions.Annotation);
            return googleDidHandle || facebookDidHandle;
        }

    }

    public class iOSInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IUnityContainer container)
        {

        }
    }
}
