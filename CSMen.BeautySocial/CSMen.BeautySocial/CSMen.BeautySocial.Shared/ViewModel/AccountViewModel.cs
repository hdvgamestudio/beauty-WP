using CSMen.BeautySocial.Constants;
using CSMen.BeautySocial.Model;
using CSMen.BeautySocial.Util;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Http;

namespace CSMen.BeautySocial.ViewModel
{
    public class AccountViewModel
    {
        public async Task LoginAsFacebook()
        {
            FacebookAuthentication facebookAuthentication = new FacebookAuthentication();
            var accessToken = await facebookAuthentication.LoginAsync(Configuration.facebookAppId);

            FacebookSdk facebookSdk = new FacebookSdk(accessToken);
            var respone = await facebookSdk.RequestAsync("me", "field=id,name", HttpMethod.Get);
            if (respone.Error != null)
                throw new Exception();

            FacebookAccount facebookAccount = respone.SingleData.ToObject<FacebookAccount>();


            BeautyUser user = new BeautyUser
            {
                UserType = UserType.facebook,
                Id = facebookAccount.FacebookId,
                FacebookAccessToken = accessToken,
                Name = facebookAccount.FacebookName,
                Email = facebookAccount.Email
            };

            BeautySDK beautySdk = new BeautySDK();
            await beautySdk.CreateUserAsync(user);
        }
    }
}
