using Nancy.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse.Models
{
    public class User : IUserIdentity
    {
        public User(JwtToken token)
        {
            UserName = token.Name;
            Claims = token.Roles;
        }
        public string UserName { get; }
        public IEnumerable<string> Claims { get; }
    }
}
