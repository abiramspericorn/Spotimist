using Prism.Navigation;
using TestApp.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home : Naxam.Controls.Forms.BottomTabbedPage, INavigatingAware
    {
        public Home()
        {
            InitializeComponent();

            //CurrentPage = Children[2];



        }
        public void OnNavigatingTo(NavigationParameters parameters)
        {
            foreach (var child in Children)
            {
                (child as INavigatingAware)?.OnNavigatingTo(parameters);
                (child?.BindingContext as INavigatingAware)?.OnNavigatingTo(parameters);
            }
        }
    }
}
