using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace CSMen.BeautySocial.Model
{
    public class BeautyApiException : Exception
    {
        public BeautyApiException()
        {
        }

        public BeautyApiException(WebException webException)
        {
            this.WebException = webException;
        }

        public WebException WebException
        {
            set;
            get;
        }

        [JsonProperty("message")]
        public string ErrorMessage
        {
            set;
            get;
        }
    }
}
