using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSMen.BeautySocial.Model
{
    public class BeautyUser
    {
        [JsonProperty("uid")]
        public string Id
        {
            set;
            get;
        }

        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("account_type")]
        public UserType? UserType
        {
            set;
            get;
        }

        [JsonProperty("name")]
        public string Name
        {
            set;
            get;
        }

        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("gender")]
        public Gender? Gender
        {
            set;
            get;
        }

        [JsonProperty("birthday")]
        public DateTime? Birthday
        {
            set;
            get;
        }

        [JsonProperty("email")]
        public string Email
        {
            set;
            get;
        }

        [JsonProperty("phone")]
        public string PhoneNumber
        {
            set;
            get;
        }

        [JsonProperty("access_token")]
        public string FacebookAccessToken
        {
            set;
            get;
        }
    }
}
