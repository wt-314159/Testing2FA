using AuthenticatorWPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.IO;
using System.Windows;

namespace Testing2FA
{
    public class MainWindowViewModel : ObservableModelBase
    {
        private string secretFileName = @"C:\Users\User\Notes\secretFile";
        private string _username;
        private string _secret;
        private bool _loggedIn;
        private Visibility _loggedInVisibility;
        private Visibility _loginFailedVisibility;

        public ICommand SecretChangedCommand { get; set; }
        public ICommand LoginCommand { get; set; }
        public ICommand Check2FACommand { get; set; }

        public string Username
        {
            get => _username;
            set => HandlePropertyChanged(ref _username, value);
        }

        public bool LoggedIn
        {
            get => _loggedIn;
            set => HandlePropertyChanged(ref _loggedIn, value);
        }

        public Visibility LoggedInVisibility
        {
            get => _loggedInVisibility;
            set => HandlePropertyChanged(ref _loggedInVisibility, value);
        }

        public Visibility LoginFailedVisibility
        {
            get => _loginFailedVisibility;
            set => HandlePropertyChanged(ref _loginFailedVisibility, value);
        }

        public MainWindowViewModel()
        {
            LoggedInVisibility = Visibility.Collapsed;
            LoginFailedVisibility = Visibility.Collapsed;
            SecretChangedCommand = new RelayCommand<string>(OnSecretChanged);
            LoginCommand = new RelayCommand<string>(OnLogin);
            Check2FACommand = new RelayCommand<string>(OnCheck2FA);
        }

        private void OnSecretChanged(string secret)
        {
            // HACK for now just store the secret in a file, 
            // NEVER DO THIS FOR A SECURE STRING!!!
            File.WriteAllText(secretFileName, secret);
            _secret = secret;
        }

        private void OnLogin(string password)
        {
            // TODO some kind of login check here:
        }

        private void OnCheck2FA(string code)
        {
            LoggedIn = TotpAuth.Verify(_secret, code);
            SetLoginVisibilities(LoggedIn);
        }

        private void SetLoginVisibilities(bool success)
        {
            if (success)
            {
                LoggedInVisibility = Visibility.Visible;
                LoginFailedVisibility = Visibility.Collapsed;
            }
            else
            {
                LoginFailedVisibility = Visibility.Visible;
                LoggedInVisibility = Visibility.Collapsed;
            }
        }
    }
}
