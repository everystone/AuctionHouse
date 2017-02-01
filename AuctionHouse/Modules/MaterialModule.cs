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

                if (Program.Trader.Materials.Contains(m))
                {
                    Program.Trader.Materials[Program.Trader.Materials.IndexOf(m)] = m;
                    Console.WriteLine("Updated material: " + m);
                    return "OK";
                }

                Program.Trader.Materials.Add(m);

                // Check if material exist in db, update etc.
                Console.WriteLine("Added new material: " + m);
                await Task.Delay(100);
                return HttpStatusCode.OK;
            };


            Get["/material/list/{col}"] = param =>
            {
                var col = this.Bind<string>();
                //await Task.Delay(100);
                switch (col)
                {
                    case "Price":
                        return Response.AsJson(Program.Trader.Materials.OrderBy(m => m.Name));

                    case "Profit":
                        return Response.AsJson(Program.Trader.Materials.OrderBy(m => m.Profit));

                    case "ProfitPerLabor":
                        return Response.AsJson(Program.Trader.Materials.OrderBy(m => m.ProfitPerLabor));

                    case "Craft Cost":
                        return Response.AsJson(Program.Trader.Materials.OrderBy(m => m.MaterialCost));


                    default:
                        return Response.AsJson(Program.Trader.Materials);

                }
                
                //return Program.Trader.Materials;
            };

            Get["/material/list"] = param =>
            {
                return Response.AsJson(Program.Trader.Materials);
            };
            Get["/material/get/{id}", true] = async (param, ct) =>
            {
                // param.id
                await Task.Delay(100);
                return "OK";
            };
        }
    }
}
