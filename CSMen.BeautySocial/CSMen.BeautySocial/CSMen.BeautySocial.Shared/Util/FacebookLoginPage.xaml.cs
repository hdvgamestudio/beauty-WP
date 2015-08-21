using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace CSMen.BeautySocial.Util
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FacebookLoginPage : Page
    {

#if WINDOWS_PHONE_APP
        public const string loginFacebookUrlTemplate = "https://m.facebook.com/dialog/oauth?client_id={0}&display=touch&response_type=token&redirect_uri={1}"; 
        public const string callbackUrl = "https://www.facebook.com/connect/login_success.html";

#elif WINDOWS_APP
        public const string loginFacebookUrlTemplate = "https://www.facebook.com/dialog/oauth?client_id={0}&display=popup&response_type=token&redirect_uri={1}";
        public const string callbackUrl = "https://www.facebook.com/connect/login_success.html";
#endif
        private FacebookAuthentication.LoginParameter m_LoginParameter;
        private bool m_IsCancelled = true;

        public FacebookLoginPage()
        {
            this.InitializeComponent();
        }

        private string CreateLoginUrl(string appId)
        {
            return string.Format(loginFacebookUrlTemplate, appId, callbackUrl);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            this.m_LoginParameter = e.Parameter as FacebookAuthentication.LoginParameter;

            var loginUrl = CreateLoginUrl(m_LoginParameter.AppId);

            webView.Navigate(new Uri(loginUrl, UriKind.RelativeOrAbsolute));
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            if (m_IsCancelled)
                m_LoginParameter.Tcs.SetCanceled();
        }

        private void OnNavigationFailed(object sender, WebViewNavigationFailedEventArgs e)
        {
            string absolutePath = e.Uri.ToString();
           
            m_LoginParameter.Tcs.SetException(new WebException());
            
            this.m_IsCancelled = false;

            System.Diagnostics.Debug.WriteLine("Navigation Failed: {0}", absolutePath);
        }

        private void OnNavigationStarting(WebView sender, WebViewNavigationStartingEventArgs args)
        {
            string absolutePath = args.Uri.ToString();
            if (absolutePath.StartsWith(callbackUrl))
            {
                string accessToken;
                if (args.Uri.TryQueryStringAfterSharp("access_token", out accessToken))
                {
                    m_LoginParameter.Tcs.SetResult(accessToken);
                }
                else
                {
                    m_LoginParameter.Tcs.SetException(new Exception());
                }

                m_IsCancelled = false;
                NavigationService.Current.GoBack();
            }
            System.Diagnostics.Debug.WriteLine("Navigation Starting: {0}", absolutePath);
        }

        private void OnNavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            string absolutePath = args.Uri.ToString();
            System.Diagnostics.Debug.WriteLine("Navigation Completed: {0}", absolutePath);
        }
    }
}
