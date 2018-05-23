using Acr.UserDialogs;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using TestApp.Helpers;
using TestApp.Models;

namespace TestApp.ViewModels
{
    public class DetailViewModel : ChildViewModelBase
    {
        #region Properties



        private BusinessDetail _businessDetail;


        public BusinessDetail BusinessDetail
        {
            get { return _businessDetail; }
            set { SetProperty(ref _businessDetail, value); }
        }

        private List<Photo> _photos;


        public List<Photo> Photos
        {
            get { return _photos; }
            set { SetProperty(ref _photos, value); }
        }


        #endregion
        public DetailViewModel(INavigationService navigationService, IPageDialogService dialogService, IEventAggregator ea) : base(ea, navigationService, dialogService)
        {


            //BindPhotos();
            IsActiveChanged += HandleIsActiveTrue;
            IsActiveChanged += HandleIsActiveFalse;
            if (IsActive)
            {
                InitAsync();
                //BindPhotos();
            }

           

        }
        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);
            if (HasInitialized) return;

            HasInitialized = true;
            if (IsActive)
            {
                InitAsync();

            }


        }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            if(Settings.MyOpenQrCodeAgain == 1){
                //NavigationService.NavigateAsync("CustomScanPage");
            }
        }




        private void HandleIsActiveTrue(object sender, EventArgs args)
        {
            if (IsActive == false) return;
            InitAsync();
            //BindPhotos();
            // Handle Logic Here
        }

        private void HandleIsActiveFalse(object sender, EventArgs args)
        {
            if (IsActive == true) return;

            // Handle Logic Here
        }

        public override void Destroy()
        {
            IsActiveChanged -= HandleIsActiveTrue;
            IsActiveChanged -= HandleIsActiveFalse;
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
                    var result = await client.GetAsync(Links.BusinessDetail);
                    if ((int)result.StatusCode == 200)
                    {
                        var jsonString = result.Content.ReadAsStringAsync().Result;
                        BusinessDetail = JsonConvert.DeserializeObject<BusinessDetail>(jsonString);
                        Console.WriteLine(BusinessDetail.ImageFullUrl);
                        Settings.MyImageUrl = BusinessDetail.ImageFullUrl;
                        Settings.MyBusinessid = BusinessDetail.id;
                        UserDialogs.Instance.HideLoading();
                    }

                    else
                    {
                        UserDialogs.Instance.HideLoading();
                        await DialogService.DisplayAlertAsync("", "Server error, Please contact support team", "OK");


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


        //private void BindPhotos()
        //{
        //    Photos = new List<Photo>()
        //    {
        //        new Photo{Id="1",ImageUrl="dummy.jpg" , Name="Johny Smith1"},
        //        new Photo{Id="2",ImageUrl="dummy.jpg", Name="Johny Smith2"},
        //        new Photo{Id="3",ImageUrl="dummy.jpg", Name="Johny Smith3"},
        //        new Photo{Id="4",ImageUrl="dummy.jpg", Name="Johny Smith4"},
        //        new Photo{Id="5",ImageUrl="dummy.jpg", Name="Johny Smith5"}
        //    };
        //}
    }
}
