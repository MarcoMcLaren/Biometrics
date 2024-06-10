using Fingerprint.ViewModel;
using Plugin.Fingerprint;
using Plugin.Media;

namespace Fingerprint
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel(CrossFingerprint.Current);
        }

  

    }



}
