using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSMen.BeautySocial.Util
{
    public class FacebookRequestError
    {
        [JsonProperty("message")]
        public string ErrorMessage
        {
            set;
            get;
        }

        [JsonProperty("code")]
        public int ErrorCode
        {
            set;
            get;
        }

        [JsonProperty("type")]
        public string ErrorType
        {
            set;
            get;
        }

        [JsonProperty("error_subcode")]
        public int ErrorSubCode
        {
            set;
            get;
        }
    }
}
