using Nancy;
using Nancy.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse.Auth
{
    public interface IUserMapper    {        /// <summary>        /// Get the real username from an identifier        /// </summary>        /// <param name="identifier">User identifier</param>        /// <param name="context">The current NancyFx context</param>        /// <returns>Matching populated IUserIdentity object, or empty</returns>        IUserIdentity GetUserFromIdentifier(Guid identifier, NancyContext context);    }
}
