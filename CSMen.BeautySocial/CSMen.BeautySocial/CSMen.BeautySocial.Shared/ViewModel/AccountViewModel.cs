using CSMen.BeautySocial.Constants;
using CSMen.BeautySocial.Model;
using CSMen.BeautySocial.Util;
using System;
using System.Collections.Generic;
using System.Text;
using Windows.Web.Http;

namespace CSMen.BeautySocial.ViewModel
{
    public class AccountViewModel
    {
        public async void LoginAsFacebook()
        {
            FacebookAuthentication facebookAuthentication = new FacebookAuthentication();
            var accessToken = await facebookAuthentication.LoginAsync(Configuration.facebookAppId);

            FacebookSdk facebookSdk = new FacebookSdk(accessToken);
            var respone = await facebookSdk.RequestAsync("me", "field=id,name", HttpMethod.Get);
            if (respone.Error != null)
                throw new Exception();

            FacebookAccount facebookAccount = respone.SingleData.ToObject<FacebookAccount>();

            string json = "{\"user\": {\"name\": \"ngocnv\",\"email\": \"nguyenngoc101@yahoo.com\",\"account_type\": \"facebook\",\"uid\": \"xxxxxxxxx\",\"access_token\": \"xxxxxxxxxxxxxx\",\"oss_attributes\": [{\"device_token\": \"xxxxxxx\", \"type\": \"Ios\"}]}}";
            BeautyApiRequest requestFacebookLogin = new BeautyApiRequest("users", HttpMethod.Post);
            requestFacebookLogin.SetStringBody(json);

            var respone1 = await requestFacebookLogin.ExecuteAsync();
        }
    }
}
