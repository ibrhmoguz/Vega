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
                new Model{Name="Model1"},
                new Model{Name="Model2"},
                new Model{Name="Model3"},
                new Model{Name="Model4"},
            };


            var modelList2 = new List<Model>{
                new Model{Name="Model5"},
                new Model{Name="Model6"},
                new Model{Name="Model7"}
            };

            modelList1.ForEach(m => dbContext.Models.Add(m));
            modelList2.ForEach(m => dbContext.Models.Add(m));

            var makeList = new List<Make>{
                new Make{Name="Make1", Models= modelList1},
                new Make{Name="Make2", Models= modelList2}
            };

            makeList.ForEach(m => dbContext.Makes.Add(m));
            dbContext.SaveChanges();
        }
    }
}