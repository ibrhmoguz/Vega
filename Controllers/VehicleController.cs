using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vega.Models;
using Vega.Persistence;
using Vega.Resources;

namespace Vega.Controllers
{
    public class VehicleController
    {
        private readonly VegaDbContext context;
        private readonly IMapper mapper;

        public VehicleController(VegaDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("api/vehicle/makes")]
        public async Task<IEnumerable<MakeResource>> GetMakes()
        {
            var makes = await this.context.Makes.Include(m => m.Models).ToListAsync();
            return this.mapper.Map<List<Make>, List<MakeResource>>(makes);
        }

        [HttpGet("api/vehicle/features")]
        public async Task<IEnumerable<FeatureResource>> GetFeatures()
        {
            var features = await this.context.Features.ToListAsync();
            return this.mapper.Map<List<Feature>, List<FeatureResource>>(features);
        }
    }
}