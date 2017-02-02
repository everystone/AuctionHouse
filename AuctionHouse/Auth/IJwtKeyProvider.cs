using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse.Auth
{
    public interface IJwtKeyProvider
    {
        string Audience { get; }
        string Issuer { get; }
        byte[] Key { get; }
    }
}
