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
    public class MaterialModule : NancyModule
    {
        public MaterialModule()
        {

            Post["/material/save", true] = async (x, ct) =>
            {

                Material m = this.Bind<Material>(); // Model binding https://github.com/NancyFx/Nancy/wiki/Model-binding
                Console.WriteLine(m);


                // Check if material exist in db, update etc.

                await Task.Delay(100);
                return HttpStatusCode.OK;
            };
        }
    }
}
