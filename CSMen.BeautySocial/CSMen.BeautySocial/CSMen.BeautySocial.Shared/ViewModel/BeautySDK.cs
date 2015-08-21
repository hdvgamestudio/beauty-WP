using System;
using System.Collections.Generic;
using System.Text;

using CSMen.BeautySocial.Model;
using Newtonsoft.Json.Linq;
using CSMen.BeautySocial.Util;
using Windows.Web.Http;
using System.Threading.Tasks;

namespace CSMen.BeautySocial.ViewModel
{
    public class BeautySDK
    {
        public const string users = "users";

        private JObject CreateJsonRequest()
        {
            JObject requestObject = new JObject();
            requestObject["device_info"] = JObject.FromObject(DeviceInfo.Current);

            return requestObject;
        }

        public async Task<BeautyApiRespone> CreateUserAsync(BeautyUser user)
        {
            JObject requestObject = CreateJsonRequest();

            requestObject["user"] = JObject.FromObject(user,
                new Newtonsoft.Json.JsonSerializer { 
                    NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore
                });

            BeautyApiRequest request = new BeautyApiRequest(users, HttpMethod.Post);
            request.SetJsonBody(requestObject);

            var beautyRespone = await request.ExecuteAsync();

            return beautyRespone;
        }
    }
}
