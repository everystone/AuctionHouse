using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse.Auth
{
    public class JwtKeyProvider : IJwtKeyProvider
    {
        //public string Audience => ConfigurationSettings.AppSettings["audience"];
        //public string Issuer => ConfigurationSettings.AppSettings["issuer"];
        //public byte[] Key => Convert.FromBase64String(ConfigurationSettings.AppSettings["key"]

        public string Audience => "Hello";
        public string Issuer => "Detec";
        public byte[] Key => Convert.FromBase64String("c3VwZXJzZWNyZXQ=");

    }
}
