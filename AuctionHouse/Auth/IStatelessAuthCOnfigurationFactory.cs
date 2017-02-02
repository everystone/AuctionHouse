using Nancy.Authentication.Stateless;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse.Auth
{
    public interface IStatelessAuthConfigurationFactory
    {
        StatelessAuthenticationConfiguration Create();
    }
}
