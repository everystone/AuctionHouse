﻿using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse
{
    class Program
    {
        public static Trader Trader; // use dependency service?

        static void Main(string[] args)
        {
            // netsh http add urlacl url=http://+:88/ user=detec\eirik
            var url = "http://+:3333";
            Trader = new AuctionHouse.Trader();

            using (WebApp.Start<Startup>(url))
            {
                Console.WriteLine("Running on {0}", url);
                Console.WriteLine("Press enter to exit");
                Console.ReadLine();
            }
        }

        

    }
}
