using System;
using System.Collections.Generic;
using System.Text;

namespace CSMen.BeautySocial.Util
{
    public static class UriHelper
    {
        public static bool TryQueryString(this Uri self, string key, out string stringOfQuery)
        {
            stringOfQuery = string.Empty;

            string url = self.ToString();
            string substring = url.Substring(((url.LastIndexOf('?') == -1) ? 0 : url.LastIndexOf('?') + 1));

            string[] pairs = substring.Split('&');

            Dictionary<string, string> output = new Dictionary<string, string>();

            foreach (string piece in pairs)
            {
                string[] pair = piece.Split('=');
                output.Add(pair[0], pair[1]);
            }

            if (output.ContainsKey(key))
            {
                stringOfQuery = output[key];
                return true;
            }

            return false;
        }

        public static bool TryQueryStringAfterSharp(this Uri self, string key, out string stringOfQuery)
        {
            stringOfQuery = string.Empty;

            string url = self.ToString();
            string substring = url.Substring(((url.LastIndexOf('#') == -1) ? 0 : url.LastIndexOf('#') + 1));

            string[] pairs = substring.Split('&');

            Dictionary<string, string> output = new Dictionary<string, string>();

            foreach (string piece in pairs)
            {
                string[] pair = piece.Split('=');
                output.Add(pair[0], pair[1]);
            }

            if (output.ContainsKey(key))
            {
                stringOfQuery = output[key];
                return true;
            }

            return false;
        }
    }
}
