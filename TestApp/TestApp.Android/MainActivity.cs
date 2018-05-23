using Acr.UserDialogs;
using Android.App;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Microsoft.Practices.Unity;
using Naxam.Controls.Platform.Droid;
using Prism.Unity;
using Xamarin;
using TestApp.Services;
using Xamarin.Forms;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Facebook;
using Xamarin.Facebook.AppEvents;
using TestApp.Droid;
using Android.Gms.Auth.Api.SignIn;
using Android.Gms.Common.Apis;
using static Android.Views.View;
using Android.Gms.Common;
using Android.Gms.Auth.Api;
using Android.Support.V4.View;



namespace TestApp.Droid
{
    [Activity(Label = "TestApp", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);
            FacebookSdk.SdkInitialize(this);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            DependencyService.Register<IFacebookManager, FacebookManager>();
            DependencyService.Register<IGoogleManager, GoogleManager>();
            UserDialogs.Init(this);
        
    //        var stateList = new Android.Content.Res.ColorStateList(
    //            new int[][] {
    //                new int[] { Android.Resource.Attribute.StateChecked
    //            },
    //            new int[] { Android.Resource.Attribute.StateEnabled
    //            }
    //            },
    //            new int[] {
    //                new  
                //Color(242,111,40), //Selected
                //   new  Color(64,65,68) //Normal
                //});

           // BottomTabbedRenderer.BackgroundColor = new Color(0x9C, 0x27, 0xB0);
            BottomTabbedRenderer.FontSize = 12f;
            BottomTabbedRenderer.IconSize = 30;
            // BottomTabbedRenderer.ItemTextColor = stateList;
            // BottomTabbedRenderer.ItemIconTintList = stateList;
           // BottomTabbedRenderer.Typeface = Typeface.CreateFromAsset(this.Assets, "architep.ttf");
           // BottomTabbedRenderer.ItemBackgroundResource = Resource.Drawable.bnv_selector;
            BottomTabbedRenderer.ItemSpacing = 4;
            //BottomTabbedRenderer.ItemPadding = new Xamarin.Forms.Thickness(6);
            BottomTabbedRenderer.BottomBarHeight = 56;
            BottomTabbedRenderer.ItemAlign = ItemAlignFlags.Center;




            ZXing.Net.Mobile.Forms.Android.Platform.Init();

            //Map Setup
            FormsMaps.Init(this, bundle);
            LoadApplication(new App(new AndroidInitializer()));
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            //base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
           // PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            global::ZXing.Net.Mobile.Android.PermissionsHandler.OnRequestPermissionsResult(requestCode, permissions, grantResults);

        }
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (requestCode == 1)
            {
                GoogleSignInResult result = Auth.GoogleSignInApi.GetSignInResultFromIntent(data);
                GoogleManager.Instance.OnAuthCompleted(result);
            }
            var manager = DependencyService.Get<IFacebookManager>();
            if (manager != null)
            {
                (manager as FacebookManager)._callbackManager.OnActivityResult(requestCode, (int)resultCode, data);
            }
        } 

    }

    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IUnityContainer container)
        {
            // Register any platform specific implementations
        }
    }
}

