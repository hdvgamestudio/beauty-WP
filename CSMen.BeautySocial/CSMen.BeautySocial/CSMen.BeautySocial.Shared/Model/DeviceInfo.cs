using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSMen.BeautySocial.Model
{
    public class DeviceInfo
    {
        [JsonProperty("os_name")]
        public string OsName
        {
            set;
            get;
        }

        [JsonProperty("device_name")]
        public string DeviceName
        {
            set;
            get;
        }

        private static DeviceInfo m_Current;
        public static DeviceInfo Current
        {
            get
            {
                if (m_Current == null)
                {
                    m_Current = new DeviceInfo
                    {
                        OsName = "WindowsPhone",
                    };
                }

                return m_Current;
            }
        }
    }
}
