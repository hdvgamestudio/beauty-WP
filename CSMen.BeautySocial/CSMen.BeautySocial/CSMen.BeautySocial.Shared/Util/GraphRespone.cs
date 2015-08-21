using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSMen.BeautySocial.Util
{
    public class GraphRespone
    {
        public FacebookRequestError Error
        {
            set;
            get;
        }

        public JToken MultipleData
        {
            set;
            get;
        }

        public JToken SingleData
        {
            set;
            get;
        }
    }
}
