﻿using Nancy.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse.Models
{
    public class History
    {
        public float Price { get; set; }

        public float MaterialCost { get; set; }
        public float Profit { get; set; }
        public DateTime Date { get; set; }
        public string User { get; set; }

        public History()
        {

        }
        public History(float price, float materialCost, float profit, IUserIdentity user)
        {
            this.Price = price;
            this.MaterialCost = materialCost;
            this.Profit = profit;
            this.Date = DateTime.Now;
            this.User = user.UserName;
        }
    }
}
