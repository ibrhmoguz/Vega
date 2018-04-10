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
                new Model{Id=1, Name="Model1"},
                new Model{Id=1, Name="Model2"},
                new Model{Id=1, Name="Model3"},
                new Model{Id=1, Name="Model4"},
            };


            var modelList2 = new List<Model>{
                new Model{Id=1, Name="Model5"},
                new Model{Id=1, Name="Model6"},
                new Model{Id=1, Name="Model7"}
            };

            var makeList = new List<Make>{
                new Make{Id=1,Name="Make1", Models= modelList1},
                new Make{Id=2,Name="Make2", Models= modelList2}
            };

            modelList1.ForEach(m => dbContext.Models.Add(m));
            modelList2.ForEach(m => dbContext.Models.Add(m));
            makeList.ForEach(m => dbContext.Makes.Add(m));
        }
    }
}