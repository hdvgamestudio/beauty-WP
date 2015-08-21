using CSMen.BeautySocial.Constants;
using CSMen.BeautySocial.Model;
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
    public class BeautyApiRequest
    {
        private enum RequestBodyType : int
        {
            JsonBody = 0,
        }

        private class RequestBody
        {
            public RequestBodyType BodyType
            {
                set;
                get;
            }

            public string ContentType
            {
                get
                {
                    string contentType = string.Empty;
                    switch (BodyType)
                    {
                        case RequestBodyType.JsonBody:
                            contentType = "application/json";
                            break;
                    }

                    return contentType;
                }
            }

            public byte[] BodyData
            {
                set;
                get;
            }
        }

        private string m_Path;
        private Dictionary<string, string> m_Parameters;
        private HttpMethod m_Method;
        private RequestBody m_Body;
        public BeautyApiRequest()
        {
            this.m_Parameters = new Dictionary<string, string>();
        }

        public BeautyApiRequest(string path, HttpMethod method)
            : this()
        {
            this.m_Path = path;
            this.m_Method = method;
        }

        public BeautyApiRequest(string path)
            : this()
        {
            this.m_Path = path;
            this.m_Method = HttpMethod.Get;
        }

        public async Task<BeautyApiRespone> ExecuteAsync()
        {
            BeautyApiRespone beautyRespone = new BeautyApiRespone();

            try
            {
                HttpWebRequest httpWebRequest = HttpWebRequest.CreateHttp(RequestUrl);
                httpWebRequest.Method = m_Method.Method;
                if (m_Body != null)
                {
                    httpWebRequest.ContentType = m_Body.ContentType;
                    byte[] bodyData = m_Body.BodyData;

                    using (var requestStream = await httpWebRequest.GetRequestStreamAsync())
                    {
                        await requestStream.WriteAsync(bodyData, 0, bodyData.Length);
                    }
                }

                var httpRespone = await httpWebRequest.GetResponseAsync();
                using (var streamReader = new StreamReader(httpRespone.GetResponseStream()))
                {
                    string jsonString = await streamReader.ReadToEndAsync();
                    beautyRespone.Data = JsonConvert.DeserializeObject<JToken>(jsonString);
                }
            }
            catch (WebException webException)
            {
                var respone = webException.Response;
                using (var errorStreamReader = new StreamReader(respone.GetResponseStream()))
                {
                    var errorString = errorStreamReader.ReadToEnd();
                    if (string.IsNullOrEmpty(errorString))
                    {
                        throw;
                    }

                    BeautyApiException beautyException = JsonConvert.DeserializeObject<BeautyApiException>(errorString);
                    beautyException.WebException = webException;
                    throw beautyException;
                }
            } 
            catch (Exception otherException)
            {
                throw;
            }

            return beautyRespone;
        }

        public string Path
        {
            set
            {
                this.m_Path = value;
            }
            get
            {
                return m_Path;
            }
        }

        public void AddParameter(string key, string value)
        {
            m_Parameters.Add(key, value);
        }

        public bool RemoveParameter(string key)
        {
            if (!m_Parameters.ContainsKey(key))
                return false;

            m_Parameters.Remove(key);

            return true;
        }

        public void SetJsonBody(string jsonBody)
        {
            RequestBody body = new RequestBody
            {
                BodyType = RequestBodyType.JsonBody,
                BodyData = Encoding.UTF8.GetBytes(jsonBody)
            };

            this.m_Body = body;
        }

        public void SetJsonBody(JToken jsonToken)
        {
            SetJsonBody(jsonToken.ToString());
        }

        public void SetJsonBody(object jsonObject)
        {
            SetJsonBody(JsonConvert.SerializeObject(jsonObject));
        }

        public void ClearBody()
        {
            this.m_Body = null;
        }

        public string RequestUrl
        {
            get
            {
                StringBuilder urlBuilder = new StringBuilder();
                urlBuilder.AppendFormat(Configuration.cosmeticApiUrl,
                    Configuration.cosmeticApiVersion,
                    m_Path);

                if (m_Parameters.Count > 0)
                {
                    urlBuilder.Append('?');

                    bool isFirstParameter = true;
                    foreach (var item in m_Parameters)
                    { 
                        if (isFirstParameter)
                            isFirstParameter = false;
                        else
                            urlBuilder.Append('&');

                        urlBuilder.AppendFormat("{0}={1}", item.Key, item.Value);
                    }
                }

                return urlBuilder.ToString();
            }
        }
    }
}
