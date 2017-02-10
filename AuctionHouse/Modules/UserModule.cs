using AuctionHouse.Auth;
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
        public UserModule(IJwtKeyProvider keyProvider)
        {
            Post["/users/login", true] = async (x, ct) =>
            {

                Credentials creds = this.Bind<Credentials>(); // Model binding https://github.com/NancyFx/Nancy/wiki/Model-binding
                Console.WriteLine(creds);


                // Lookup user / pass from db

                if (creds.Username.Equals("admin") && creds.Password.Equals("admin"))
                {
                    var jwt = new JwtToken
                    {
                        Name = creds.Username,
                        Expiration = DateTime.UtcNow.AddDays(2),
                        Roles = new[] { "admin:false", "gender:man" },
                        Issuer = keyProvider.Issuer,
                        Audience = keyProvider.Audience
                    };

                    // Return Encrypted Token
                    return Jose.JWT.Encode(jwt, keyProvider.Key, Jose.JwsAlgorithm.HS256);

                }

                await Task.Delay(100);
                return HttpStatusCode.Unauthorized;
            };

            Get["/test/{id}"] = parameter => {
                
                return "OK";
            };
        }
    }
}
