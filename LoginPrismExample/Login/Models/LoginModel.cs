using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Prism.Events;

namespace LoginPrismExample.Login.Models
{
    [Export(typeof(LoginModel))]
    public class LoginModel
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly bool _isFirstStartup = true;

        [ImportingConstructor]
        public LoginModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public event EventHandler LoginFailureEvent;

        public void AttempLogin(string loginPassword)
        {
            var loginAttempFailed = true;


            if (loginAttempFailed)
            {
                var handler = LoginFailureEvent;
                if (handler != null)
                {
                    var args = new DataEventArgs<bool>(false);

                    handler(this, args);
                }
            }
        }

        public string ComputeHash(string plainText, string salt)
        {
            var utf8 = new UTF8Encoding();
            var textWithSaltBytes = utf8.GetBytes(string.Concat(plainText, salt));
            HashAlgorithm hasher = new SHA1CryptoServiceProvider();
            var hashedBytes = hasher.ComputeHash(textWithSaltBytes);
            hasher.Clear();
            return Convert.ToBase64String(hashedBytes);
        }
    }
}