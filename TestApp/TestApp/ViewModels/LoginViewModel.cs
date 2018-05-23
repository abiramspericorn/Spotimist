using Acr.UserDialogs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using TestApp.Helpers;
using TestApp.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using Prism.Events;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using Xamarin.Forms;
using TestApp.Services;
namespace TestApp.ViewModels
{

    public class LoginViewModel : ViewModelBase
    {
        public Service service;
        public DelegateCommand HomeNavigation { get; set; }
        public DelegateCommand FacebookLoginPressed { get; set; }
        public DelegateCommand GoogleLoginPressed { get; set; }


        private FacebookUser _facebookUser;

        public FacebookUser FacebookUser
        {
            get { return _facebookUser; }
            set { SetProperty(ref _facebookUser, value); }
        }
        private GoogleUser _googleUser;

        public GoogleUser GoogleUser
        {
            get { return _googleUser; }
            set { SetProperty(ref _googleUser, value); }
        }

        private string _userName;

        public string Username
        {
            get { return _userName; }
            set { SetProperty(ref _userName, value); }
        }

        private string _password;

        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }

        private UserModel _user;

        public UserModel User
        {
            get { return _user; }
            set { SetProperty(ref _user, value); }
        }


        private List<Photo> _photos;

        public List<Photo> Photos
        {
            get { return _photos; }
            set { SetProperty(ref _photos, value); }
        }

        private string socialType;





        public LoginViewModel(INavigationService navigationService, IPageDialogService dialogService, IFacebookManager facebookManager, IGoogleManager googleManager) : base(navigationService, dialogService, facebookManager, googleManager)
        {
            HomeNavigation = new DelegateCommand(homenavigationAsync);
            FacebookLoginPressed = new DelegateCommand(FacebookLogin);
            GoogleLoginPressed = new DelegateCommand(GoogleLogin);
        }


        private async void homenavigationAsync()
        {

            try
            {

                if (string.IsNullOrEmpty(Username))
                {
                    await DialogService.DisplayAlertAsync("", "Enter email id", "OK");
                }
                else if (!Regex.IsMatch(Username, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase))
                {
                    await DialogService.DisplayAlertAsync("", "Enter valid email id", "OK");
                }

                if (!string.IsNullOrEmpty(Username) && string.IsNullOrEmpty(Password))
                {
                    if (Regex.Match(Username, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").Success)
                    {
                        await DialogService.DisplayAlertAsync("", "Enter valid Password", "OK");
                    }
                }
                if (!string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password))
                {
                    UserDialogs.Instance.ShowLoading(null, MaskType.None);

                    //Username = "madhav@spotimist.com";
                    //Password = "FCx4cC4YfdM2mUNE";

                    service = new Service();

                    var response = await service.CustomPostRequest<TokenModel>(Links.LoginUrl, service.GetContent(new { email = Username, password = Password }));

                    if (response.Status == ResponseStatus.Success || string.IsNullOrEmpty(response.ExceptionMessage))
                    {
                        var result = response.Data;
                        Console.WriteLine(result.key);
                        Settings.Token = result.key;
                        hasBusiness();
                        //var m = JsonConvert.DeserializeObject<string>(result);
                        //UserDialogs.Instance.HideLoading();
                        //await NavigationService.NavigateAsync(new Uri("http://www.testapp.com/Home", UriKind.Absolute));
                    }
                    else
                    {

                        UserDialogs.Instance.HideLoading();
                        await DialogService.DisplayAlertAsync("", response.Message, "OK");
                    }
                }
            }
            catch (Exception)
            {

                UserDialogs.Instance.HideLoading();
                await DialogService.DisplayAlertAsync("", "Email or password are wrong", "OK");
            }

        }

        private async void hasBusiness()
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
                    var result = await client.GetAsync(Links.UserUrl);
                    if ((int)result.StatusCode == 200)
                    {
                        var jsonString = result.Content.ReadAsStringAsync().Result;
                        Console.WriteLine(jsonString);
                        User = JsonConvert.DeserializeObject<UserModel>(jsonString);
                        Settings.FullName = User.full_name;

                        UserDialogs.Instance.HideLoading();
                        if (User.has_business)
                        {
                            Settings.UserID = User.business_id;

                            Application.Current.Properties["UserLogin"] = true;
                            await NavigationService.NavigateAsync(new Uri("http://www.testapp.com/Home", UriKind.Absolute));

                        }
                        else
                        {

                            await DialogService.DisplayAlertAsync("", "You don't have business with us, please register here - > https://dev.spotimist.com/spotimist/get-listed/", "OK");
                        }
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




                //service = new Service();

                //// var response = await service.CustomPostRequest<TokenModel>(Links.UserUrl, service.GetContent(new { email = "madhav@spotimist.com", password = "FCx4cC4YfdM2mUNE" }));
                //var response = await service.GetJsonRequest<UserModel>(Links.UserUrl);
                //if (response.Status == ResponseStatus.Success || string.IsNullOrEmpty(response.ExceptionMessage))
                //{
                //    var result = response.Data;
                //    result = _user;
                //    //User = result;
                //    UserDialogs.Instance.HideLoading();
                //   // await NavigationService.NavigateAsync(new Uri("http://www.testapp.com/Home", UriKind.Absolute));
                //}

                //else
                //{

                //    UserDialogs.Instance.HideLoading();
                //    await DialogService.DisplayAlertAsync("", response.Message, "OK");
                //}
            }
            catch (Exception e)
            {

                UserDialogs.Instance.HideLoading();
                await DialogService.DisplayAlertAsync("", e.Message, "OK");
            }
        }



