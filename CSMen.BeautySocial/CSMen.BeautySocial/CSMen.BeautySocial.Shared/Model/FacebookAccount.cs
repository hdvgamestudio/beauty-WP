using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSMen.BeautySocial.Model
{
    public class FacebookAccount
    {
        [JsonProperty("id")]
        public string FacebookId
        {
            set;
            get;
        }

        [JsonProperty("name")]
        public string FacebookName
        {
            set;
            get;
        }

        public string Email
        {
            set;
            get;
        }
    }
}
