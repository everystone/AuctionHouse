using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse.Models
{
    public class JwtToken
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public DateTime Expiration { get; set; }
        public string[] Roles { get; set; }
        public string Name { get; set; }
    }
}