        private void FacebookLogin()
        {
            _facebookManager.Login(OnLoginComplete);
            socialType = "facebook";
        }

        private void OnLoginComplete(FacebookUser facebookUser, string message)
        {
            if (facebookUser != null)
            {
                FacebookUser = facebookUser;
                socialFacebookApiCall();
                _facebookManager.Logout();

                //NavigationService.NavigateAsync(new Uri("http://www.testapp.com/Home", UriKind.Absolute));
                //IsLogedIn = true;
            }
            else
            {
                DialogService.DisplayAlertAsync("Error", message, "Ok");
            }
        }



        private void GoogleLogin()
        {
            _googleManager.Login(OnLoginComplete);
            socialType = "google";
        }

        private void OnLoginComplete(GoogleUser googleUser, string message)
        {
            if (googleUser != null)
            {
                GoogleUser = googleUser;
                socialGoogleApiCall();
                _googleManager.Logout();
            }
            else
            {
                DialogService.DisplayAlertAsync("Error", message, "Ok");
            }
        }


       

        private async void socialFacebookApiCall()
        {
            {
                try
                {
                    UserDialogs.Instance.ShowLoading(null, MaskType.None);
                    HttpClient client = new HttpClient();
                    client.Timeout = TimeSpan.FromMinutes(5);
                   
                    var formContent = new FormUrlEncodedContent(new[]
           {
                        new KeyValuePair<string, string>("email", FacebookUser.Email),
                        new KeyValuePair<string, string>("socialmedia_id", FacebookUser.Id),
                        new KeyValuePair<string, string>("social_type", socialType),
                        new KeyValuePair<string, string>("first_name", FacebookUser.FirstName),
                        new KeyValuePair<string, string>("last_name", FacebookUser.LastName),

            });
                    try
                    {
                      
                        var result = await client.PostAsync(Links.SocialLogin, formContent);
                        if ((int)result.StatusCode == 200)
                        {
                            var jsonString = result.Content.ReadAsStringAsync().Result;
                            User = JsonConvert.DeserializeObject<UserModel>(jsonString);
                            Settings.FullName = User.full_name;
                            _facebookManager.Logout();
                            if (User.email == "")
                            {
                                UserDialogs.Instance.HideLoading();
                                await DialogService.DisplayAlertAsync("", "You don't have registered email with us, please register here - > https://dev.spotimist.com/spotimist/get-listed/", "OK");
                            }
                            else
                            {
                                UserDialogs.Instance.HideLoading();
                                if (User.has_business)
                                {
                                    Settings.UserID = User.business_id;
                                    Application.Current.Properties["UserLogin"] = true;
                                    await NavigationService.NavigateAsync(new Uri("http://www.testapp.com/Home", UriKind.Absolute));

                                }
                                else
                                {
                                    UserDialogs.Instance.HideLoading();
                                    await DialogService.DisplayAlertAsync("", "You don't have business with us, please register here - > https://dev.spotimist.com/spotimist/get-listed/", "OK");
                                }
                            }

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

        }


        private async void socialGoogleApiCall()
        {
            {
                try
                {
                    UserDialogs.Instance.ShowLoading(null, MaskType.None);
                    HttpClient client = new HttpClient();
                    client.Timeout = TimeSpan.FromMinutes(5);
                 
                    var formContent = new FormUrlEncodedContent(new[]
           {
                        new KeyValuePair<string, string>("email", GoogleUser.Email),
                        new KeyValuePair<string, string>("socialmedia_id", GoogleUser.id),
                        new KeyValuePair<string, string>("social_type", socialType),
                        new KeyValuePair<string, string>("first_name", GoogleUser.first_name),
                        new KeyValuePair<string, string>("last_name", GoogleUser.last_name),

            });
                    try
                    {
                      
                        var result = await client.PostAsync(Links.SocialLogin, formContent);
                        if ((int)result.StatusCode == 200)
                        {
                            var jsonString = result.Content.ReadAsStringAsync().Result;
                            User = JsonConvert.DeserializeObject<UserModel>(jsonString);
                            Settings.FullName = User.full_name;
                            _googleManager.Logout();
                            UserDialogs.Instance.HideLoading();
                            if (User.has_business)
                            {
                                Settings.UserID = User.business_id;
                                Application.Current.Properties["UserLogin"] = true;
                                await NavigationService.NavigateAsync(new Uri("http://www.testapp.com/Home", UriKind.Absolute));
                            }
                            else
                            {

                                await DialogService.DisplayAlertAsync("", "You don't have business with us, please register here - > https://dev.spotimist.com/spotimist/get-listed/", "OK");
                            }
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
                catch (Exception)
                {

                    UserDialogs.Instance.HideLoading();
                    await DialogService.DisplayAlertAsync("", "Network issue please try once again", "OK");
                }
            }

        }

    }

        //void HandleTextChanged(object sender, TextChangedEventArgs e)
        //{
        //  const string pwRegex = @"^(?=.{8,})(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&+=]).*$";
        //  const string emailRegex = @"^ ([\w\.\-] +)@([\w\-] +)((\.(\w){ 2,3})+)$";
        //  bool IsPasswordValid = false;
        //  bool IsEmailValid = false;
        //  IsPasswordValid = (Regex.IsMatch(e.NewTextValue, pwRegex, RegexOptions.IgnoreCase));
        //  ((Entry)sender).TextColor = IsPasswordValid ? Color.Default : Color.Red;

        //  IsEmailValid = (Regex.IsMatch(e.NewTextValue, emailRegex, RegexOptions.IgnoreCase));
        //  ((Entry)sender).TextColor = IsEmailValid ? Color.Default : Color.Red;


        //  // LABEL errorEmailMessage
        //  Label errorEmailMessage = ((Entry)sender).FindByName<Label>("errorEmailMessage");
        //  if (IsPasswordValid)
        //  {
        //      errorEmailMessage.Text = "Email Invalid!";
        //  }
        //  else
        //  {
        //      errorEmailMessage.Text = "";
        //  }

        //  // LABEL errorPasswordMessage
        //  Label errorPasswordMessage = ((Entry)sender).FindByName<Label>("errorPasswordMessage");
        //  if (IsEmailValid)
        //  {
        //      errorPasswordMessage.Text = "Passowrd Invalid";
        //  }
        //  else
        //  {
        //      errorPasswordMessage.Text = "";
        //  }
        //}


  

    public class TokenModel
    {
        public string key { get; set; }
    }




}
