using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace CSMen.BeautySocial.Util
{
    public class FacebookAuthentication
    {
        public class LoginParameter
        {
            public TaskCompletionSource<string> Tcs
            {
                set;
                get;
            }

            public string AppId
            {
                set;
                get;
            }
        }

        public Task<string> LoginAsync(string appId)
        {
            TaskCompletionSource<string> tcs = new TaskCompletionSource<string>();
            
            NavigationService.Current.Navigate(
                typeof(FacebookLoginPage),
                new LoginParameter
                {
                    Tcs = tcs,
                    AppId = appId
                });

            return tcs.Task;
        }
    }
}
