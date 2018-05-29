using System;
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
    [Route("api/[controller]")]
    public class VehicleController : Controller
    {
        private readonly VegaDbContext context;
        private readonly IMapper mapper;

        public VehicleController(VegaDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("makes")]
        public async Task<IEnumerable<MakeResource>> GetMakes()
        {
            var makes = await this.context.Makes.Include(m => m.Models).ToListAsync();
            return this.mapper.Map<List<Make>, List<MakeResource>>(makes);
        }

        [HttpGet("features")]
        public async Task<IEnumerable<FeatureResource>> GetFeatures()
        {
            var features = await this.context.Features.ToListAsync();
            return this.mapper.Map<List<Feature>, List<FeatureResource>>(features);
        }

        [HttpPost]
        public async Task<IActionResult> CreateVehicle([FromBody] VehicleResource vehicleResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var modelEntity = await this.context.Models.FirstOrDefaultAsync(x => x.Id == vehicleResource.ModelId);
            if (modelEntity == null)
            {
                ModelState.AddModelError("FK_NotFound", "Model not found for given model id");
                return BadRequest(ModelState);
            }

            var vehicle = this.mapper.Map<VehicleResource, Vehicle>(vehicleResource);

            vehicle.LastUpdate = DateTime.Now;
            this.context.Vehicles.Add(vehicle);
            await this.context.SaveChangesAsync();

            var result = this.mapper.Map<Vehicle, VehicleResource>(vehicle);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVehicle(int id, [FromBody] VehicleResource vehicleResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vehicle = await this.context.Vehicles.Include(x => x.Features).SingleOrDefaultAsync(x => x.Id == id);
            this.mapper.Map<VehicleResource, Vehicle>(vehicleResource, vehicle);

            vehicle.LastUpdate = DateTime.Now;
            await this.context.SaveChangesAsync();

            var result = this.mapper.Map<Vehicle, VehicleResource>(vehicle);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vehicle = await this.context.Vehicles.SingleOrDefaultAsync(x => x.Id == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            this.context.Vehicles.Remove(vehicle);
            await this.context.SaveChangesAsync();
            return Ok(id);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicle(int id)
        {
            var vehicle = await this.context.Vehicles.Include(vega => vega.Features).SingleOrDefaultAsync(v => v.Id == id);
            if (vehicle == null)
            {
                ModelState.AddModelError("Novehicle", "Vehicle not found!");
                return NotFound();
            }
            var vehicleResource = this.mapper.Map<Vehicle, VehicleResource>(vehicle);

            return Ok(vehicleResource);
        }
    }
}