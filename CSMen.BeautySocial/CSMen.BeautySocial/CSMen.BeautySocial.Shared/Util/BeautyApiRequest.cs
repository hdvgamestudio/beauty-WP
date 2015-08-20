using CSMen.BeautySocial.Constants;
using CSMen.BeautySocial.Model;
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
        private string m_Path;
        private Dictionary<string, string> m_Parameters;
        private HttpMethod m_Method;
        private byte[] m_Body;
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
            HttpWebRequest httpWebRequest = HttpWebRequest.CreateHttp(RequestUrl);
            httpWebRequest.Method = m_Method.Method;
            if (m_Body != null && m_Body.Length > 0)
            {
                httpWebRequest.ContentType = "application/json";
                using (var requestStream = await httpWebRequest.GetRequestStreamAsync())
                {
                    await requestStream.WriteAsync(m_Body, 0, m_Body.Length);
                }
            }

            var httpRespone = await httpWebRequest.GetResponseAsync();
            using (var streamReader = new StreamReader(httpRespone.GetResponseStream()))
            {
                string jsonString = await streamReader.ReadToEndAsync();
            }
            return null;
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

        public void SetStringBody(string body)
        {
            this.m_Body = UTF8Encoding.UTF8.GetBytes(body);
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
