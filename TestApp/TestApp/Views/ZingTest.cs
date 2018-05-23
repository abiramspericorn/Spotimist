using System;
using Prism.Navigation;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace TestApp.Views
{
    public class CustomScanPage : ContentPage
    {
        INavigationService _navigationService;
        public static ZXingScannerView zxing;
        ZXingDefaultOverlay overlay;

        public CustomScanPage() : base()
        {
            try
            {

                zxing = new ZXingScannerView
                {

                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand
                };

                var abort = new Button
                {
                    Text = "Abort",
                };


                abort.Clicked += Abort_Clicked;

                zxing.OnScanResult += (result) =>
                         Device.BeginInvokeOnMainThread(async () => {

                         // Stop analysis until we navigate away so we don't keep reading barcodes
                         zxing.IsAnalyzing = false;

                         // Show an alert
                         //await DisplayAlert("Scanned Barcode", result.Text, "OK");

                         // Navigate away
                         // await Navigation.PopAsync();

                         MessagingCenter.Send(result.Text, "Result");


                         //var myMainPage = new ScannerSuccess();
                         //var navPage = new NavigationPage(myMainPage);
                         //Navigation.PopModalAsync();
                         //zxing.IsScanning = false;

                         //if (!string.IsNullOrEmpty(result.Text))
                         //    await _navigationService.NavigateAsync("ScannerSuccess");
                         //else
                         //await _navigationService.NavigateAsync("ScannerFail");
                     });

                overlay = new ZXingDefaultOverlay
                {

                    TopText = "Hold your phone up to the barcode",
                    BottomText = "Scanning will happen automatically",
                    //ShowFlashButton = zxing.HasTorch, 


                };
                overlay.FlashButtonClicked += (sender, e) => {
                    zxing.IsTorchOn = !zxing.IsTorchOn;
                };


                var grid = new Grid
                {
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                };
                grid.Children.Add(zxing);
                grid.Children.Add(overlay);
                grid.Children.Add(abort);



                // The root page of your application
                Content = grid;
                //Content = abort;
            }
            catch (Exception)
            {

            }
        }

        protected override void OnAppearing()
        {
            zxing.IsScanning = true;
            zxing.IsAnalyzing = true;
            zxing.IsVisible = true;

            //NavigationService.
            //this._navigationService.GoBackAsync();
            //base.OnAppearing();
        }

        private void Abort_Clicked(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                zxing.IsScanning = false;
                MessagingCenter.Send("Yes", "Back");
            });
        }

        protected override void OnDisappearing()
        {
            zxing.IsScanning = false;
            zxing.IsAnalyzing = false;
            zxing.IsVisible = false;

            //base.OnDisappearing();
        }
        protected override bool OnBackButtonPressed()
        {
            MessagingCenter.Send("Yes", "Back");
            return base.OnBackButtonPressed();

        }
    }
}


