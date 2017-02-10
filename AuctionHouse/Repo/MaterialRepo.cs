using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionHouse.Models;
using Newtonsoft.Json;
using System.IO;
using Nancy.Security;

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

        public List<Material> Update(Material entity, IUserIdentity user)
        {
            var search = List.FirstOrDefault(m => m.Id == entity.Id);
            if(search != null)
            {
                var index = List.IndexOf(search);
                var material = List[index];
                material.SetRepo(this);
                // Are we only updating price? (quickEdit)
                if (entity.Name == null)
                {
                    material.Price = entity.Price;
                }else
                {
                    // Full update, overwrite with all data from client
                    material = entity;
                }

                
                // high & low
                material.High = material.Price > material.High ? material.Price : material.High;

                // temporary fix
                if (material.Low == 0) material.Low = material.Price;

                material.Low = material.Price < material.Low ? material.Price : material.Low;

                if (material.History == null)
                {
                    material.History = new List<History>();
                }
                material.History.Add(new History(material.Price, material.MaterialCost, material.Profit, user));
                // because changing the price of an item affects all items that has it in the recipe, send all.
                // find all affected items
                var affected = List.Where(i => i.CraftingRecipe != null && i.CraftingRecipe.Any(r => r.id == material.Id)).ToList();
                Console.WriteLine("Updated: {0}, affected: {1}", material, string.Join(",", affected.Select(a => a.Name)));
                affected.Add(material); // Also return the updated item.
                Save();

                // broadcast event via SignalR...?
                return affected;
            }
            else
            {
                entity.SetRepo(this);
                entity.Id = List.Count + 1;
                entity.High = entity.Price;
                entity.Low = entity.Price;
                List.Add(entity);
                Console.WriteLine("New material added: " + entity);
                Save();
                return new List<Material>() { entity };
            }
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
