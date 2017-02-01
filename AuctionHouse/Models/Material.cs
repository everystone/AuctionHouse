using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse.Models
{
    public class Material
    {
        public int Id { get; set; }
        public int Produce { get; set; }        // number of items produced when crafting
        public string Name { get; set; }
        public float Price { get; set; }    // sell price
        public int Labor { get; set; }
        public int Experience { get; set; }

        // should these be methods?
        //public float ListingFee => TotalSellPrice * 0.1f; // 10%
        public float TotalSellPrice => (Price * Produce);
        public float Fee => TotalSellPrice * 0.05f; // 5% trade fee
        public float Profit => (TotalSellPrice - MaterialCost - Fee);
        public float ProfitPerLabor => Profit / Labor;
        public float Margin => (Profit / Price) * 100;
        public float MaterialCost => 0;
        //public float MaterialCost => GetMats() != null ? GetMats().Sum(m => m.Key.SellPrice * m.Value) : SellPrice;

        public override string ToString()
        {
            return string.Format("{0}: {1}", Name, Price);
        }
    }
}
