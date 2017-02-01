using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse.Models
{
    public class Material
    {
        public Material()
        {

        }

        public Material(string name, float price)
        {
            this.Name = name;
            this.Price = price;
            this.Id = Program.Trader.Materials.Count;
        }

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
        public float ProfitPerLabor => (Profit > Labor) ? (Profit / Labor) : 0;
        public float Margin => (Profit / Price) * 100;
        public float MaterialCost => 0;

        //public float MaterialCost => GetMats() != null ? GetMats().Sum(m => m.Key.SellPrice * m.Value) : SellPrice;
        
            // For storage.
        public Dictionary<int,int> CraftingRecipy { get; set; }
        public Dictionary<Material, int> GetMats() => CraftingRecipy?.ToDictionary(i => Program.Trader.Materials.FirstOrDefault(m => m.Id == i.Key), i => i.Value);


        // History
        public List<History> History { get; set; }

        public override string ToString()
        {
            return string.Format("{0}: {1}", Name, Price);
        }
    }
}
