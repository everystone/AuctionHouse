using AuctionHouse.Repo;
using System.Collections.Generic;
using System.Linq;

namespace AuctionHouse.Models
{

    public class Ingredient
    {
        public int id;
        public int count;
    }

    public class Material : Entity
    {
        private IMaterialRepo _repo;

        public Material()
        {
        }
        public void SetRepo(IMaterialRepo repo)
        {
            _repo = repo;
        }

        //public Material(string name, float price, int id)
        //{
        //    this.Name = name;
        //    this.Price = price;
        //    this.Id = id;
        //}

        public string Name { get; set; }
        public float Price { get; set; }    // sell price
        public int Produce { get; set; }        // number of items produced when crafting
        public int Labor { get; set; }
        public float High { get; set; }
        public float Low { get; set; }
        public int Experience { get; set; }
        // should these be methods?
        //public float ListingFee => TotalSellPrice * 0.1f; // 10%
        public float TotalSellPrice => (Price * Produce);
        public float Fee => TotalSellPrice * 0.05f; // 5% trade fee
        public float Profit => Labor > 0 ? (TotalSellPrice - MaterialCost - Fee) : 0;
        public float ProfitPerLabor => ((Profit < Labor) || Labor == 0) ? 0 : (Profit / Labor);
        public float Margin => (Profit / Price) * 100;
        public float MaterialCost => GetMats() != null ? GetMats().Sum(m => m.Key.Price * m.Value) : Price;

            // For storage.
        public List<Ingredient> CraftingRecipe { get; set; }

        public Dictionary<Material, int> GetMats() => CraftingRecipe?.ToDictionary(i => _repo.List.FirstOrDefault(m => m.Id == i.id), i => i.count);


        // History
        public List<History> History { get; set; }

        public override string ToString()
        {
            return string.Format("{0}: {1}", Name, Price);
        }
    }
}
