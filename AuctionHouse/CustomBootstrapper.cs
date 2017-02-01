using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;

namespace AuctionHouse
{
    public class CustomBootstrapper : DefaultNancyBootstrapper
    {
        // http://fagner.co/2016/01/07/Quick-tips-for-building-a-API-using-Nancy/
        // https://github.com/NancyFx/Nancy/wiki/Bootstrapper
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {



            //CORS Enable

            pipelines.AfterRequest += (ctx) =>
            {
                ctx.Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Origin");
                ctx.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                //ctx.Response.Headers.Add("Access-Control-Allow-Methods", "POST,GET");
            };

            //pipelines.AfterRequest.AddItemToEndOfPipeline((ctx) =>
            //{
            //    ctx.Response.WithHeader("Access-Control-Allow-Origin", "http://localhost:8080")
            //                    .WithHeader("Access-Control-Allow-Methods", "POST,GET")
            //                    .WithHeader("Access-Control-Allow-Headers", "Accept, Origin, Content-type");

            //});

            //pipelines.AfterRequest += ctx =>
            //{
            //    ctx.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            //    ctx.Response.Headers.Add("Access-Control-Allow-Headers", "Origin, X-Requested-With, Content-Type, Accept, Authorization");
            //    ctx.Response.Headers.Add("Access-Control-Allow-Methods", "DELETE");
            //};
            //base.ApplicationStartup(container, pipelines);
            Console.WriteLine("Custom bootstrapper..");
        }
    }
}
