using AuctionHouse.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse
{
    public class Trader
    {
        private string fileName = "trades.json";
        public List<Material> Materials = new List<Material>();

        public Trader()
        {
            Load();
            //LoadDummy();
        }

        void LoadDummy()
        {
            Materials.Add(new Material("test", 40));
            Materials.Add(new Material("asdsd", 40));
        }

        public void Save()
        {
            var json = JsonConvert.SerializeObject(Materials);
            File.WriteAllText(fileName, json);
        }

        public void Load()
        {
            if (File.Exists(fileName))
            {
                var json = File.ReadAllText(fileName);
                Materials = JsonConvert.DeserializeObject<List<Material>>(json);
                Console.WriteLine("Loaded {0} Materials.", Materials.Count);
            }
        }

        internal void Add(Material product)
        {
            Materials.Add(product);
            Save();
        }

        internal void UpdateRevenue(Material m)
        {
            if (Materials.Contains(m))
            {
                var index = Materials.IndexOf(m);
                var material = Materials[index];
                material.Price = m.Price;
                if (material.History == null)
                {
                    material.History = new List<History>();
                }
                material.History.Add(new History(m.Price, m.MaterialCost, m.Profit));

                Save();
            }
        }
        public float GetLastPrice(Material m)
        {
            var index = Materials.IndexOf(m);
            var lastPrice = Materials[index].History?.Last()?.Price;
            return lastPrice ?? 0;
        }
    }
}
