using AuctionHouse.Models;
using Nancy;
using Nancy.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse.Modules
{

    
    public class UserModule : NancyModule
    {
        public UserModule()
        {
            Post["/users/login", true] = async (x, ct) =>
            {

                Credentials creds = this.Bind<Credentials>(); // Model binding https://github.com/NancyFx/Nancy/wiki/Model-binding
                Console.WriteLine(creds);
                await Task.Delay(100);
                return HttpStatusCode.OK;
            };

            Get["/test/{id}"] = parameter => {
                
                return "OK";
            };
        }
    }
}
