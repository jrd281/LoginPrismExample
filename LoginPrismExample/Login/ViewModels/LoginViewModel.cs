using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using LoginPrismExample.Login.Models;
using Prism.Commands;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;

namespace LoginPrismExample.Login.ViewModels
{
    [Export(typeof(LoginViewModel))]
    public class LoginViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly DelegateCommand<object> _loginCommand;
        private readonly LoginModel _loginModel;

        private bool _canLogin;
        private string _loginUsername;
        private string _loginPassword;
        private string _resultMessage;

        [ImportingConstructor]
        public LoginViewModel(IEventAggregator eventAggregator, LoginModel loginModel)
        {
            _eventAggregator = eventAggregator;
            _loginModel = loginModel;
            _loginModel.LoginFailureEvent += LoginModelOnLoginFailureEvent;
            _loginCommand = new DelegateCommand<object>(AttemptLogin);
            NotificationRequest = new InteractionRequest<INotification>();
            RaiseNotificationCommand = new DelegateCommand(RaiseNotification);
        }

        public ICommand RaiseNotificationCommand { get; private set; }

        public string LoginUsername
        {
            get { return _loginUsername; }
            set
            {
                SetProperty(ref _loginUsername, value);
                ValidateLogin();
            }
        }

        public string LoginPassword
        {
            get { return _loginPassword; }
            set
            {
                SetProperty(ref _loginPassword, value);
                ValidateLogin();
            }
        }

        private void ValidateLogin()
        {
            CanLogin = !(string.IsNullOrEmpty(_loginUsername) || string.IsNullOrEmpty(_loginPassword));
        }

        public bool CanLogin
        {
            get { return _canLogin; }
            set { SetProperty(ref _canLogin, value); }
        }

        public InteractionRequest<INotification> NotificationRequest { get; }

        public string InteractionResultMessage
        {
            get { return _resultMessage; }
            set
            {
                _resultMessage = value;
                OnPropertyChanged("InteractionResultMessage");
            }
        }

        public ICommand LoginCommand => _loginCommand;

        private void LoginModelOnLoginFailureEvent(object sender, EventArgs eventArgs)
        {
            NotificationRequest.Raise(
                new Notification { Content = "Incorrect Password", Title = "Incorrect Password" },
                n => { InteractionResultMessage = "Incorrect Password."; });
        }

        private void RaiseNotification()
        {
            LoginPassword = "";
            // By invoking the Raise method we are raising the Raised event and triggering any InteractionRequestTrigger that
            // is subscribed to it.
            // As parameters we are passing a Notification, which is a default implementation of INotification provided by Prism
            // and a callback that is executed when the interaction finishes.
            NotificationRequest.Raise(
                new Notification { Content = "Notification Message", Title = "Notification" },
                n => { InteractionResultMessage = "The user was notified."; });
        }

        private bool CanAttemptLogin()
        {
            return LoginPassword != null && LoginPassword.Trim().Length > 0;
        }

        private void AttemptLogin(object obj)
        {
            _loginModel.AttempLogin(LoginPassword);
        }
    }
}
