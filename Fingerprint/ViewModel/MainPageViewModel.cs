using Plugin.Fingerprint.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Plugin.Fingerprint;
using System.Runtime.CompilerServices;

namespace Fingerprint.ViewModel
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private readonly IFingerprint _fingerprint;
        private ICommand _fingerprintCommand;

        public MainPageViewModel(IFingerprint fingerprint)
        {
            _fingerprint = fingerprint;
            _fingerprintCommand = new Command(async () => await FingerprintLoginAsync());
        }

        public ICommand FingerprintCommand
        {
            get { return _fingerprintCommand; }
        }

        private async Task FingerprintLoginAsync()
        {
            var isAvailable = await _fingerprint.IsAvailableAsync();

            if (isAvailable)
            {
                var request = new AuthenticationRequestConfiguration("Login using biometrics", "Confirm login with your biometrics")
                {
                    FallbackTitle = "Use PIN",
                    AllowAlternativeAuthentication = true
                };

                var result = await _fingerprint.AuthenticateAsync(request);

                if (result.Authenticated)
                {
                    // User authenticated, proceed with accessing secure data or actions
                }
                else
                {
                    // Authentication failed, handle according to your needs
                }
            }
            else
            {
                // Display an alert to the user that biometrics aren't available
                await App.Current.MainPage.DisplayAlert("Unavailable", "Biometrics not available on device, or permission not allowed.", "OK");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
