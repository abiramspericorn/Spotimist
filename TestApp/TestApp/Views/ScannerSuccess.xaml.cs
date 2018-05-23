using TestApp.Helpers;
using TestApp.Models;
using TestApp.ViewModels;
using Xamarin.Forms;

namespace TestApp.Views
{
    public partial class ScannerSuccess : ContentPage
    {
        public ScannerSuccess()
        {
            InitializeComponent();
            if (!string.IsNullOrEmpty(Settings.QrUserName))
            {
                fullname.Text = "Name : "+ ScannerViewModel.QrCodeResult.QrUserName;//Settings.QrUserName;
                email.Text = "Email :" + ScannerViewModel.QrCodeResult.email;
                date.Text = "Date : " + ScannerViewModel.QrCodeResult.date;
                time.Text = "Time : " + ScannerViewModel.QrCodeResult.time;
                bookingname.Text = "Booking Items : " + ScannerViewModel.QrCodeResult.booking_items[0].name;
            }
            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}
