using Prism.Events;
using Prism.Navigation;
using Prism.Services;
using System;
using Xamarin.Forms;
using Acr.UserDialogs;
using System.Net.Http;
using TestApp.Helpers;
using System.Net.Http.Headers;
using TestApp.Models;
using Newtonsoft.Json;
using TestApp.Views;

namespace TestApp.ViewModels
{
    public class ScannerViewModel : ChildViewModelBase
    {
        #region Properties


        public static QRResult QrCodeResult;

        public QRResult QRResult
        {
            get { return QrCodeResult; }
            set { SetProperty(ref QrCodeResult, value); }
        }
        #endregion


        public ScannerViewModel(INavigationService navigationService, IPageDialogService dialogService, IEventAggregator ea) : base(ea, navigationService, dialogService)
        {
            IsActiveChanged += HandleIsActiveTrue;
            IsActiveChanged += HandleIsActiveFalse;
            if (IsActive)
            {
                OpenScanner();
            }

        }

        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);
            if (HasInitialized) return;

            HasInitialized = true;
            if (IsActive)
            {
                OpenScanner();
            }
        }

        private void HandleIsActiveTrue(object sender, EventArgs args)
        {
            if (IsActive == false) return;
             OpenScanner();
            // Handle Logic Here
        }

        private void HandleIsActiveFalse(object sender, EventArgs args)
        {
            if (IsActive == true) 
                return;

            // Handle Logic Here
        }

        public override void Destroy()
        {
            IsActiveChanged -= HandleIsActiveTrue;
            IsActiveChanged -= HandleIsActiveFalse;
            //IsActive = true;
        }

        //private void OpenScanner(){
        //    var scanPage = new ZXingScannerPage();

        //    scanPage.OnScanResult += (result) => {
        //        // Stop scanning
        //        scanPage.IsScanning = false;

        //        // Pop the page and show the result
        //        Device.BeginInvokeOnMainThread(() => {
        //            //Navigation.PopAsync();
        //            //DisplayAlert("Scanned Barcode", result.Text, "OK");
        //        });
        //    };

        //    // Navigate to our scanner page
        //    await Navigation.PushAsync(scanPage);
        //}

        private void OpenScanner()
        {
           NavigationService.NavigateAsync("CustomScanPage");



            //MessagingCenter.Subscribe<Scanner,string>(this, "Result", (model) =>
            //{
            //    if (!string.IsNullOrEmpty(model))
            //        NavigationService.NavigateAsync("ScannerSuccess");
            //    else
            //        NavigationService.NavigateAsync("ScannerFail");

            //});
           



            MessagingCenter.Subscribe<string>(this, "Result", async (model) =>
            {
                if (!string.IsNullOrEmpty(model))
                {
                    Settings.QrCode = model;
                    Console.WriteLine(Settings.QrCode);
                    Console.WriteLine(Settings.MyBusinessid);
                    InitAsync();
                }
                else{
                    var a = await DialogService.DisplayAlertAsync("", "error!, This is not a valid booking, please try again", "OK", "Cancel");
                    CustomScanPage.zxing.IsAnalyzing = true;
                }
                    

                //await DialogService.DisplayAlertAsync("", "error!, This is not a valid booking, please try again", "OK");
            });

            MessagingCenter.Subscribe<string>(this, "Back", (model) =>
            {
               if(model=="Yes")
                     NavigationService.NavigateAsync(new Uri("http://www.testapp.com/Home", UriKind.Absolute));
            });

            //var ScannerPage = new CustomScanPage();

            //ScannerPage.IsScanning = true;

            //ScannerPage.OnScanResult += (ZXing.Result result) =>
            //{

            //    // Stop scanning
            //    ScannerPage.IsScanning = false;

            //    // Alert with scanned code
            //    Device.BeginInvokeOnMainThread(() =>
            //    {
            //        DialogService.DisplayAlertAsync("", $"Result: {result.Text}", "OK");
            //    });

            //    if (!string.IsNullOrEmpty(result.Text))
            //        NavigationService.NavigateAsync("ScannerSuccess");
            //    else
            //        NavigationService.NavigateAsync("ScannerFail");
            //};

            //ScannerPage.IsScanning = true;
        }

        private async void InitAsync()
        {
            try
            {
                UserDialogs.Instance.ShowLoading(null, MaskType.None);
                HttpClient client = new HttpClient();

                if (!string.IsNullOrEmpty(Settings.Token))
                {
                    string _ContentType = "application/json";
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(_ContentType));
                    client.DefaultRequestHeaders.Add("Authorization", $"Token {Settings.Token}");
                }

                client.Timeout = TimeSpan.FromMinutes(5);
                try
                {
                    var result = await client.GetAsync(Links.QrCodeValidation);
                    if ((int)result.StatusCode == 200)
                    {
                        var jsonString = result.Content.ReadAsStringAsync().Result;
                        QRResult = JsonConvert.DeserializeObject<QRResult>(jsonString);

                        if (QRResult.bookingStatus == 0) {
                            UserDialogs.Instance.HideLoading(); 
                            await DialogService.DisplayAlertAsync("",QRResult.message,"OK");
                        } else if (QRResult.bookingStatus == 2) {
                            UserDialogs.Instance.HideLoading(); 
                            await DialogService.DisplayAlertAsync("", QRResult.message,"OK");
                        } else if (QRResult.bookingStatus == 1) {
                            Settings.QrUserName = QRResult.QrUserName;
                            NavigationService.GoBackAsync();
                            await NavigationService.NavigateAsync("ScannerSuccess");
                            //BusinessDetail = JsonConvert.DeserializeObject<BusinessDetail>(jsonString);
                            UserDialogs.Instance.HideLoading();  
                        }
                    }   
                    else
                    {
                        UserDialogs.Instance.HideLoading();
                        //await NavigationService.NavigateAsync("ScannerFail");
                        var a = await DialogService.DisplayAlertAsync("", "error!, This Booking seems not belongs to your account", "OK","Cancel");
                        CustomScanPage.zxing.IsAnalyzing = true;
                        //await NavigationService.NavigateAsync(new Uri("http://www.testapp.com/Home", UriKind.Absolute));
                        //await NavigationService.NavigateAsync("CustomScanPage");
                    }
                }

                finally
                {
                    client.Dispose();
                }
            }
            catch (Exception e)
            {

                UserDialogs.Instance.HideLoading();
                await DialogService.DisplayAlertAsync("", e.Message, "OK");
            }
        }
    }
}
