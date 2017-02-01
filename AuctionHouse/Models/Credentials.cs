using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse.Models
{
    public class Credentials
    {
        public string Username;
        public string Password;

        public override string ToString()
        {
            return string.Format("User: {0}\nPass: {1}", Username, Password);
        }
    }
}
