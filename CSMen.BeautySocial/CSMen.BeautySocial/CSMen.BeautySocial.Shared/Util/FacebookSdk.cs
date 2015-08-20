using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Http;

namespace CSMen.BeautySocial.Util
{
    public class FacebookSdk
    {
        public const string graphApiUrl = "https://graph.facebook.com";

        private string m_AccessToken;
        public FacebookSdk(string accessToken)
        {
            if (string.IsNullOrEmpty(accessToken))
                throw new ArgumentNullException();

            this.m_AccessToken = accessToken;
        }

        public async Task<GraphRespone> RequestAsync(string graphPath, string parameter, HttpMethod httpMethod)
        {
            TaskCompletionSource<GraphRespone> tcs = new TaskCompletionSource<GraphRespone>();

            GraphRespone graphRespone = new GraphRespone();
            
            string requestUrl = GetGraphRequestUrl(graphPath, parameter);

            HttpWebRequest httpWebRequest = HttpWebRequest.CreateHttp(requestUrl);
            httpWebRequest.Method = httpMethod.Method;

            try
            {
                var httpRespone = await httpWebRequest.GetResponseAsync();
                using (var streamReader = new StreamReader(httpRespone.GetResponseStream()))
                {
                    var responeString = await streamReader.ReadToEndAsync();
                    graphRespone.SingleData = JsonConvert.DeserializeObject<JToken>(responeString);
                    graphRespone.MultipleData = graphRespone.SingleData["data"];
                }
            }
            catch (WebException webException)
            {
                using (var errorStreamReader = new StreamReader(webException.Response.GetResponseStream()))
                {
                    var errorResponeString = errorStreamReader.ReadToEnd();
                    var errorResponeObject = JsonConvert.DeserializeObject<JObject>(errorResponeString);
                    graphRespone.Error = errorResponeObject["error"].ToObject<FacebookRequestError>();
                }
            }

            return graphRespone;
        }

        private string GetGraphRequestUrl(string graphPath, string parameter)
        {
            StringBuilder sb = new StringBuilder();
            
            sb.Append(graphApiUrl);
            sb.Append(string.Format("/{0}", graphPath));
            sb.Append('?');
            sb.Append(string.Format("access_token={0}", m_AccessToken));

            return sb.ToString();
        }
    }
}
