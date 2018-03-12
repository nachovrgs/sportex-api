using System;
using System.Collections.Generic;
using System.Text;

namespace UserAPI.DBAccess
{
    public class AppConfig
    {
        public ConnectionStringsConfig ConnectionStrings { get; set; }
        public ApiSettingsConfig ApiSettings { get; set; }

        public class ConnectionStringsConfig
        {
            public string LocalDB { get; set; }
        }

        public class ApiSettingsConfig
        {
            public string Url { get; set; }
            public string ApiKey { get; set; }
            public bool UseCache { get; set; }
        }
    }
}
