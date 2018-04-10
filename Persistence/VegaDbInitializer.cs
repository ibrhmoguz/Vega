using System.Collections.Generic;
using System.Linq;
using Vega.Models;

namespace Vega.Persistence
{
    public static class VegaDbInitializer
    {
        public static void Initialize(VegaDbContext dbContext)
        {
            dbContext.Database.EnsureCreated();

            if (dbContext.Makes.Any())
            {
                return;
            }

            var modelList1 = new List<Model>{
                new Model{Name="E350"},
                new Model{Name="E220"},
                new Model{Name="SLA 120"},
                new Model{Name="C180"},
            };


            var modelList2 = new List<Model>{
                new Model{Name="C5"},
                new Model{Name="C4"},
                new Model{Name="C3"}
            };

            modelList1.ForEach(m => dbContext.Models.Add(m));
            modelList2.ForEach(m => dbContext.Models.Add(m));

            var makeList = new List<Make>{
                new Make{Name="Mercedes-Benz", Models= modelList1},
                new Make{Name="Citroen", Models= modelList2}
            };

            makeList.ForEach(m => dbContext.Makes.Add(m));
            dbContext.SaveChanges();
        }
    }
}