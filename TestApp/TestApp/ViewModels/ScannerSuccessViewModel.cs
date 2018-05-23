using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using TestApp.Helpers;
using Acr.UserDialogs;
using System.Net.Http;

using System.Net.Http.Headers;
using TestApp.Models;
using Newtonsoft.Json;

namespace TestApp.ViewModels
{

    public class ScannerSuccessViewModel : ViewModelBase
	{
        #region Properties

        public static QRResult QrCodeResult;

        public QRResult QRResult
        {
            get { return QrCodeResult; }
            set { SetProperty(ref QrCodeResult, value); }
        }
        #endregion

        #region Properties

        public DelegateCommand ScanAgain { get; set; }
        public DelegateCommand Close { get; set; }

        #endregion
        public ScannerSuccessViewModel(INavigationService navigationService, IPageDialogService dialogService) : base(navigationService, dialogService)
        {
            //InitAsync();
            Close = new DelegateCommand(closenavigation);
            ScanAgain = new DelegateCommand(scanagainnaviagtion);
        }
        private void scanagainnaviagtion()
        {
            NavigationService.GoBackAsync();
        }

        private void closenavigation()
        {
            Settings.MyOpenQrCodeAgain = 1;
            NavigationService.NavigateAsync(new Uri("http://www.testapp.com/Home", UriKind.Absolute));
            //NavigationService.NavigateAsync(new Uri("http://www.testapp.com/Scanner", UriKind.Absolute));
            //NavigationService.NavigateAsync("CustomScanPage");
            //NavigationService.NavigateAsync("CustomScanPage");
            //Navigation.PushModalAsync(new LoginPage());

            //NavigationService.GoBackAsync();
            //NavigationService.GoBackToRootAsync();


        }

        //public override void OnNavigatedFrom(NavigationParameters parameters)
        //{
        //    NavigationService.NavigateAsync(new Uri("http://www.testapp.com/Home", UriKind.Absolute));
        //}

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

                        Settings.QrUserName = QRResult.QrUserName;
                        //await NavigationService.NavigateAsync("ScannerSuccess");
                        //BusinessDetail = JsonConvert.DeserializeObject<BusinessDetail>(jsonString);
                        UserDialogs.Instance.HideLoading();
                    }

                    else
                    {

                        UserDialogs.Instance.HideLoading();
                        //await NavigationService.NavigateAsync("ScannerFail");
                        await DialogService.DisplayAlertAsync("", "error!, This is not a valid booking, please try again", "OK");
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
