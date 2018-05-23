using System;
using System.Collections.Generic;
using System.Text;
using TestApp.Helpers;

namespace TestApp.Models
{
    public static class Links
    {
        public static string BaseUrl
        {
            //get => "https://dev.spotimist.com/spotimist/";
            get => "https://spotimist.spericorn.com/spotimist/";
        }

        public static string LoginUrl

        {
            get => $"{BaseUrl}login/";

        }

        public static string SocialLogin
        {
            get => $"{BaseUrl}social-login/";

        }


        public static string UserUrl
        {
            get => $"{BaseUrl}user/";

        }
        public static string BusinessDetail
        {
            //get => $"https://dev.spotimist.com/api/business-detail/{Settings.UserID}/";
            get => $"https://spotimist.spericorn.com/api/business-detail/{Settings.UserID}/";
        }
        public static string QrCodeValidation
        {
            //get => $"https://dev.spotimist.com/api/booking-detail/{Settings.QrCode}/{Settings.MyBusinessid}";
            get => $"https://spotimist.spericorn.com/api/booking-detail/{Settings.QrCode}/{Settings.MyBusinessid}";
        }
    }
}
