using TestApp.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Detail : ContentPage
    {
        public Detail()
        {
            InitializeComponent();
            if (!string.IsNullOrEmpty(Settings.FullName))
            {
                fullname.Text = Settings.FullName;
            }
            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}
