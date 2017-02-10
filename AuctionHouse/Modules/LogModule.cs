using Nancy;
using Nancy.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse.Modules
{
    class LogModule : NancyModule
    {
        public LogModule()
        {
            this.RequiresAuthentication();
            Get["/log/{count}"] = param =>
            {


                return "";
            };
        }
    }
}
