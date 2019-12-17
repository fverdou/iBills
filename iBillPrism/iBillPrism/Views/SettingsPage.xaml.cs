using iBillPrism.ViewModels;
using Xamarin.Forms;

namespace iBillPrism.Views
{
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();

            ((SettingsPageViewModel)BindingContext).Page = this;
        }
    }
}
