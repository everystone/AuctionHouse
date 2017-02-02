using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionHouse.Models;
using Newtonsoft.Json;
using System.IO;

namespace AuctionHouse.Repo
{
    class MaterialRepo : IMaterialRepo
    {
        public List<Material> List { get; set; }
        private string fileName = "trades.json";
        public MaterialRepo()
        {
            Load();
        }

        public void Add(Material entity)
        {
            List.Add(entity);
            Save();
        }

        public void Delete(Material entity)
        {
            throw new NotImplementedException();
        }

        public Material FindById(int id)
        {
            return List.FirstOrDefault(m => m.Id == id);
        }

        public void Update(Material entity)
        {
            var index = List.IndexOf(entity);
            if(index > 0)
            {
                var material = List[index];

                material.Price = entity.Price;
                if (material.History == null)
                {
                    material.History = new List<History>();
                }
                material.History.Add(new History(entity.Price, entity.MaterialCost, entity.Profit));
                Console.WriteLine("Material Updated: " + entity);
            }
            else
            {
                List.Add(entity);
                Console.WriteLine("New material added: " + entity);
            }

            Save();
        }

        public void Save()
        {
            var json = JsonConvert.SerializeObject(List);
            File.WriteAllText(fileName, json);
        }

        public void Load()
        {
            if (File.Exists(fileName))
            {
                var json = File.ReadAllText(fileName);
                List = JsonConvert.DeserializeObject<List<Material>>(json);
                List.ForEach(m => m.SetRepo(this));
                Console.WriteLine("Loaded {0} Materials.", List.Count);
            }
        }
    }
}
