using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace TestApp.Helpers
{/// <summary>
 /// This is the Settings static class that can be used in your Core solution or in any
 /// of your client applications. All settings are laid out the same exact way with getters
 /// and setters. 
 /// </summary>
    public static class Settings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        #region Setting Constants

        private const string SettingsKey = "settings_key";
        private static readonly string SettingsDefault = string.Empty;




        public static string GeneralSettings
        {
            get
            {
                return AppSettings.GetValueOrDefault(SettingsKey, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(SettingsKey, value);
            }
        }
        #endregion

        #region Token

        private const string TokenKey = "Token_key";
        private static readonly string TokenDefault = string.Empty;




        public static string Token
        {
            get
            {
                return AppSettings.GetValueOrDefault(TokenKey, TokenDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(TokenKey, value);
            }
        }
        #endregion

        private const string FullNameKey = "FullName";
        private static readonly string FullNameDefault = string.Empty;

        public static string FullName
        {
            get
            {
                return AppSettings.GetValueOrDefault(FullNameKey, FullNameDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(FullNameKey, value);
            }
        }


        private const string UserIdkey = "userId";
        private static readonly int UserIdkeyDefault = 0;

        public static int UserID
        {
            get
            {
                return AppSettings.GetValueOrDefault(UserIdkey, UserIdkeyDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(UserIdkey, value);
            }
        }

        private const string QrCodeKey = "QrCode";
        private static readonly string QrCodeDefault = string.Empty;

        public static string QrCode
        {
            get
            {
                return AppSettings.GetValueOrDefault(QrCodeKey, QrCodeDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(QrCodeKey, value);
            }
        }

        private const string QRUserName = "QrUserName";
        private static readonly string QRUserNameDefault = string.Empty;

        public static string QrUserName
        {
            get
            {
                return AppSettings.GetValueOrDefault(QRUserName, QRUserNameDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(QRUserName, value);
            }
        }

        private const string FullImageUrl = "DetailImageUrl";
        private static readonly string FullImageUrlDefault = string.Empty;

        public static string MyImageUrl
        {
            get
            {
                return AppSettings.GetValueOrDefault(FullImageUrl, FullImageUrlDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(FullImageUrl, value);
            }
        }


        private const string businessid = "businessid";
        private static readonly int businessidDefault = 0;

        public static int MyBusinessid
        {
            get
            {
                return AppSettings.GetValueOrDefault(businessid, businessidDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(businessid, value);
            }
        }


        private const string OpenQrCodeAgain = "QrCodeAgain";
        private static readonly int OpenQrCodeAgainDefault = 0;

        public static int MyOpenQrCodeAgain
        {
            get
            {
                return AppSettings.GetValueOrDefault(OpenQrCodeAgain, OpenQrCodeAgainDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(OpenQrCodeAgain, value);
            }
        }

    }
}
