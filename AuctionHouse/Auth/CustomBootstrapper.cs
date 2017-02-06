using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;
using AuctionHouse.Auth;
using Nancy.Authentication.Stateless;
using Nancy.Conventions;

namespace AuctionHouse
{
    public class CustomBootstrapper : DefaultNancyBootstrapper
    {
        // http://fagner.co/2016/01/07/Quick-tips-for-building-a-API-using-Nancy/
        // https://github.com/NancyFx/Nancy/wiki/Bootstrapper
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {

            base.ApplicationStartup(container, pipelines);
            // Stateless Authentication
            // https://github.com/NancyFx/Nancy/wiki/Stateless-authentication
            // http://thesenilecoder.blogspot.no/2016/11/nancyfx-stateless-auth-example.html
            var statelessAuthConfigurationFactory = container.Resolve<IStatelessAuthConfigurationFactory>();
            var configuration = statelessAuthConfigurationFactory.Create();
            StatelessAuthentication.Enable(pipelines, configuration);

            //CORS Enable
            pipelines.AfterRequest += (ctx) =>
            {
                ctx.Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Origin, Authorization");
                ctx.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                //ctx.Response.Headers.Add("Access-Control-Allow-Methods", "POST,GET");
            };


            Console.WriteLine("Custom bootstrapper..");
        }
        protected override void ConfigureConventions(NancyConventions nancyConventions)
        {
            nancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("/", "Content"));
            base.ConfigureConventions(nancyConventions);
        }
    }
}
